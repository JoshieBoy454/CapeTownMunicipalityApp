///-----------------------------------Start of File---------------------------------->
using Microsoft.AspNetCore.Mvc;
using CapeTownMunicipalityApp.Services;
using CapeTownMunicipalityApp.Models;

namespace CapeTownMunicipalityApp.Controllers
{
    /// <summary>
    /// Class: EventsAndAnnouncementsController
    /// The main controller for the events/announcements page
    /// </summary>
    /// <remarks>
    /// displays all the events/announcements and allows user to filter them
    /// It displays the recommendations for user
    /// </remarks>
    /// <author>Joshua Gain</author>
    public class EventsAndAnnouncementsController : Controller
    {
        private readonly IEventService _eventService;
        ///------------------------------------------------------------------------>
        /// <summary>
        /// Method: EventsAndAnnouncementsController
        /// Constructor injects the service
        /// </summary>
        /// <param name="eventService">The service to get the events from</param>
        /// <returns></returns>
        public EventsAndAnnouncementsController(IEventService eventService)
        {
            _eventService = eventService;
        }
        ///------------------------------------------------------------------------>
        public IActionResult Index(EventCategory? category = null, EventType? type = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            ViewData["Title"] = "Events & Announcements";
            
            var events = _eventService.GetFilteredEvents(category, type, startDate, endDate);
            var categories = _eventService.GetAvailableCategories();
            var dates = _eventService.GetAvailableDates();
            
            ViewBag.Events = events;
            ViewBag.Categories = categories;
            ViewBag.Dates = dates;
            ViewBag.SelectedCategory = category; 
            ViewBag.SelectedType = type;
            ViewBag.SelectedStartDate = startDate;
            ViewBag.SelectedEndDate = endDate;
            
            return View();
        }
        ///------------------------------------------------------------------------>
        public IActionResult GetEventDetails(int id)
        {
            var eventItem = _eventService.GetEventById(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            
            return PartialView("_EventModal", eventItem);
        }
        ///------------------------------------------------------------------------>
        [HttpPost]
        public IActionResult TrackInteraction(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                _eventService.TrackUserInteraction(category);
            }
            
            return Json(new { success = true });
        }
        ///------------------------------------------------------------------------>
        [HttpPost]
        public IActionResult GetRecommendations(string userInteractions)
        {
            var recommendations = _eventService.GetRecommendedEvents(userInteractions);
            
            return PartialView("_Recommendations", recommendations);
        }
        ///------------------------------------------------------------------------>
    }
}
///-----------------------------------End of File----------------------------------->