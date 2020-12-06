using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZMO_Eskisehir_WebUI.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace ZMO_Eskisehir_WebUI.Controllers
{
    public class ErrorController : Controller
    {
        // when error occured, this controller will be trigerred. (startup.cs)
        // dont forget to change development to production in launchsettings.json
        public IActionResult Index()
        {
            ErrorModel errorModel = new ErrorModel();
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            errorModel.ErrorMessage = exceptionDetails.Error.Message;
            errorModel.ExtraMessage = exceptionDetails.Path;
            return View(errorModel);
        }
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
