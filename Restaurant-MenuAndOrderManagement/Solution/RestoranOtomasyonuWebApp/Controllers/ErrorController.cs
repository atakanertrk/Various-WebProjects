using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestoranOtomasyonuWebApp.Models;

namespace RestoranOtomasyonuWebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            ErrorModel errorModel = new ErrorModel();
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            errorModel.ErrorMessage = exceptionDetails.Error.Message;
            return View("Error", errorModel);
        }
    }
}
