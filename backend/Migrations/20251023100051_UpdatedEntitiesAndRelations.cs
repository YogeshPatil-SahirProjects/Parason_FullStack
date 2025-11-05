using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntitiesAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeListValues_AttributeDefs_AttributeID",
                table: "AttributeListValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemMasters_Categories_CategoryId",
                table: "ItemMasters");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Equipment_Attribute_AttributeDefs_AttributeID",
                table: "L_Equipment_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Equipment_Attribute_Equipments_EquipmentID",
                table: "L_Equipment_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Process_Equipment_Equipments_EquipmentID",
                table: "L_Process_Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Process_Equipment_Processes_ProcessID",
                table: "L_Process_Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Series_Attribute_AttributeDefs_AttributeID",
                table: "L_Series_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Vertical_Process_Processes_ProcessID",
                table: "L_Vertical_Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Series_SeriesID",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Equipments_EquipmentID",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_ItemMasters_ItemId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Models_ModelId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Scope_Of_Supply_Equipments_EquipmentID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_Scope_Of_Supply_GroupMasters_GroupID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_Scope_Of_Supply_ItemMasters_ItemId",
                table: "Scope_Of_Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_Scope_Of_Supply_Models_ModelId",
                table: "Scope_Of_Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_Scope_Of_Supply_Series_SeriesID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Equipments_EquipmentID",
                table: "Series");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "L_Group_Item");

            migrationBuilder.DropTable(
                name: "L_Model_AttributeValue");

            migrationBuilder.DropTable(
                name: "GroupMasters");

            migrationBuilder.DropIndex(
                name: "IX_Series_Code",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_EquipmentID",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Scope_Of_Supply_EquipmentID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropIndex(
                name: "IX_Scope_Of_Supply_GroupID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropIndex(
                name: "IX_Scope_Of_Supply_SeriesID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processes",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Prices_EquipmentID",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_ItemId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_ModelId",
                table: "Prices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_Code",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_SeriesID",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemMasters",
                table: "ItemMasters");

            migrationBuilder.DropIndex(
                name: "IX_ItemMasters_CategoryId",
                table: "ItemMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeListValues",
                table: "AttributeListValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeListValues_AttributeID",
                table: "AttributeListValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDefs",
                table: "AttributeDefs");

            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "EquipmentID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Scope_Of_Supply");

            migrationBuilder.DropColumn(
                name: "IsMandatory",
                table: "Scope_Of_Supply");

            migrationBuilder.DropColumn(
                name: "SeriesID",
                table: "Scope_Of_Supply");

            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "PriceMarkup",
                table: "ItemMasters");

            migrationBuilder.DropColumn(
                name: "IsConfigurable",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "AttributeDescription",
                table: "AttributeDefs");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Vertical_Area",
                newName: "Vertical_Area",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Series",
                newName: "Series",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "L_Vertical_Process",
                newName: "L_Vertical_Process",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "L_Series_Attribute",
                newName: "L_Series_Attribute",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "L_Process_Equipment",
                newName: "L_Process_Equipment",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "L_Equipment_Attribute",
                newName: "L_Equipment_Attribute",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Scope_Of_Supply",
                newName: "ScopeOfSupply",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Processes",
                newName: "Process",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Prices",
                newName: "Price",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Model",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ItemMasters",
                newName: "ItemMaster",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Equipments",
                newName: "Equipment",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AttributeListValues",
                newName: "AttributeListValue",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AttributeDefs",
                newName: "AttributeDef",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "VerticalDescription",
                schema: "dbo",
                table: "Vertical_Area",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "Vertical_Area",
                newName: "VerticalCode");

            migrationBuilder.RenameIndex(
                name: "IX_Vertical_Area_Code",
                schema: "dbo",
                table: "Vertical_Area",
                newName: "IX_Vertical_Area_VerticalCode");

            migrationBuilder.RenameColumn(
                name: "SeriesDescription",
                schema: "dbo",
                table: "Series",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "Series",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                schema: "dbo",
                table: "ScopeOfSupply",
                newName: "ModelID");

            migrationBuilder.RenameIndex(
                name: "IX_Scope_Of_Supply_ModelId",
                schema: "dbo",
                table: "ScopeOfSupply",
                newName: "IX_ScopeOfSupply_ModelID");

            migrationBuilder.RenameIndex(
                name: "IX_Scope_Of_Supply_ItemId",
                schema: "dbo",
                table: "ScopeOfSupply",
                newName: "IX_ScopeOfSupply_ItemId");

            migrationBuilder.RenameColumn(
                name: "ProcessDescription",
                schema: "dbo",
                table: "Process",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "Process",
                newName: "ProcessCode");

            migrationBuilder.RenameIndex(
                name: "IX_Processes_Code",
                schema: "dbo",
                table: "Process",
                newName: "IX_Process_ProcessCode");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                schema: "dbo",
                table: "Price",
                newName: "ModelID");

            migrationBuilder.RenameColumn(
                name: "ModelDescription",
                schema: "dbo",
                table: "Model",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "Model",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "DrawingNumber",
                schema: "dbo",
                table: "ItemMaster",
                newName: "ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ItemMasters_ItemCode",
                schema: "dbo",
                table: "ItemMaster",
                newName: "IX_ItemMaster_ItemCode");

            migrationBuilder.RenameColumn(
                name: "EquipmentType",
                schema: "dbo",
                table: "Equipment",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "EquipmentDescription",
                schema: "dbo",
                table: "Equipment",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "Equipment",
                newName: "EquipmentCode");

            migrationBuilder.RenameIndex(
                name: "IX_Equipments_Code",
                schema: "dbo",
                table: "Equipment",
                newName: "IX_Equipment_EquipmentCode");

            migrationBuilder.RenameColumn(
                name: "Unit",
                schema: "dbo",
                table: "AttributeDef",
                newName: "UnitDefault");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "AttributeDef",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Vertical_Area",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Vertical_Area",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Vertical_Area",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Vertical_Area",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Vertical_Area",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Series",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Series",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriesCode",
                schema: "dbo",
                table: "Series",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                schema: "dbo",
                table: "ScopeOfSupply",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordID",
                schema: "dbo",
                table: "ScopeOfSupply",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price_INR",
                schema: "dbo",
                table: "ScopeOfSupply",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "dbo",
                table: "ScopeOfSupply",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Process",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Process",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Process",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Process",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Process",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveFrom",
                schema: "dbo",
                table: "Price",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "PriceID",
                schema: "dbo",
                table: "Price",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Price",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Price",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Model",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "ModelCode",
                schema: "dbo",
                table: "Model",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Model",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Model",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                schema: "dbo",
                table: "ItemMaster",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "ItemMaster",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "ItemMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "dbo",
                table: "ItemMaster",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Equipment",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Equipment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Equipment",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Equipment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "dbo",
                table: "AttributeDef",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "AttributeCode",
                schema: "dbo",
                table: "AttributeDef",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "AttributeDef",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "AttributeDef",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "AttributeDef",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "dbo",
                table: "AttributeDef",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "AttributeDef",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScopeOfSupply",
                schema: "dbo",
                table: "ScopeOfSupply",
                columns: new[] { "RecordID", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Process",
                schema: "dbo",
                table: "Process",
                column: "ProcessID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Price",
                schema: "dbo",
                table: "Price",
                column: "PriceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model",
                schema: "dbo",
                table: "Model",
                column: "ModelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemMaster",
                schema: "dbo",
                table: "ItemMaster",
                column: "ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipment",
                schema: "dbo",
                table: "Equipment",
                column: "EquipmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeListValue",
                schema: "dbo",
                table: "AttributeListValue",
                column: "ListValueID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDef",
                schema: "dbo",
                table: "AttributeDef",
                column: "AttributeID");

            migrationBuilder.CreateTable(
                name: "ImageRef",
                schema: "dbo",
                columns: table => new
                {
                    ImageRefID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    SeriesID = table.Column<int>(type: "int", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    ImagePurpose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageRef", x => x.ImageRefID);
                    table.ForeignKey(
                        name: "FK_ImageRef_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID");
                    table.ForeignKey(
                        name: "FK_ImageRef_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "dbo",
                        principalTable: "Model",
                        principalColumn: "ModelID");
                    table.ForeignKey(
                        name: "FK_ImageRef_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalSchema: "dbo",
                        principalTable: "Series",
                        principalColumn: "SeriesID");
                });

            migrationBuilder.CreateTable(
                name: "L_Equipment_AttributeValue",
                schema: "dbo",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    NumValue = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    ListValueID = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L_Equipment_AttributeValue", x => new { x.EquipmentID, x.AttributeID, x.SequenceNo });
                    table.ForeignKey(
                        name: "FK_L_Equipment_AttributeValue_AttributeDef_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "dbo",
                        principalTable: "AttributeDef",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_L_Equipment_AttributeValue_AttributeListValue_ListValueID",
                        column: x => x.ListValueID,
                        principalSchema: "dbo",
                        principalTable: "AttributeListValue",
                        principalColumn: "ListValueID");
                    table.ForeignKey(
                        name: "FK_L_Equipment_AttributeValue_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Quote_Vertical",
                schema: "dbo",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteID = table.Column<int>(type: "int", nullable: false),
                    QuoteRevision = table.Column<byte>(type: "tinyint", nullable: false),
                    Layer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VerticalID = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote_Vertical", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_Quote_Vertical_Process_ProcessID",
                        column: x => x.ProcessID,
                        principalSchema: "dbo",
                        principalTable: "Process",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quote_Vertical_QuoteHeader_QuoteID_QuoteRevision",
                        columns: x => new { x.QuoteID, x.QuoteRevision },
                        principalSchema: "dbo",
                        principalTable: "QuoteHeader",
                        principalColumns: new[] { "QuoteID", "QuoteRevision" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quote_Vertical_Vertical_Area_VerticalID",
                        column: x => x.VerticalID,
                        principalSchema: "dbo",
                        principalTable: "Vertical_Area",
                        principalColumn: "VerticalID",
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
                    Price_INR = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote_Equipment_Or_Model", x => x.QEOMId);
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID");
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "dbo",
                        principalTable: "Model",
                        principalColumn: "ModelID");
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Quote_Vertical_RecordID",
                        column: x => x.RecordID,
                        principalSchema: "dbo",
                        principalTable: "Quote_Vertical",
                        principalColumn: "RecordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quote_Equipment_Or_Model_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalSchema: "dbo",
                        principalTable: "Series",
                        principalColumn: "SeriesID");
                });

            migrationBuilder.CreateTable(
                name: "Spec_Details",
                schema: "dbo",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    ListValueID = table.Column<int>(type: "int", nullable: true),
                    NumValue = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spec_Details", x => new { x.RecordID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_Spec_Details_AttributeDef_AttributeID",
                        column: x => x.AttributeID,
                        principalSchema: "dbo",
                        principalTable: "AttributeDef",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spec_Details_AttributeListValue_ListValueID",
                        column: x => x.ListValueID,
                        principalSchema: "dbo",
                        principalTable: "AttributeListValue",
                        principalColumn: "ListValueID");
                    table.ForeignKey(
                        name: "FK_Spec_Details_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalSchema: "dbo",
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID");
                    table.ForeignKey(
                        name: "FK_Spec_Details_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "dbo",
                        principalTable: "Model",
                        principalColumn: "ModelID");
                    table.ForeignKey(
                        name: "FK_Spec_Details_Quote_Vertical_RecordID",
                        column: x => x.RecordID,
                        principalSchema: "dbo",
                        principalTable: "Quote_Vertical",
                        principalColumn: "RecordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Series",
                schema: "dbo",
                table: "Series",
                columns: new[] { "EquipmentID", "SeriesCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Price_Equip",
                schema: "dbo",
                table: "Price",
                columns: new[] { "EquipmentID", "EffectiveFrom" },
                unique: true,
                filter: "EquipmentID IS NOT NULL AND ModelID IS NULL AND ItemId IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_Price_Item",
                schema: "dbo",
                table: "Price",
                columns: new[] { "ItemId", "EffectiveFrom" },
                unique: true,
                filter: "ItemId IS NOT NULL AND EquipmentID IS NULL AND ModelID IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_Price_Model",
                schema: "dbo",
                table: "Price",
                columns: new[] { "ModelID", "EffectiveFrom" },
                unique: true,
                filter: "ModelID IS NOT NULL AND EquipmentID IS NULL AND ItemId IS NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Model",
                schema: "dbo",
                table: "Model",
                columns: new[] { "SeriesID", "ModelCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_AttrList",
                schema: "dbo",
                table: "AttributeListValue",
                columns: new[] { "AttributeID", "AttributeValue" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDef_AttributeCode",
                schema: "dbo",
                table: "AttributeDef",
                column: "AttributeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageRef_EquipmentID",
                schema: "dbo",
                table: "ImageRef",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageRef_ModelID",
                schema: "dbo",
                table: "ImageRef",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageRef_SeriesID",
                schema: "dbo",
                table: "ImageRef",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Equipment_AttributeValue_AttributeID",
                schema: "dbo",
                table: "L_Equipment_AttributeValue",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Equipment_AttributeValue_ListValueID",
                schema: "dbo",
                table: "L_Equipment_AttributeValue",
                column: "ListValueID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Equipment_Or_Model_EquipmentID",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Equipment_Or_Model_ModelID",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Equipment_Or_Model_SeriesID",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "UX_QEOM_EQUIP",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                columns: new[] { "RecordID", "EquipmentID" },
                unique: true,
                filter: "EquipmentID IS NOT NULL AND SeriesID IS NULL AND ModelID IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_QEOM_MODEL",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                columns: new[] { "RecordID", "ModelID" },
                unique: true,
                filter: "ModelID IS NOT NULL AND EquipmentID IS NULL AND SeriesID IS NULL");

            migrationBuilder.CreateIndex(
                name: "UX_QEOM_SERIES",
                schema: "dbo",
                table: "Quote_Equipment_Or_Model",
                columns: new[] { "RecordID", "SeriesID" },
                unique: true,
                filter: "SeriesID IS NOT NULL AND EquipmentID IS NULL AND ModelID IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_Vertical_ProcessID",
                schema: "dbo",
                table: "Quote_Vertical",
                column: "ProcessID");

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
                name: "IX_Spec_Details_AttributeID",
                schema: "dbo",
                table: "Spec_Details",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Spec_Details_EquipmentID",
                schema: "dbo",
                table: "Spec_Details",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Spec_Details_ListValueID",
                schema: "dbo",
                table: "Spec_Details",
                column: "ListValueID");

            migrationBuilder.CreateIndex(
                name: "IX_Spec_Details_ModelID",
                schema: "dbo",
                table: "Spec_Details",
                column: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeListValue_AttributeDef_AttributeID",
                schema: "dbo",
                table: "AttributeListValue",
                column: "AttributeID",
                principalSchema: "dbo",
                principalTable: "AttributeDef",
                principalColumn: "AttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Equipment_Attribute_AttributeDef_AttributeID",
                schema: "dbo",
                table: "L_Equipment_Attribute",
                column: "AttributeID",
                principalSchema: "dbo",
                principalTable: "AttributeDef",
                principalColumn: "AttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Equipment_Attribute_Equipment_EquipmentID",
                schema: "dbo",
                table: "L_Equipment_Attribute",
                column: "EquipmentID",
                principalSchema: "dbo",
                principalTable: "Equipment",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Process_Equipment_Equipment_EquipmentID",
                schema: "dbo",
                table: "L_Process_Equipment",
                column: "EquipmentID",
                principalSchema: "dbo",
                principalTable: "Equipment",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Process_Equipment_Process_ProcessID",
                schema: "dbo",
                table: "L_Process_Equipment",
                column: "ProcessID",
                principalSchema: "dbo",
                principalTable: "Process",
                principalColumn: "ProcessID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Series_Attribute_AttributeDef_AttributeID",
                schema: "dbo",
                table: "L_Series_Attribute",
                column: "AttributeID",
                principalSchema: "dbo",
                principalTable: "AttributeDef",
                principalColumn: "AttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Vertical_Process_Process_ProcessID",
                schema: "dbo",
                table: "L_Vertical_Process",
                column: "ProcessID",
                principalSchema: "dbo",
                principalTable: "Process",
                principalColumn: "ProcessID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Series_SeriesID",
                schema: "dbo",
                table: "Model",
                column: "SeriesID",
                principalSchema: "dbo",
                principalTable: "Series",
                principalColumn: "SeriesID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Equipment_EquipmentID",
                schema: "dbo",
                table: "Price",
                column: "EquipmentID",
                principalSchema: "dbo",
                principalTable: "Equipment",
                principalColumn: "EquipmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_ItemMaster_ItemId",
                schema: "dbo",
                table: "Price",
                column: "ItemId",
                principalSchema: "dbo",
                principalTable: "ItemMaster",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Model_ModelID",
                schema: "dbo",
                table: "Price",
                column: "ModelID",
                principalSchema: "dbo",
                principalTable: "Model",
                principalColumn: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_ScopeOfSupply_ItemMaster_ItemId",
                schema: "dbo",
                table: "ScopeOfSupply",
                column: "ItemId",
                principalSchema: "dbo",
                principalTable: "ItemMaster",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScopeOfSupply_Model_ModelID",
                schema: "dbo",
                table: "ScopeOfSupply",
                column: "ModelID",
                principalSchema: "dbo",
                principalTable: "Model",
                principalColumn: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_ScopeOfSupply_Quote_Vertical_RecordID",
                schema: "dbo",
                table: "ScopeOfSupply",
                column: "RecordID",
                principalSchema: "dbo",
                principalTable: "Quote_Vertical",
                principalColumn: "RecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Equipment_EquipmentID",
                schema: "dbo",
                table: "Series",
                column: "EquipmentID",
                principalSchema: "dbo",
                principalTable: "Equipment",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeListValue_AttributeDef_AttributeID",
                schema: "dbo",
                table: "AttributeListValue");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Equipment_Attribute_AttributeDef_AttributeID",
                schema: "dbo",
                table: "L_Equipment_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Equipment_Attribute_Equipment_EquipmentID",
                schema: "dbo",
                table: "L_Equipment_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Process_Equipment_Equipment_EquipmentID",
                schema: "dbo",
                table: "L_Process_Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Process_Equipment_Process_ProcessID",
                schema: "dbo",
                table: "L_Process_Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Series_Attribute_AttributeDef_AttributeID",
                schema: "dbo",
                table: "L_Series_Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_L_Vertical_Process_Process_ProcessID",
                schema: "dbo",
                table: "L_Vertical_Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_Series_SeriesID",
                schema: "dbo",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_Equipment_EquipmentID",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_ItemMaster_ItemId",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_Model_ModelID",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_ScopeOfSupply_ItemMaster_ItemId",
                schema: "dbo",
                table: "ScopeOfSupply");

            migrationBuilder.DropForeignKey(
                name: "FK_ScopeOfSupply_Model_ModelID",
                schema: "dbo",
                table: "ScopeOfSupply");

            migrationBuilder.DropForeignKey(
                name: "FK_ScopeOfSupply_Quote_Vertical_RecordID",
                schema: "dbo",
                table: "ScopeOfSupply");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Equipment_EquipmentID",
                schema: "dbo",
                table: "Series");

            migrationBuilder.DropTable(
                name: "ImageRef",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "L_Equipment_AttributeValue",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Quote_Equipment_Or_Model",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Spec_Details",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Quote_Vertical",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "QuoteHeader",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "UQ_Series",
                schema: "dbo",
                table: "Series");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScopeOfSupply",
                schema: "dbo",
                table: "ScopeOfSupply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Process",
                schema: "dbo",
                table: "Process");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Price",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropIndex(
                name: "UX_Price_Equip",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropIndex(
                name: "UX_Price_Item",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropIndex(
                name: "UX_Price_Model",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model",
                schema: "dbo",
                table: "Model");

            migrationBuilder.DropIndex(
                name: "UQ_Model",
                schema: "dbo",
                table: "Model");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemMaster",
                schema: "dbo",
                table: "ItemMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipment",
                schema: "dbo",
                table: "Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeListValue",
                schema: "dbo",
                table: "AttributeListValue");

            migrationBuilder.DropIndex(
                name: "UQ_AttrList",
                schema: "dbo",
                table: "AttributeListValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDef",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.DropIndex(
                name: "IX_AttributeDef_AttributeCode",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Vertical_Area");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "Vertical_Area");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Vertical_Area");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Vertical_Area");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "SeriesCode",
                schema: "dbo",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "RecordID",
                schema: "dbo",
                table: "ScopeOfSupply");

            migrationBuilder.DropColumn(
                name: "Price_INR",
                schema: "dbo",
                table: "ScopeOfSupply");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "dbo",
                table: "ScopeOfSupply");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "PriceID",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "Price");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "ModelCode",
                schema: "dbo",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "ItemMaster");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "ItemMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "dbo",
                table: "ItemMaster");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "dbo",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "AttributeCode",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "AttributeDef");

            migrationBuilder.RenameTable(
                name: "Vertical_Area",
                schema: "dbo",
                newName: "Vertical_Area");

            migrationBuilder.RenameTable(
                name: "Series",
                schema: "dbo",
                newName: "Series");

            migrationBuilder.RenameTable(
                name: "L_Vertical_Process",
                schema: "dbo",
                newName: "L_Vertical_Process");

            migrationBuilder.RenameTable(
                name: "L_Series_Attribute",
                schema: "dbo",
                newName: "L_Series_Attribute");

            migrationBuilder.RenameTable(
                name: "L_Process_Equipment",
                schema: "dbo",
                newName: "L_Process_Equipment");

            migrationBuilder.RenameTable(
                name: "L_Equipment_Attribute",
                schema: "dbo",
                newName: "L_Equipment_Attribute");

            migrationBuilder.RenameTable(
                name: "ScopeOfSupply",
                schema: "dbo",
                newName: "Scope_Of_Supply");

            migrationBuilder.RenameTable(
                name: "Process",
                schema: "dbo",
                newName: "Processes");

            migrationBuilder.RenameTable(
                name: "Price",
                schema: "dbo",
                newName: "Prices");

            migrationBuilder.RenameTable(
                name: "Model",
                schema: "dbo",
                newName: "Models");

            migrationBuilder.RenameTable(
                name: "ItemMaster",
                schema: "dbo",
                newName: "ItemMasters");

            migrationBuilder.RenameTable(
                name: "Equipment",
                schema: "dbo",
                newName: "Equipments");

            migrationBuilder.RenameTable(
                name: "AttributeListValue",
                schema: "dbo",
                newName: "AttributeListValues");

            migrationBuilder.RenameTable(
                name: "AttributeDef",
                schema: "dbo",
                newName: "AttributeDefs");

            migrationBuilder.RenameColumn(
                name: "VerticalCode",
                table: "Vertical_Area",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Vertical_Area",
                newName: "VerticalDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Vertical_Area_VerticalCode",
                table: "Vertical_Area",
                newName: "IX_Vertical_Area_Code");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Series",
                newName: "SeriesDescription");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Series",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ModelID",
                table: "Scope_Of_Supply",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ScopeOfSupply_ModelID",
                table: "Scope_Of_Supply",
                newName: "IX_Scope_Of_Supply_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ScopeOfSupply_ItemId",
                table: "Scope_Of_Supply",
                newName: "IX_Scope_Of_Supply_ItemId");

            migrationBuilder.RenameColumn(
                name: "ProcessCode",
                table: "Processes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Processes",
                newName: "ProcessDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Process_ProcessCode",
                table: "Processes",
                newName: "IX_Processes_Code");

            migrationBuilder.RenameColumn(
                name: "ModelID",
                table: "Prices",
                newName: "ModelId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Models",
                newName: "ModelDescription");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Models",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ItemMasters",
                newName: "DrawingNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ItemMaster_ItemCode",
                table: "ItemMasters",
                newName: "IX_ItemMasters_ItemCode");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Equipments",
                newName: "EquipmentType");

            migrationBuilder.RenameColumn(
                name: "EquipmentCode",
                table: "Equipments",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Equipments",
                newName: "EquipmentDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Equipment_EquipmentCode",
                table: "Equipments",
                newName: "IX_Equipments_Code");

            migrationBuilder.RenameColumn(
                name: "UnitDefault",
                table: "AttributeDefs",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "AttributeDefs",
                newName: "Code");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Vertical_Area",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "SequenceNo",
                table: "Series",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Scope_Of_Supply",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentID",
                table: "Scope_Of_Supply",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Scope_Of_Supply",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Scope_Of_Supply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMandatory",
                table: "Scope_Of_Supply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SeriesID",
                table: "Scope_Of_Supply",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Processes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveFrom",
                table: "Prices",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<int>(
                name: "SequenceNo",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "ItemMasters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                table: "ItemMasters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "ItemMasters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "ItemMasters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemMasters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ItemMasters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "ItemMasters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "ItemMasters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceMarkup",
                table: "ItemMasters",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfigurable",
                table: "Equipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                table: "AttributeDefs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "AttributeDescription",
                table: "AttributeDefs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processes",
                table: "Processes",
                column: "ProcessID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "ModelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemMasters",
                table: "ItemMasters",
                column: "ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments",
                column: "EquipmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeListValues",
                table: "AttributeListValues",
                column: "ListValueID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDefs",
                table: "AttributeDefs",
                column: "AttributeID");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentCategoryId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "GroupMasters",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMasters", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "L_Model_AttributeValue",
                columns: table => new
                {
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    ListValueID = table.Column<int>(type: "int", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: false),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    NumValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_L_Model_AttributeValue_AttributeDefs_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "AttributeDefs",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_L_Model_AttributeValue_AttributeListValues_ListValueID",
                        column: x => x.ListValueID,
                        principalTable: "AttributeListValues",
                        principalColumn: "ListValueID");
                    table.ForeignKey(
                        name: "FK_L_Model_AttributeValue_Models_ModelID",
                        column: x => x.ModelID,
                        principalTable: "Models",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "L_Group_Item",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L_Group_Item", x => new { x.GroupID, x.ItemID });
                    table.ForeignKey(
                        name: "FK_L_Group_Item_GroupMasters_GroupID",
                        column: x => x.GroupID,
                        principalTable: "GroupMasters",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_L_Group_Item_ItemMasters_ItemID",
                        column: x => x.ItemID,
                        principalTable: "ItemMasters",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Series_Code",
                table: "Series",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Series_EquipmentID",
                table: "Series",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_EquipmentID",
                table: "Scope_Of_Supply",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_GroupID",
                table: "Scope_Of_Supply",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_SeriesID",
                table: "Scope_Of_Supply",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_EquipmentID",
                table: "Prices",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ItemId",
                table: "Prices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ModelId",
                table: "Prices",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_Code",
                table: "Models",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_SeriesID",
                table: "Models",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_CategoryId",
                table: "ItemMasters",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeListValues_AttributeID",
                table: "AttributeListValues",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_L_Group_Item_ItemID",
                table: "L_Group_Item",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Model_AttributeValue_AttributeID",
                table: "L_Model_AttributeValue",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Model_AttributeValue_ListValueID",
                table: "L_Model_AttributeValue",
                column: "ListValueID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Model_AttributeValue_ModelID",
                table: "L_Model_AttributeValue",
                column: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeListValues_AttributeDefs_AttributeID",
                table: "AttributeListValues",
                column: "AttributeID",
                principalTable: "AttributeDefs",
                principalColumn: "AttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemMasters_Categories_CategoryId",
                table: "ItemMasters",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_L_Equipment_Attribute_AttributeDefs_AttributeID",
                table: "L_Equipment_Attribute",
                column: "AttributeID",
                principalTable: "AttributeDefs",
                principalColumn: "AttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Equipment_Attribute_Equipments_EquipmentID",
                table: "L_Equipment_Attribute",
                column: "EquipmentID",
                principalTable: "Equipments",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Process_Equipment_Equipments_EquipmentID",
                table: "L_Process_Equipment",
                column: "EquipmentID",
                principalTable: "Equipments",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Process_Equipment_Processes_ProcessID",
                table: "L_Process_Equipment",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ProcessID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Series_Attribute_AttributeDefs_AttributeID",
                table: "L_Series_Attribute",
                column: "AttributeID",
                principalTable: "AttributeDefs",
                principalColumn: "AttributeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_L_Vertical_Process_Processes_ProcessID",
                table: "L_Vertical_Process",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ProcessID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Series_SeriesID",
                table: "Models",
                column: "SeriesID",
                principalTable: "Series",
                principalColumn: "SeriesID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Equipments_EquipmentID",
                table: "Prices",
                column: "EquipmentID",
                principalTable: "Equipments",
                principalColumn: "EquipmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_ItemMasters_ItemId",
                table: "Prices",
                column: "ItemId",
                principalTable: "ItemMasters",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Models_ModelId",
                table: "Prices",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Scope_Of_Supply_Equipments_EquipmentID",
                table: "Scope_Of_Supply",
                column: "EquipmentID",
                principalTable: "Equipments",
                principalColumn: "EquipmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Scope_Of_Supply_GroupMasters_GroupID",
                table: "Scope_Of_Supply",
                column: "GroupID",
                principalTable: "GroupMasters",
                principalColumn: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Scope_Of_Supply_ItemMasters_ItemId",
                table: "Scope_Of_Supply",
                column: "ItemId",
                principalTable: "ItemMasters",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scope_Of_Supply_Models_ModelId",
                table: "Scope_Of_Supply",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Scope_Of_Supply_Series_SeriesID",
                table: "Scope_Of_Supply",
                column: "SeriesID",
                principalTable: "Series",
                principalColumn: "SeriesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Equipments_EquipmentID",
                table: "Series",
                column: "EquipmentID",
                principalTable: "Equipments",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
