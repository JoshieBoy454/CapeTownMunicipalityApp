///-----------------------------------Start of File---------------------------------->
namespace CapeTownMunicipalityApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public EventCategory Category { get; set; } 
        public EventType Type { get; set; } 
        public int Priority { get; set; } = 1; 
        public string Location { get; set; } = string.Empty; 
        public string ContactInfo { get; set; } = string.Empty; 
    }
}
///-----------------------------------End of File----------------------------------->