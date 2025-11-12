using CapeTownMunicipalityApp.Models;

namespace CapeTownMunicipalityApp.Services
{
    public interface IServiceRequestService
    {
        Task<IEnumerable<Report>> GetAllAsync(string? query = null);
        Task<Report?> FindByTrackingCodeAsync(string trackingCode);
        Task<bool> UpdatePriorityAsync(string trackingCode, bool increase);
    }
}


