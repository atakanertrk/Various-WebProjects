using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnurShopSecond.Models;

namespace OnurShopSecond.Controllers
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
            if (exceptionDetails.Error.InnerException != null)
            {
                errorModel.InnerMessage = exceptionDetails.Error.InnerException.Message;
            }
            return View("Error",errorModel);
        }
    }
}
