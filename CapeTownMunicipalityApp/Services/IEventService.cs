using CapeTownMunicipalityApp.Models;

namespace CapeTownMunicipalityApp.Services
{
    public interface IEventService
    {
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Event> GetEventsByCategory(EventCategory category);
        IEnumerable<Event> GetEventsByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<Event> GetEventsByType(EventType type);
        IEnumerable<Event> GetFilteredEvents(EventCategory? category, EventType? type, DateTime? startDate, DateTime? endDate);
        Event? GetEventById(int id);
        IEnumerable<Event> GetRecommendedEvents(string userInteractionsJson);
        IEnumerable<EventCategory> GetAvailableCategories();
        IEnumerable<DateTime> GetAvailableDates();
        void TrackUserInteraction(string category);
    }
}
