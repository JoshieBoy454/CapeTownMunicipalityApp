using CapeTownMunicipalityApp.Models;

namespace CapeTownMunicipalityApp.Services
{
    public interface IReportService
    {
        Task<Report> CreateReportAsync(string location, ReportCategory category, string description, IEnumerable<IFormFile> imageFile);
        Task<Report?> GetReportAsync(int Id);
        Task<IEnumerable<Report>> GetAllReportsAsync();
    }
}
