using CapeTownMunicipalityApp.Models;
using CapeTownMunicipalityApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CapeTownMunicipalityApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Form()
        {
            ViewData["Title"] = "Form";
            return View();
        }
        public async Task<IActionResult> Submit(string location, ReportCategory category, string description, List<IFormFile> attatchments)
        {
            var reort = await _reportService.CreateReportAsync(location ?? "", category, description ?? "", attatchments);
            return RedirectToAction("Index", "Home");
        }
    }
}
