using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedProcessesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Process",
                columns: new[] { "ProcessID", "CreatedAt", "CreatedBy", "Description", "IsActive", "ModifiedAt", "ModifiedBy", "ProcessCode", "ProcessName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yogesh P", "Stock Preparation", true, null, null, "LCP", "Low Consistency Pulping" },
                    { 2, new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yogesh P", "Pulp Preparation", true, null, null, "HCP", "High Consistency Pulping" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Process",
                keyColumn: "ProcessID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Process",
                keyColumn: "ProcessID",
                keyValue: 2);
        }
    }
}
