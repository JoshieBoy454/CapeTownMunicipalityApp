///-----------------------------------Start of File---------------------------------->
using Microsoft.EntityFrameworkCore;
namespace CapeTownMunicipalityApp.Models
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        
        public DbSet<Report> Report { get; set; } 
        public DbSet<ReportAttatchment> ReportAttatchment { get; set; } 
        ///------------------------------------------------------------------------>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable("Report");
            modelBuilder.Entity<ReportAttatchment>().ToTable("ReportAttatchment");

            modelBuilder.Entity<Report>()
                .HasMany(r => r.Attatchments) 
                .WithOne() 
                .HasForeignKey("ReportId") 
                .OnDelete(DeleteBehavior.Cascade); 
        }
        ///------------------------------------------------------------------------>
    }
}
///-----------------------------------End of File----------------------------------->