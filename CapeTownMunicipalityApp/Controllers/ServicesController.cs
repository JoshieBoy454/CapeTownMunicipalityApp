using Microsoft.AspNetCore.Mvc;

namespace CapeTownMunicipalityApp.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Temp()
        {
            ViewData["Title"] = "Services";
            return View();
        }
    }
}
