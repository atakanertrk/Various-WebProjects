using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZMO_Eskisehir_WebUI.Models;
using ZMO_Eskisehir_WebUI.DataAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace ZMO_Eskisehir_WebUI.Controllers
{
    public class LoginController : Controller
    {
        private SqlDataAccess dataAccess;
        public LoginController(IConfiguration config)
        {
            dataAccess = new SqlDataAccess(config);
        }
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Giris(LoginModel loginModel)
        {
            if (dataAccess.GetSifre().FirstOrDefault().Password == loginModel.Password)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,loginModel.Password)
                };
                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principle = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, props).Wait();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Giris");
        }

    }
}
