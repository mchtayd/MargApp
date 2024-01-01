using Microsoft.AspNetCore.Mvc;

namespace MVC_WebInterface.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Aradığınız Sayfa Bulunamadı..";
                    break;
            }

            return View("Error");
        }

        [Route("Error")]
        public IActionResult ServerError()
        {
            ViewBag.ErrorMessage = "İşleminiz Gerçekleştirilemedi..";
            return View("Error");
        }
    }
}
