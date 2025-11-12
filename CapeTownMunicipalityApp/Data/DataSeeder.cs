using System;
using System.Collections.Generic;
using System.Linq;
using CapeTownMunicipalityApp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CapeTownMunicipalityApp.Data
{
    internal static class DataSeeder
    {
        private const int MinimumServiceRequestCount = 20;

        private static readonly (string Location, ReportCategory Category, string Description, int Priority, ReportStatus Status)[] SeedReports =
        {
            ("Cape Town CBD - Adderley Street", ReportCategory.Utilities, "Burst water pipe causing flooding near bus stop.", 1, ReportStatus.Pending),
            ("Bellville - Durban Road", ReportCategory.Roads, "Deep potholes slowing traffic near the taxi rank.", 2, ReportStatus.Pending),
            ("Khayelitsha - Spine Road", ReportCategory.Sanitation, "Blocked stormwater drain causing standing water.", 2, ReportStatus.Pending),
            ("Sea Point - Regent Road", ReportCategory.Utilities, "Street lights flickering intermittently.", 1, ReportStatus.Resolved),
            ("Mitchells Plain - Westgate Mall", ReportCategory.Other, "Broken public bench outside main entrance.", 3, ReportStatus.Pending),
            ("Claremont - Main Road", ReportCategory.Roads, "Damaged pedestrian crossing markings need repainting.", 3, ReportStatus.Pending),
            ("Somerset West - Lourensford Road", ReportCategory.Sanitation, "Overflowing waste bins after weekend market.", 2, ReportStatus.Closed),
            ("Woodstock - Albert Road", ReportCategory.Utilities, "Low water pressure reported by several households.", 1, ReportStatus.Pending),
            ("Durbanville - Wellington Road", ReportCategory.Roads, "Traffic signal timing issue causing congestion.", 3, ReportStatus.Pending),
            ("Muizenberg - Beachfront", ReportCategory.Sanitation, "Public toilets require urgent cleaning.", 2, ReportStatus.Resolved),
            ("Harfield Village - 2nd Avenue", ReportCategory.Other, "Graffiti on community notice board.", 3, ReportStatus.Pending),
            ("Observatory - Lower Main Road", ReportCategory.Utilities, "Power outage affecting small businesses.", 1, ReportStatus.Pending),
            ("Hout Bay - Victoria Avenue", ReportCategory.Roads, "Rockfall debris partially blocking roadway.", 3, ReportStatus.Closed),
            ("Green Point - Somerset Road", ReportCategory.Sanitation, "Sewage odour reported near restaurant strip.", 2, ReportStatus.Pending),
            ("Mowbray - Liesbeek Parkway", ReportCategory.Utilities, "Stormwater grate stolen exposing open hole.", 1, ReportStatus.Pending),
            ("Gugulethu - NY1", ReportCategory.Other, "Community centre notice board requires update.", 3, ReportStatus.Pending),
            ("Fish Hoek - Main Road", ReportCategory.Roads, "Sinkhole forming near pedestrian walkway.", 2, ReportStatus.Pending),
            ("Parow - Voortrekker Road", ReportCategory.Sanitation, "Illegal dumping piling up behind shops.", 3, ReportStatus.Resolved),
            ("Atlantis - Reygersdal Drive", ReportCategory.Utilities, "Water treatment plant alarm triggered overnight.", 2, ReportStatus.Pending),
            ("Bloubergstrand - Marine Circle", ReportCategory.Other, "Damaged signage at beachfront car park.", 3, ReportStatus.Pending)
        };

        public static async Task EnsureSeedDataAsync(LocalDbContext db)
        {
            // Normalize existing priorities to valid range (1-3)
            await NormalizePrioritiesAsync(db);
            
            var existingCount = await db.Report.CountAsync();
            if (existingCount >= MinimumServiceRequestCount)
            {
                return;
            }

            var existingCodes = new HashSet<string>(
                await db.Report
                    .Select(r => r.TrackingCode)
                    .Where(code => !string.IsNullOrWhiteSpace(code))
                    .ToListAsync(),
                StringComparer.OrdinalIgnoreCase);

            var reportsToAdd = new List<Report>();
            var now = DateTime.UtcNow;

            foreach (var seed in SeedReports)
            {
                if (existingCount + reportsToAdd.Count >= MinimumServiceRequestCount)
                {
                    break;
                }

                var createdAt = now.AddHours(-6 * (existingCount + reportsToAdd.Count + 1));

                var report = new Report
                {
                    Location = seed.Location,
                    Category = seed.Category,
                    Description = seed.Description,
                    Priority = seed.Priority,
                    Status = seed.Status,
                    CreatedAt = createdAt,
                    UpdatedAt = seed.Status == ReportStatus.Pending ? (DateTime?)null : createdAt.AddHours(4)
                };

                report.TrackingCode = GenerateTrackingCode(existingCodes);
                existingCodes.Add(report.TrackingCode);

                reportsToAdd.Add(report);
            }

            if (reportsToAdd.Count == 0)
            {
                return;
            }

            db.Report.AddRange(reportsToAdd);
            await db.SaveChangesAsync();
        }

        private static string GenerateTrackingCode(ISet<string> existingCodes)
        {
            for (var attempt = 0; attempt < 20; attempt++)
            {
                var raw = Guid.NewGuid().ToString("N").ToUpperInvariant();
                var candidate = $"SR-{raw[..4]}-{raw[4..8]}";
                if (!existingCodes.Contains(candidate))
                {
                    return candidate;
                }
            }

            throw new InvalidOperationException("Unable to generate a unique tracking code for seed data.");
        }

        private static async Task NormalizePrioritiesAsync(LocalDbContext db)
        {
            // Normalize all priorities to valid range (1-3)
            // <= 1 becomes 1 (High), >= 3 becomes 3 (Low), others stay as is
            var reportsWithInvalidPriority = await db.Report
                .Where(r => r.Priority < 1 || r.Priority > 3)
                .ToListAsync();

            if (reportsWithInvalidPriority.Any())
            {
                foreach (var report in reportsWithInvalidPriority)
                {
                    report.Priority = report.Priority switch
                    {
                        <= 1 => 1,
                        >= 3 => 3,
                        _ => report.Priority
                    };
                }
                await db.SaveChangesAsync();
            }
        }
    }
}

