using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parason_Api.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeDefs",
                columns: table => new
                {
                    AttributeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AttributeDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDefs", x => x.AttributeID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                name: "Equipments",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EquipmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EquipmentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EquipmentDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsConfigurable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EquipmentID);
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
                name: "Processes",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProcessDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.ProcessID);
                });

            migrationBuilder.CreateTable(
                name: "Vertical_Area",
                columns: table => new
                {
                    VerticalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VerticalName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VerticalDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vertical_Area", x => x.VerticalID);
                });

            migrationBuilder.CreateTable(
                name: "AttributeListValues",
                columns: table => new
                {
                    ListValueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Display = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeListValues", x => x.ListValueID);
                    table.ForeignKey(
                        name: "FK_AttributeListValues_AttributeDefs_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "AttributeDefs",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemMasters",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UOM = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DrawingNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceMarkup = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMasters", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ItemMasters_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "L_Equipment_Attribute",
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
                    table.PrimaryKey("PK_L_Equipment_Attribute", x => new { x.EquipmentID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_L_Equipment_Attribute_AttributeDefs_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "AttributeDefs",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_L_Equipment_Attribute_Equipments_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    SeriesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeriesName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SeriesDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.SeriesID);
                    table.ForeignKey(
                        name: "FK_Series_Equipments_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "L_Process_Equipment",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    EquipmentID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L_Process_Equipment", x => new { x.ProcessID, x.EquipmentID });
                    table.ForeignKey(
                        name: "FK_L_Process_Equipment_Equipments_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_L_Process_Equipment_Processes_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "Processes",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "L_Vertical_Process",
                columns: table => new
                {
                    VerticalID = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L_Vertical_Process", x => new { x.VerticalID, x.ProcessID });
                    table.ForeignKey(
                        name: "FK_L_Vertical_Process_Processes_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "Processes",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_L_Vertical_Process_Vertical_Area_VerticalID",
                        column: x => x.VerticalID,
                        principalTable: "Vertical_Area",
                        principalColumn: "VerticalID",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "L_Series_Attribute",
                columns: table => new
                {
                    SeriesID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    AttributeCategory = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L_Series_Attribute", x => new { x.SeriesID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_L_Series_Attribute_AttributeDefs_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "AttributeDefs",
                        principalColumn: "AttributeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_L_Series_Attribute_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalTable: "Series",
                        principalColumn: "SeriesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModelDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ModelID);
                    table.ForeignKey(
                        name: "FK_Models_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalTable: "Series",
                        principalColumn: "SeriesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "L_Model_AttributeValue",
                columns: table => new
                {
                    ModelID = table.Column<int>(type: "int", nullable: false),
                    AttributeID = table.Column<int>(type: "int", nullable: false),
                    NumValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    ListValueID = table.Column<int>(type: "int", nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Prices",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    BasePriceINR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Prices_Equipments_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentID");
                    table.ForeignKey(
                        name: "FK_Prices_ItemMasters_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ItemMasters",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_Prices_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelID");
                });

            migrationBuilder.CreateTable(
                name: "Scope_Of_Supply",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    SeriesID = table.Column<int>(type: "int", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    GroupID = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Scope_Of_Supply_Equipments_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentID");
                    table.ForeignKey(
                        name: "FK_Scope_Of_Supply_GroupMasters_GroupID",
                        column: x => x.GroupID,
                        principalTable: "GroupMasters",
                        principalColumn: "GroupID");
                    table.ForeignKey(
                        name: "FK_Scope_Of_Supply_ItemMasters_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ItemMasters",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_Scope_Of_Supply_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelID");
                    table.ForeignKey(
                        name: "FK_Scope_Of_Supply_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalTable: "Series",
                        principalColumn: "SeriesID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeListValues_AttributeID",
                table: "AttributeListValues",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Code",
                table: "Equipments",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_CategoryId",
                table: "ItemMasters",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_ItemCode",
                table: "ItemMasters",
                column: "ItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_L_Equipment_Attribute_AttributeID",
                table: "L_Equipment_Attribute",
                column: "AttributeID");

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

            migrationBuilder.CreateIndex(
                name: "IX_L_Process_Equipment_EquipmentID",
                table: "L_Process_Equipment",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Series_Attribute_AttributeID",
                table: "L_Series_Attribute",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_L_Vertical_Process_ProcessID",
                table: "L_Vertical_Process",
                column: "ProcessID");

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
                name: "IX_Processes_Code",
                table: "Processes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_EquipmentID",
                table: "Scope_Of_Supply",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_GroupID",
                table: "Scope_Of_Supply",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_ItemId",
                table: "Scope_Of_Supply",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_ModelId",
                table: "Scope_Of_Supply",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Of_Supply_SeriesID",
                table: "Scope_Of_Supply",
                column: "SeriesID");

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
                name: "IX_Vertical_Area_Code",
                table: "Vertical_Area",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "L_Equipment_Attribute");

            migrationBuilder.DropTable(
                name: "L_Group_Item");

            migrationBuilder.DropTable(
                name: "L_Model_AttributeValue");

            migrationBuilder.DropTable(
                name: "L_Process_Equipment");

            migrationBuilder.DropTable(
                name: "L_Series_Attribute");

            migrationBuilder.DropTable(
                name: "L_Vertical_Process");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Scope_Of_Supply");

            migrationBuilder.DropTable(
                name: "AttributeListValues");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "Vertical_Area");

            migrationBuilder.DropTable(
                name: "GroupMasters");

            migrationBuilder.DropTable(
                name: "ItemMasters");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "AttributeDefs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Equipments");
        }
    }
}
