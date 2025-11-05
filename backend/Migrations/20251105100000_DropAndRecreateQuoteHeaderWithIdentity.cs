using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class DropAndRecreateQuoteHeaderWithIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing QuoteHeader table completely
            migrationBuilder.DropTable(
                name: "QuoteHeader",
                schema: "dbo");

            // Recreate the QuoteHeader table with QuoteID as IDENTITY
            migrationBuilder.CreateTable(
                name: "QuoteHeader",
                schema: "dbo",
                columns: table => new
                {
                    QuoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteRevision = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    QuoteNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    QuoteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Draft"),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, defaultValue: "INR"),
                    ValidityDays = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteHeader", x => new { x.QuoteID, x.QuoteRevision });
                });

            // Re-insert seed data
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "QuoteHeader",
                columns: new[] { "QuoteID", "QuoteRevision", "QuoteNumber", "QuoteName", "CustomerName", "Status", "Currency", "ValidityDays", "Notes", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, (byte)1, "Q-2025-001", "First Demo Quote", "ABC Industries", "Draft", "INR", 30, "Seed sample quote", new DateTime(2025, 1, 1), "System", new DateTime(2025, 1, 1), "Yogesh Patil" },
                    { 2, (byte)1, "Q-2025-002", "Second Demo Quote", "XYZ Manufacturing", "Approved", "USD", 45, "Second seed record", new DateTime(2025, 1, 2), "System", new DateTime(2025, 1, 1), "Yogesh Patil" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the table
            migrationBuilder.DropTable(
                name: "QuoteHeader",
                schema: "dbo");

            // Recreate the table without identity (reverting to previous state)
            migrationBuilder.CreateTable(
                name: "QuoteHeader",
                schema: "dbo",
                columns: table => new
                {
                    QuoteID = table.Column<int>(type: "int", nullable: false),
                    QuoteRevision = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    QuoteNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    QuoteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Draft"),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, defaultValue: "INR"),
                    ValidityDays = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteHeader", x => new { x.QuoteID, x.QuoteRevision });
                });

            // Re-insert seed data
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "QuoteHeader",
                columns: new[] { "QuoteID", "QuoteRevision", "QuoteNumber", "QuoteName", "CustomerName", "Status", "Currency", "ValidityDays", "Notes", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, (byte)1, "Q-2025-001", "First Demo Quote", "ABC Industries", "Draft", "INR", 30, "Seed sample quote", new DateTime(2025, 1, 1), "System", new DateTime(2025, 1, 1), "Yogesh Patil" },
                    { 2, (byte)1, "Q-2025-002", "Second Demo Quote", "XYZ Manufacturing", "Approved", "USD", 45, "Second seed record", new DateTime(2025, 1, 2), "System", new DateTime(2025, 1, 1), "Yogesh Patil" }
                });
        }
    }
}
