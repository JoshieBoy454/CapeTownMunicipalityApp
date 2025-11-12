///-----------------------------------Start of File---------------------------------->
namespace CapeTownMunicipalityApp.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Location { get; set; } = ""; 
        public ReportCategory Category { get; set; } 
        public string Description { get; set; } = ""; 

        // Service request tracking
        public string TrackingCode { get; set; } = ""; // unique human-friendly code
        public ReportStatus Status { get; set; } = ReportStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int Priority { get; set; } = 2; // 1=High, 2=Medium, 3=Low

        public List<ReportAttatchment> Attatchments { get; set; } = new(); 
    }
}
///-----------------------------------End of File----------------------------------->