using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapeTownMunicipalityApp.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceRequestFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Report",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Report",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Report",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrackingCode",
                table: "Report",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Report",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "TrackingCode",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Report");
        }
    }
}
