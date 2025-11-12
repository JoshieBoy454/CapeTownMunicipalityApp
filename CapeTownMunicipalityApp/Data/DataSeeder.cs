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
            ("Cape Town CBD - Adderley Street", ReportCategory.Utilities, "Burst water pipe causing flooding near bus stop.", 1, ReportStatus.InProgress),
            ("Bellville - Durban Road", ReportCategory.Roads, "Deep potholes slowing traffic near the taxi rank.", 2, ReportStatus.Submitted),
            ("Khayelitsha - Spine Road", ReportCategory.Sanitation, "Blocked stormwater drain causing standing water.", 2, ReportStatus.InProgress),
            ("Sea Point - Regent Road", ReportCategory.Utilities, "Street lights flickering intermittently.", 1, ReportStatus.Resolved),
            ("Mitchells Plain - Westgate Mall", ReportCategory.Other, "Broken public bench outside main entrance.", 4, ReportStatus.Submitted),
            ("Claremont - Main Road", ReportCategory.Roads, "Damaged pedestrian crossing markings need repainting.", 3, ReportStatus.OnHold),
            ("Somerset West - Lourensford Road", ReportCategory.Sanitation, "Overflowing waste bins after weekend market.", 2, ReportStatus.Closed),
            ("Woodstock - Albert Road", ReportCategory.Utilities, "Low water pressure reported by several households.", 1, ReportStatus.Acknowledged),
            ("Durbanville - Wellington Road", ReportCategory.Roads, "Traffic signal timing issue causing congestion.", 3, ReportStatus.InProgress),
            ("Muizenberg - Beachfront", ReportCategory.Sanitation, "Public toilets require urgent cleaning.", 2, ReportStatus.Resolved),
            ("Harfield Village - 2nd Avenue", ReportCategory.Other, "Graffiti on community notice board.", 4, ReportStatus.Submitted),
            ("Observatory - Lower Main Road", ReportCategory.Utilities, "Power outage affecting small businesses.", 1, ReportStatus.Submitted),
            ("Hout Bay - Victoria Avenue", ReportCategory.Roads, "Rockfall debris partially blocking roadway.", 3, ReportStatus.Closed),
            ("Green Point - Somerset Road", ReportCategory.Sanitation, "Sewage odour reported near restaurant strip.", 2, ReportStatus.InProgress),
            ("Mowbray - Liesbeek Parkway", ReportCategory.Utilities, "Stormwater grate stolen exposing open hole.", 1, ReportStatus.OnHold),
            ("Gugulethu - NY1", ReportCategory.Other, "Community centre notice board requires update.", 5, ReportStatus.Acknowledged),
            ("Fish Hoek - Main Road", ReportCategory.Roads, "Sinkhole forming near pedestrian walkway.", 2, ReportStatus.Submitted),
            ("Parow - Voortrekker Road", ReportCategory.Sanitation, "Illegal dumping piling up behind shops.", 3, ReportStatus.Resolved),
            ("Atlantis - Reygersdal Drive", ReportCategory.Utilities, "Water treatment plant alarm triggered overnight.", 2, ReportStatus.Submitted),
            ("Bloubergstrand - Marine Circle", ReportCategory.Other, "Damaged signage at beachfront car park.", 4, ReportStatus.InProgress)
        };

        public static async Task EnsureSeedDataAsync(LocalDbContext db)
        {
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
                    UpdatedAt = seed.Status == ReportStatus.Submitted ? (DateTime?)null : createdAt.AddHours(4)
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
    }
}

