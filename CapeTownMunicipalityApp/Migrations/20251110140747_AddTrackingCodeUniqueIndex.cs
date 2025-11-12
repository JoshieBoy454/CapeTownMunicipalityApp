using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapeTownMunicipalityApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackingCodeUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Backfill unique TrackingCode values for existing rows that may be empty
            // Use a random suffix from hex substrings of a GUID to ensure uniqueness
            migrationBuilder.Sql(@"
                UPDATE Report
                SET TrackingCode = 
                    'SR-' || substr(hex(randomblob(8)),1,4) || '-' || substr(hex(randomblob(8)),5,8)
                WHERE ifnull(TrackingCode,'') = '';
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Report_TrackingCode",
                table: "Report",
                column: "TrackingCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Report_TrackingCode",
                table: "Report");
        }
    }
}
