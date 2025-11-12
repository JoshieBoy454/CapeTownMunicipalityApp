using CapeTownMunicipalityApp.Models;

namespace CapeTownMunicipalityApp.Services
{
    public interface IServiceRequestService
    {
        Task<IEnumerable<Report>> GetAllAsync(string? query = null);
        Task<Report?> FindByTrackingCodeAsync(string trackingCode);
        Task<(double percent, List<ReportStatus> path)> GetProgressAsync(ReportStatus current);
    }
}


