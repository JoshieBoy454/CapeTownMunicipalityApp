using CapeTownMunicipalityApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CapeTownMunicipalityApp.Services
{
    public class ReportService : IReportService
    {
        private readonly LocalDbContext _db;
        private readonly string _imagePath;

        private readonly DoublyLinkedList<Report> _reportList = new();
        private readonly DoublyLinkedList<ReportAttatchment> _reportAttatchmentList = new();

        public ReportService(LocalDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _imagePath = Path.Combine(env.WebRootPath, "images");
            if (!Directory.Exists(_imagePath)) Directory.CreateDirectory(_imagePath);
            var existing = _db.Report.Include(r => r.Attatchments).OrderBy(r => r.Id).ToList();
            foreach(var r in existing) _reportList.AddLast(r);
        }
        public async Task<Report> CreateReportAsync(string location, ReportCategory category, string description, IEnumerable<IFormFile> files)
        {
            var report = new Report
            {
                Location = location,
                Category = category,
                Description = description
            };
            _db.Report.Add(report);
            await _db.SaveChangesAsync();

            foreach(var f in files?? Enumerable.Empty<IFormFile>())
            {
                if (f.Length <= 0) continue;
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(f.FileName)}";
                var filePath = Path.Combine(_imagePath, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await f.CopyToAsync(fs);
                }
                var attatchment = new ReportAttatchment
                {
                    ReportId = report.Id,
                    FileName = f.FileName,
                    FilePath = $"/images/{fileName}",
                };
                _db.ReportAttatchment.Add(attatchment);
                report.Attatchments.Add(attatchment);
            }
            await _db.SaveChangesAsync();

            _reportList.AddLast(report);
            foreach (var a in report.Attatchments)
            {
                _reportAttatchmentList.AddLast(a);
            }
            return report;
        }

        public Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Report?> GetReportAsync(int id)
        {
            foreach (var r in _reportList)
                if (r.Id == id) return r;
            return await _db.Report.Include(r => r.Attatchments).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
