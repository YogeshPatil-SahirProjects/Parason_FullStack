using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedQuoteHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "QuoteHeader",
                columns: new[] { "QuoteID", "QuoteRevision", "CreatedAt", "CreatedBy", "Currency", "CustomerName", "ModifiedAt", "ModifiedBy", "Notes", "QuoteName", "QuoteNumber", "Status", "ValidityDays" },
                values: new object[,]
                {
                    { 1, (byte)1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "INR", "ABC Industries", null, null, "Seed sample quote", "First Demo Quote", "Q-2025-001", "Draft", 30 },
                    { 2, (byte)1, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "USD", "XYZ Manufacturing", null, null, "Second seed record", "Second Demo Quote", "Q-2025-002", "Approved", 45 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "QuoteHeader",
                keyColumns: new[] { "QuoteID", "QuoteRevision" },
                keyValues: new object[] { 1, (byte)1 });

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "QuoteHeader",
                keyColumns: new[] { "QuoteID", "QuoteRevision" },
                keyValues: new object[] { 2, (byte)1 });
        }
    }
}
