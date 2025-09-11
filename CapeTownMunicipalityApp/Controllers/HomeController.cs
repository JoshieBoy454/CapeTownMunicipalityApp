using System.Diagnostics;
using CapeTownMunicipalityApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;

namespace CapeTownMunicipalityApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            ViewData["CurrentCulture"] = Thread.CurrentThread.CurrentCulture.Name;

            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Settings";
            return View();
        }

        [HttpPost]
        // Using the cookies to save what language the applicaiotn will be in that way if it is closed and reopened it will still be in the selected language
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
