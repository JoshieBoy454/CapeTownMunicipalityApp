using CapeTownMunicipalityApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CapeTownMunicipalityApp.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly LocalDbContext _db;

        public ServiceRequestService(LocalDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Report>> GetAllAsync(string? query = null)
        {
            var baseQuery = _db.Report
                .Include(r => r.Attatchments)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var q = query.Trim();
                baseQuery = baseQuery.Where(r =>
                    r.TrackingCode.Contains(q) ||
                    r.Location.Contains(q) ||
                    r.Description.Contains(q));
            }

            var list = await baseQuery.ToListAsync();

            // Prioritize using MinHeap: lower Priority first, then older CreatedAt
            var heap = new MinHeap<Report>((a, b) =>
            {
                var cmp = a.Priority.CompareTo(b.Priority);
                if (cmp != 0) return cmp;
                return a.CreatedAt.CompareTo(b.CreatedAt);
            });
            foreach (var r in list) heap.Push(r);
            return heap.PopAll().ToList();
        }

        public async Task<Report?> FindByTrackingCodeAsync(string trackingCode)
        {
            return await _db.Report
                .Include(r => r.Attatchments)
                .FirstOrDefaultAsync(r => r.TrackingCode == trackingCode);
        }

        public async Task<bool> UpdatePriorityAsync(string trackingCode, bool increase)
        {
            var report = await _db.Report.FirstOrDefaultAsync(r => r.TrackingCode == trackingCode);
            if (report == null)
                return false;

            // Normalize priority to valid range (1-3)
            var currentPriority = report.Priority switch
            {
                <= 1 => 1,
                >= 3 => 3,
                _ => report.Priority
            };

            // Priority: 1=High, 2=Medium, 3=Low
            // Increase means higher priority (lower number), decrease means lower priority (higher number)
            if (increase && currentPriority > 1)
            {
                report.Priority = currentPriority - 1;
            }
            else if (!increase && currentPriority < 3)
            {
                report.Priority = currentPriority + 1;
            }
            else
            {
                return false; // Already at min/max
            }

            report.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}


