using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class FreshInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First, drop all existing tables if they exist
            migrationBuilder.Sql(@"
                -- Disable all constraints
                DECLARE @sql NVARCHAR(MAX) = N'';

                -- Drop all foreign key constraints
                SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) +
                               ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
                FROM sys.foreign_keys;
                IF @sql != N'' EXEC sp_executesql @sql;

                -- Drop all tables
                SET @sql = N'';
                SELECT @sql += 'DROP TABLE ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(name) + ';'
                FROM sys.tables
                WHERE name != '__EFMigrationsHistory';
                IF @sql != N'' EXEC sp_executesql @sql;
            ");

            // Ensure dbo schema exists
            migrationBuilder.EnsureSchema(
                name: "dbo");

            // Create Master Tables
            migrationBuilder.CreateTable(
                name: "AttributeDef",
                schema: "dbo",
                columns: table => new
                {
                    AttributeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDef", x => x.AttributeID);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                schema: "dbo",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EquipmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EquipmentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsConfigurable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentID);
                });

            migrationBuilder.CreateTable(
                name: "ItemMaster",
                schema: "dbo",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UOM = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "EA"),
                    DrawingNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMaster", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Process",
                schema: "dbo",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.ProcessID);
                });

            migrationBuilder.CreateTable(
                name: "VerticalArea",
                schema: "dbo",
                columns: table => new
                {
                    VerticalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VerticalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VerticalName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerticalArea", x => x.VerticalID);
                });

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

            // Create dependent tables
            migrationBuilder.CreateTable(
                name: "AttributeListValue",
                schema: "dbo",
                columns: table => new
                {
                    AttributeValueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Display = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeListValue", x => x.AttributeValueID);
                    table.ForeignKey(
                        name: "FK_AttributeListValue_AttributeDef_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "dbo",
                        principalTable: "AttributeDef",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                schema: "dbo",
                columns: table => new
                {
                    SeriesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(type: "int", nullable: false),
                    SeriesCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeriesName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.SeriesID);
                    table.ForeignKey(
                        name: "FK_Series_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentAttribute",
                schema: "dbo",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    AttributeCategory = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentAttribute", x => new { x.EquipmentID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_EquipmentAttribute_AttributeDef_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "dbo",
                        principalTable: "AttributeDef",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentAttribute_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentAttributeValue",
                schema: "dbo",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    NumValue = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    ListValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentAttributeValue", x => new { x.EquipmentID, x.AttributeID, x.SequenceNo });
                    table.ForeignKey(
                        name: "FK_EquipmentAttributeValue_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageRef",
                schema: "dbo",
                columns: table => new
                {
                    ImageRefID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageRef", x => x.ImageRefID);
                });

            migrationBuilder.CreateTable(
                name: "L_Process_Equipment",
                schema: "dbo",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    EquipmentID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L_Process_Equipment", x => new { x.ProcessID, x.EquipmentID });
                    table.ForeignKey(
                        name: "FK_L_Process_Equipment_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_L_Process_Equipment_Process_ProcessID",
                        column: x => x.ProcessID,
                        principalSchema: "dbo",
                        principalTable: "Process",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "L_Vertical_Process",
                schema: "dbo",
                columns: table => new
                {
                    VerticalID = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L_Vertical_Process", x => new { x.VerticalID, x.ProcessID });
                    table.ForeignKey(
                        name: "FK_L_Vertical_Process_Process_ProcessID",
                        column: x => x.ProcessID,
                        principalSchema: "dbo",
                        principalTable: "Process",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_L_Vertical_Process_VerticalArea_VerticalID",
                        column: x => x.VerticalID,
                        principalSchema: "dbo",
                        principalTable: "VerticalArea",
                        principalColumn: "VerticalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                schema: "dbo",
                columns: table => new
                {
                    ModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesID = table.Column<int>(type: "int", nullable: false),
                    ModelCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ModelID);
                    table.ForeignKey(
                        name: "FK_Model_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalSchema: "dbo",
                        principalTable: "Series",
                        principalColumn: "SeriesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesAttribute",
                schema: "dbo",
                columns: table => new
                {
                    SeriesID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesAttribute", x => new { x.SeriesID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_SeriesAttribute_AttributeDef_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "dbo",
                        principalTable: "AttributeDef",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeriesAttribute_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalSchema: "dbo",
                        principalTable: "Series",
                        principalColumn: "SeriesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quote_Vertical",
                schema: "dbo",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteID = table.Column<int>(type: "int", nullable: false),
                    QuoteRevision = table.Column<byte>(type: "tinyint", nullable: false),
                    VerticalID = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    Layer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote_Vertical", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_Quote_Vertical_QuoteHeader_QuoteID_QuoteRevision",
                        columns: x => new { x.QuoteID, x.QuoteRevision },
                        principalSchema: "dbo",
                        principalTable: "QuoteHeader",
                        principalColumns: new[] { "QuoteID", "QuoteRevision" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quote_Vertical_VerticalArea_VerticalID",
                        column: x => x.VerticalID,
                        principalSchema: "dbo",
                        principalTable: "VerticalArea",
                        principalColumn: "VerticalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quote_Vertical_Process_ProcessID",
                        column: x => x.ProcessID,
                        principalSchema: "dbo",
                        principalTable: "Process",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelAttributeValue",
                schema: "dbo",
                columns: table => new
                {
                    ModelID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    NumValue = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    ListValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelAttributeValue", x => new { x.ModelID, x.AttributeID, x.SequenceNo });
                    table.ForeignKey(
                        name: "FK_ModelAttributeValue_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "dbo",
                        principalTable: "Model",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                schema: "dbo",
                columns: table => new
                {
                    PriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    BasePriceINR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.PriceID);
                    table.ForeignKey(
                        name: "FK_Price_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Price_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "dbo",
                        principalTable: "Model",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Price_ItemMaster_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "dbo",
                        principalTable: "ItemMaster",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quote_Equipment_Or_Model",
                schema: "dbo",
                columns: table => new
                {
                    QEOMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    SeriesID = table.Column<int>(type: "int", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    Price_INR = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote_Equipment_Or_Model", x => x.QEOMId);
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Quote_Vertical_RecordID",
                        column: x => x.RecordID,
                        principalSchema: "dbo",
                        principalTable: "Quote_Vertical",
                        principalColumn: "RecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalSchema: "dbo",
                        principalTable: "Series",
                        principalColumn: "SeriesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "dbo",
                        principalTable: "Model",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScopeOfSupply",
                schema: "dbo",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price_INR = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScopeOfSupply", x => new { x.RecordID, x.ItemId });
                    table.ForeignKey(
                        name: "FK_ScopeOfSupply_Quote_Vertical_RecordID",
                        column: x => x.RecordID,
                        principalSchema: "dbo",
                        principalTable: "Quote_Vertical",
                        principalColumn: "RecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScopeOfSupply_ItemMaster_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "dbo",
                        principalTable: "ItemMaster",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecDetails",
                schema: "dbo",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    NumValue = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    ListValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecDetails", x => new { x.RecordID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_SpecDetails_Quote_Vertical_RecordID",
                        column: x => x.RecordID,
                        principalSchema: "dbo",
                        principalTable: "Quote_Vertical",
                        principalColumn: "RecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecDetails_AttributeDef_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "dbo",
                        principalTable: "AttributeDef",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_AttributeDef_AttributeCode",
                schema: "dbo",
                table: "AttributeDef",
                column: "AttributeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_AttrList",
                schema: "dbo",
                table: "AttributeListValue",
                columns: new[] { "AttributeID", "AttributeValue" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeListValue_AttributeID",
                schema: "dbo",
                table: "AttributeListValue",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentCode",
                schema: "dbo",
                table: "Equipment",
                column: "EquipmentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAttribute_AttributeID",
                schema: "dbo",
                table: "EquipmentAttribute",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMaster_ItemCode",
                schema: "dbo",
                table: "ItemMaster",
                column: "ItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_L_Process_Equipment_EquipmentID",
                schema: "dbo",
                table: "L_Process_Equipment",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Vertical_Process_ProcessID",
                schema: "dbo",
                table: "L_Vertical_Process",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_SeriesID",
                schema: "dbo",
                table: "Model",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "UQ_Model",
                schema: "dbo",
                table: "Model",
                columns: new[] { "SeriesID", "ModelCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Price_Equip",
                schema: "dbo",
                table: "Price",
                columns: new[] { "EquipmentID", "EffectiveFrom" },
                unique: true,
                filter: "EquipmentID IS NOT NULL AND ModelID IS NULL AND ItemId IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_Price_Model",
                schema: "dbo",
                table: "Price",
                columns: new[] { "ModelID", "EffectiveFrom" },
                unique: true,
                filter: "ModelID IS NOT NULL AND EquipmentID IS NULL AND ItemId IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_Price_Item",
                schema: "dbo",
                table: "Price",
                columns: new[] { "ItemId", "EffectiveFrom" },
                unique: true,
                filter: "ItemId IS NOT NULL AND EquipmentID IS NULL AND ModelID IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Price_EquipmentID",
                schema: "dbo",
                table: "Price",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Price_ModelID",
                schema: "dbo",
                table: "Price",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Price_ItemId",
                schema: "dbo",
                table: "Price",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Process_ProcessCode",
                schema: "dbo",
                table: "Process",
                column: "ProcessCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_QEOM_EQUIP",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                columns: new[] { "RecordID", "EquipmentID" },
                unique: true,
                filter: "EquipmentID IS NOT NULL AND SeriesID IS NULL AND ModelID IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_QEOM_SERIES",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                columns: new[] { "RecordID", "SeriesID" },
                unique: true,
                filter: "SeriesID IS NOT NULL AND EquipmentID IS NULL AND ModelID IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_QEOM_MODEL",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                columns: new[] { "RecordID", "ModelID" },
                unique: true,
                filter: "ModelID IS NOT NULL AND EquipmentID IS NULL AND SeriesID IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Equipment_Or_Model_RecordID",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                column: "RecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Equipment_Or_Model_EquipmentID",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Equipment_Or_Model_SeriesID",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Equipment_Or_Model_ModelID",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Vertical_QuoteID_QuoteRevision",
                schema: "dbo",
                table: "Quote_Vertical",
                columns: new[] { "QuoteID", "QuoteRevision" });

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Vertical_VerticalID",
                schema: "dbo",
                table: "Quote_Vertical",
                column: "VerticalID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Vertical_ProcessID",
                schema: "dbo",
                table: "Quote_Vertical",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_ScopeOfSupply_ItemId",
                schema: "dbo",
                table: "ScopeOfSupply",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_EquipmentID",
                schema: "dbo",
                table: "Series",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "UQ_Series",
                schema: "dbo",
                table: "Series",
                columns: new[] { "EquipmentID", "SeriesCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeriesAttribute_AttributeID",
                schema: "dbo",
                table: "SeriesAttribute",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_SpecDetails_AttributeID",
                schema: "dbo",
                table: "SpecDetails",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_VerticalArea_VerticalCode",
                schema: "dbo",
                table: "VerticalArea",
                column: "VerticalCode",
                unique: true);

            // Insert seed data
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "VerticalArea",
                columns: new[] { "VerticalID", "VerticalCode", "VerticalName", "Description", "IsActive", "CreatedAt", "CreatedBy" },
                values: new object[,]
                {
                    { 1, "SPT", "Stock Preparation", "Stock Preparation", true, new DateTime(2025, 10, 23), "Yogesh P" },
                    { 2, "PP", "Pulp Preparation", "Pulp Preparation", true, new DateTime(2025, 10, 23), "Yogesh P" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Process",
                columns: new[] { "ProcessID", "ProcessCode", "ProcessName", "Description", "IsActive", "CreatedAt", "CreatedBy" },
                values: new object[,]
                {
                    { 1, "LCP", "Low Consistency Pulping", "Stock Preparation", true, new DateTime(2025, 10, 23), "Yogesh P" },
                    { 2, "HCP", "High Consistency Pulping", "Pulp Preparation", true, new DateTime(2025, 10, 23), "Yogesh P" }
                });

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
            // Drop all tables in reverse order
            migrationBuilder.DropTable(name: "SpecDetails", schema: "dbo");
            migrationBuilder.DropTable(name: "ScopeOfSupply", schema: "dbo");
            migrationBuilder.DropTable(name: "Quote_Equipment_Or_Model", schema: "dbo");
            migrationBuilder.DropTable(name: "Quote_Vertical", schema: "dbo");
            migrationBuilder.DropTable(name: "ModelAttributeValue", schema: "dbo");
            migrationBuilder.DropTable(name: "Price", schema: "dbo");
            migrationBuilder.DropTable(name: "EquipmentAttributeValue", schema: "dbo");
            migrationBuilder.DropTable(name: "SeriesAttribute", schema: "dbo");
            migrationBuilder.DropTable(name: "EquipmentAttribute", schema: "dbo");
            migrationBuilder.DropTable(name: "L_Process_Equipment", schema: "dbo");
            migrationBuilder.DropTable(name: "L_Vertical_Process", schema: "dbo");
            migrationBuilder.DropTable(name: "ImageRef", schema: "dbo");
            migrationBuilder.DropTable(name: "Model", schema: "dbo");
            migrationBuilder.DropTable(name: "Series", schema: "dbo");
            migrationBuilder.DropTable(name: "AttributeListValue", schema: "dbo");
            migrationBuilder.DropTable(name: "QuoteHeader", schema: "dbo");
            migrationBuilder.DropTable(name: "ItemMaster", schema: "dbo");
            migrationBuilder.DropTable(name: "Process", schema: "dbo");
            migrationBuilder.DropTable(name: "VerticalArea", schema: "dbo");
            migrationBuilder.DropTable(name: "AttributeDef", schema: "dbo");
            migrationBuilder.DropTable(name: "Equipment", schema: "dbo");
        }
    }
}

#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
