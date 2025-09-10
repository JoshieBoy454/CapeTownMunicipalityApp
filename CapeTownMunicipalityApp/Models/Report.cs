namespace CapeTownMunicipalityApp.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Location { get; set; } = "";
        public ReportCategory Category { get; set; }
        public string Description { get; set; } = "";
        public List<ReportAttatchment> Attatchments { get; set; } = new();
    }
}
