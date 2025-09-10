namespace CapeTownMunicipalityApp.Models
{
    public class ReportAttatchment
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string FilePath { get; set; } = "";
        public string FileType { get; set; } = "";
    }
}
