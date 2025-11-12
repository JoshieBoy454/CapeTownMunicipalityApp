using CapeTownMunicipalityApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CapeTownMunicipalityApp.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly IServiceRequestService _service;
        public ServiceRequestsController(IServiceRequestService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? q)
        {
            ViewData["Title"] = "Services";
            var items = await _service.GetAllAsync(q);
            return View(items);
        }

        public async Task<IActionResult> Status(string trackingCode)
        {
            if (string.IsNullOrWhiteSpace(trackingCode))
                return RedirectToAction(nameof(Index));

            var report = await _service.FindByTrackingCodeAsync(trackingCode);
            if (report == null)
            {
                TempData["ErrorMessage"] = "Tracking code not found.";
                return RedirectToAction(nameof(Index));
            }

            var progress = await _service.GetProgressAsync(report.Status);
            ViewBag.ProgressPercent = progress.percent;
            ViewBag.ProgressPath = progress.path;
            ViewData["Title"] = "Services";
            return View(report);
        }
    }
}


