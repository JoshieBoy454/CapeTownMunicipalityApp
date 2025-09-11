using Microsoft.AspNetCore.Mvc;

namespace CapeTownMunicipalityApp.Controllers
{
    public class EventsAndAnnouncementsController : Controller
    {
        public IActionResult Temp()
        {
            ViewData["Title"] = "Events";
            return View();
        }
    }
}
