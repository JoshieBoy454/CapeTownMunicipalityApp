///-----------------------------------Start of File---------------------------------->
using System;
using System.Collections.Generic;
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
        private static readonly Dictionary<ReportCategory, int> _categoryPriorities = new()
        {
            [ReportCategory.Utilities] = 1, // High
            [ReportCategory.Sanitation] = 2, // Medium
            [ReportCategory.Roads] = 2, // Medium
            [ReportCategory.Other] = 3 // Low
        };
        ///------------------------------------------------------------------------>
        public ReportService(LocalDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _imagePath = Path.Combine(env.WebRootPath, "images");
            if (!Directory.Exists(_imagePath))
                Directory.CreateDirectory(_imagePath);
        }
        ///------------------------------------------------------------------------>
        public async Task<Report> CreateReportAsync(string location, ReportCategory category, string description, IEnumerable<IFormFile> files)
        {
            var report = new Report
            {
                Location = location,
                Category = category,
                Description = description,
                Status = ReportStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Priority = _categoryPriorities.TryGetValue(category, out var priority) ? priority : 2
            };

            report.TrackingCode = await GenerateUniqueTrackingCodeAsync();

            _db.Report.Add(report);
            await _db.SaveChangesAsync();

            foreach (var f in files ?? Enumerable.Empty<IFormFile>())
            {
                if (f.Length <= 0) continue; 
                
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(f.FileName)}";
                var filePath = Path.Combine(_imagePath, fileName);
                
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await f.CopyToAsync(fs);
                }
                
                var attachment = new ReportAttatchment
                {
                    ReportId = report.Id,
                    FileName = f.FileName, 
                    FilePath = $"/images/{fileName}", 
                };
                
                _db.ReportAttatchment.Add(attachment);
                report.Attatchments.Add(attachment);
            }
            report.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            _reportList.AddLast(report);
            foreach (var a in report.Attatchments)
            {
                _reportAttatchmentList.AddLast(a);
            }
            
            return report;
        }
        private async Task<string> GenerateUniqueTrackingCodeAsync()
        {
            for (var attempt = 0; attempt < 15; attempt++)
            {
                var raw = Guid.NewGuid().ToString("N").ToUpperInvariant();
                var candidate = $"SR-{raw[..4]}-{raw[4..8]}";

                if (!await _db.Report.AnyAsync(r => r.TrackingCode == candidate))
                {
                    return candidate;
                }
            }

            throw new InvalidOperationException("Unable to generate a unique tracking code.");
        }
        ///------------------------------------------------------------------------>
        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            var reports = await _db.Report.Include(r => r.Attatchments).OrderBy(r => r.Id).ToListAsync();
            
            _reportList.Clear();
            foreach (var r in reports) _reportList.AddLast(r);
            
            return reports;
        }
        ///------------------------------------------------------------------------>
        public async Task<Report?> GetReportAsync(int id)
        {
            foreach (var r in _reportList)
                if (r.Id == id) return r;
                
            var report = await _db.Report.Include(r => r.Attatchments).FirstOrDefaultAsync(r => r.Id == id);
            if (report != null)
            {
                _reportList.AddLast(report);
                foreach (var a in report.Attatchments)
                    _reportAttatchmentList.AddLast(a);
            }
            return report;
        }
        ///------------------------------------------------------------------------>
    }
}
///-----------------------------------End of File----------------------------------->