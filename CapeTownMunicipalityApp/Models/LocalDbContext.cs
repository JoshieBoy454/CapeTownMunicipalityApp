using Microsoft.EntityFrameworkCore;
namespace CapeTownMunicipalityApp.Models
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportAttatchment> ReportAttatchment { get; set; }
    }
}
