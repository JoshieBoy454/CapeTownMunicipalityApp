using CapeTownMunicipalityApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CapeTownMunicipalityApp.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly LocalDbContext _db;
        private readonly StatusGraph _graph = new();

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
            // Load all into BST for O(log n) lookups by key (demonstrative; EF can query directly)
            var items = await _db.Report.AsNoTracking()
                .Include(r => r.Attatchments)
                .ToListAsync();
            var bst = new BinarySearchTree<string, Report>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in items)
                bst.Insert(r.TrackingCode, r);

            return bst.TryGetValue(trackingCode, out var found) ? found : null;
        }

        public Task<(double percent, List<ReportStatus> path)> GetProgressAsync(ReportStatus current)
        {
            var path = _graph.ShortestPath(ReportStatus.Submitted, current);
            var percent = _graph.GetProgressPercent(current);
            return Task.FromResult((percent, path));
        }
    }
}


