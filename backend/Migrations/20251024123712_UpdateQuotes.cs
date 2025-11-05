using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "QuoteHeader",
                keyColumns: new[] { "QuoteID", "QuoteRevision" },
                keyValues: new object[] { 1, (byte)1 },
                columns: new[] { "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yogesh Patil" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "QuoteHeader",
                keyColumns: new[] { "QuoteID", "QuoteRevision" },
                keyValues: new object[] { 2, (byte)1 },
                columns: new[] { "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yogesh Patil" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "QuoteHeader",
                keyColumns: new[] { "QuoteID", "QuoteRevision" },
                keyValues: new object[] { 1, (byte)1 },
                columns: new[] { "ModifiedAt", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "QuoteHeader",
                keyColumns: new[] { "QuoteID", "QuoteRevision" },
                keyValues: new object[] { 2, (byte)1 },
                columns: new[] { "ModifiedAt", "ModifiedBy" },
                values: new object[] { null, null });
        }
    }
}
