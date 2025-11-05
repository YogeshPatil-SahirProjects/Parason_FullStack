using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureQuoteIdAsIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First, we need to drop the existing seed data to avoid conflicts
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

            // Drop and recreate the table with QuoteID as IDENTITY
            // We can't alter a column to be identity in SQL Server, so we need to recreate the table

            // 1. Create a temporary table with the correct schema
            migrationBuilder.Sql(@"
                CREATE TABLE [dbo].[QuoteHeader_Temp] (
                    [QuoteID] int IDENTITY(1,1) NOT NULL,
                    [QuoteRevision] tinyint NOT NULL DEFAULT 0,
                    [QuoteNumber] nvarchar(30) NOT NULL,
                    [QuoteName] nvarchar(100) NOT NULL,
                    [CustomerName] nvarchar(100) NOT NULL,
                    [Status] nvarchar(30) NOT NULL DEFAULT 'Draft',
                    [Currency] nvarchar(3) NOT NULL DEFAULT 'INR',
                    [ValidityDays] int NOT NULL DEFAULT 30,
                    [Notes] nvarchar(2000) NULL,
                    [CreatedAt] datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
                    [CreatedBy] nvarchar(100) NOT NULL,
                    [ModifiedAt] datetime2 NULL,
                    [ModifiedBy] nvarchar(100) NULL,
                    CONSTRAINT [PK_QuoteHeader] PRIMARY KEY ([QuoteID], [QuoteRevision])
                );
            ");

            // 2. Copy existing data (if any) - but skip the seed data we deleted
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[QuoteHeader_Temp] ON;

                IF EXISTS (SELECT 1 FROM [dbo].[QuoteHeader])
                BEGIN
                    INSERT INTO [dbo].[QuoteHeader_Temp]
                        ([QuoteID], [QuoteRevision], [QuoteNumber], [QuoteName], [CustomerName],
                         [Status], [Currency], [ValidityDays], [Notes], [CreatedAt], [CreatedBy],
                         [ModifiedAt], [ModifiedBy])
                    SELECT
                        [QuoteID], [QuoteRevision], [QuoteNumber], [QuoteName], [CustomerName],
                        [Status], [Currency], [ValidityDays], [Notes], [CreatedAt], [CreatedBy],
                        [ModifiedAt], [ModifiedBy]
                    FROM [dbo].[QuoteHeader];
                END

                SET IDENTITY_INSERT [dbo].[QuoteHeader_Temp] OFF;
            ");

            // 3. Drop the old table
            migrationBuilder.DropTable(
                name: "QuoteHeader",
                schema: "dbo");

            // 4. Rename the temp table
            migrationBuilder.Sql(@"
                EXEC sp_rename '[dbo].[QuoteHeader_Temp]', 'QuoteHeader';
            ");

            // 5. Re-insert seed data
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
            // Delete the seed data
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

            // Revert back to non-identity QuoteID
            migrationBuilder.Sql(@"
                CREATE TABLE [dbo].[QuoteHeader_Temp] (
                    [QuoteID] int NOT NULL,
                    [QuoteRevision] tinyint NOT NULL DEFAULT 0,
                    [QuoteNumber] nvarchar(30) NOT NULL,
                    [QuoteName] nvarchar(100) NOT NULL,
                    [CustomerName] nvarchar(100) NOT NULL,
                    [Status] nvarchar(30) NOT NULL DEFAULT 'Draft',
                    [Currency] nvarchar(3) NOT NULL DEFAULT 'INR',
                    [ValidityDays] int NOT NULL DEFAULT 30,
                    [Notes] nvarchar(2000) NULL,
                    [CreatedAt] datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
                    [CreatedBy] nvarchar(100) NOT NULL,
                    [ModifiedAt] datetime2 NULL,
                    [ModifiedBy] nvarchar(100) NULL,
                    CONSTRAINT [PK_QuoteHeader] PRIMARY KEY ([QuoteID], [QuoteRevision])
                );
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM [dbo].[QuoteHeader])
                BEGIN
                    INSERT INTO [dbo].[QuoteHeader_Temp]
                        ([QuoteID], [QuoteRevision], [QuoteNumber], [QuoteName], [CustomerName],
                         [Status], [Currency], [ValidityDays], [Notes], [CreatedAt], [CreatedBy],
                         [ModifiedAt], [ModifiedBy])
                    SELECT
                        [QuoteID], [QuoteRevision], [QuoteNumber], [QuoteName], [CustomerName],
                        [Status], [Currency], [ValidityDays], [Notes], [CreatedAt], [CreatedBy],
                        [ModifiedAt], [ModifiedBy]
                    FROM [dbo].[QuoteHeader];
                END
            ");

            migrationBuilder.DropTable(
                name: "QuoteHeader",
                schema: "dbo");

            migrationBuilder.Sql(@"
                EXEC sp_rename '[dbo].[QuoteHeader_Temp]', 'QuoteHeader';
            ");

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
