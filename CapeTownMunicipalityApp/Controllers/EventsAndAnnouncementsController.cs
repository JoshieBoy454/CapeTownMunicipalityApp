///-----------------------------------Start of File---------------------------------->
using Microsoft.AspNetCore.Mvc;
using CapeTownMunicipalityApp.Services;
using CapeTownMunicipalityApp.Models;

namespace CapeTownMunicipalityApp.Controllers
{
/// <summary>
/// Class: EventsAndAnnouncementsController
/// 
/// </summary>
/// <remarks>
/// </remarks>
/// <author>Joshua Gain</author>
    public class EventsAndAnnouncementsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsAndAnnouncementsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        ///------------------------------------------------------------------------>
        /// <summary>
        /// Method: IActionResult Index
        /// Main page displaying events and announcements with filtering
        /// </summary>
        /// <param name="category">Optional category filter</param>
        /// <param name="type">Optional type filter (Event/Announcement)</param>
        /// <param name="startDate">Optional start date filter</param>
        /// <param name="endDate">Optional end date filter</param>
        /// <returns></returns>
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
        /// <summary>
        /// Method: IActionResult GetEventDetails
        /// Returns event details for modal popup
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns></returns>
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
        /// <summary>
        /// Method: IActionResult TrackInteraction
        /// Tracks user interaction with categories for recommendations
        /// </summary>
        /// <param name="category">Category name</param>
        /// <returns></returns>
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
        /// <summary>
        /// Method: IActionResult GetRecommendations
        /// Returns recommended events based on user interactions
        /// </summary>
        /// <param name="userInteractions">JSON string of user interactions</param>
        /// <returns></returns>
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