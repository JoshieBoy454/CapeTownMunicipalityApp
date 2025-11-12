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

            ViewData["Title"] = "Services";
            return View(report);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UpdatePriority(string trackingCode, bool increase)
        {
            if (string.IsNullOrWhiteSpace(trackingCode))
                return Json(new { success = false, message = "Tracking code is required." });

            var success = await _service.UpdatePriorityAsync(trackingCode, increase);
            if (!success)
                return Json(new { success = false, message = "Unable to update priority." });

            return Json(new { success = true });
        }
    }
}


