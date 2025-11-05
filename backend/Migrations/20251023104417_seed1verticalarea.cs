using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class seed1verticalarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Vertical_Area",
                keyColumn: "VerticalID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Vertical_Area",
                keyColumn: "VerticalID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Vertical_Area",
                keyColumn: "VerticalID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 23, 16, 4, 27, 281, DateTimeKind.Local).AddTicks(2208));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Vertical_Area",
                keyColumn: "VerticalID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 23, 16, 4, 27, 282, DateTimeKind.Local).AddTicks(7334));
        }
    }
}
