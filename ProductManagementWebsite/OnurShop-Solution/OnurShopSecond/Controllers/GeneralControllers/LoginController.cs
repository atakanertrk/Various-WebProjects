using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnurShopSecond.DataAccess;
using OnurShopSecond.Models;

namespace OnurShopSecond.Controllers
{
    public class LoginController : Controller
    {
        private IDataAccess _dataAccess;
        private ILoginCookie _loginCookie;
        public LoginController(IDataAccess sqlDataAccess,ILoginCookie loginCookie)
        {
            _loginCookie = loginCookie;
            _dataAccess = sqlDataAccess;
        }
        public IActionResult Login2()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login2(LoginModel loginModel)
        {
            if (_loginCookie.LogIn(loginModel.Password))
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,loginModel.Password)
                };
                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principle = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principle,props).Wait();
                return RedirectToAction("UrunGuruplari", "Admin");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> LogOut2()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login2");
        }
    }
}
