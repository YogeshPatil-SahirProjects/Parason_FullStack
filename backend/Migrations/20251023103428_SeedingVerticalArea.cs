using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedingVerticalArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Vertical_Area",
                columns: new[] { "VerticalID", "CreatedAt", "CreatedBy", "Description", "IsActive", "ModifiedAt", "ModifiedBy", "VerticalCode", "VerticalName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 23), "Yogesh P", "Stock Preparation", true, null, null, "SPT", "Stock Preparation" },
                    { 2, new DateTime(2025, 10, 23), "Yogesh P", "Pulp Preparation", true, null, null, "PP", "Pulp Preparation" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Vertical_Area",
                keyColumn: "VerticalID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Vertical_Area",
                keyColumn: "VerticalID",
                keyValue: 2);
        }
    }
}
