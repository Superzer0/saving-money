using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingMoney.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SavingMoney");

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "SavingMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCategories",
                schema: "SavingMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCategories_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "SavingMoney",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostSubCategories",
                schema: "SavingMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostSubCategories_CostCategories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "SavingMoney",
                        principalTable: "CostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                schema: "SavingMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CostSubCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeSpentUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Currency = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Costs_CostSubCategories_CostSubCategoryId",
                        column: x => x.CostSubCategoryId,
                        principalSchema: "SavingMoney",
                        principalTable: "CostSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Costs_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "SavingMoney",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PredictedSubcategoryCosts",
                schema: "SavingMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CostSubCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    PredictedMonth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Currency = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictedSubcategoryCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredictedSubcategoryCosts_CostSubCategories_CostSubCategoryId",
                        column: x => x.CostSubCategoryId,
                        principalSchema: "SavingMoney",
                        principalTable: "CostSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PredictedSubcategoryCosts_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "SavingMoney",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCategories_Id_OrganizationId",
                schema: "SavingMoney",
                table: "CostCategories",
                columns: new[] { "Id", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostCategories_OrganizationId",
                schema: "SavingMoney",
                table: "CostCategories",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CostSubCategoryId",
                schema: "SavingMoney",
                table: "Costs",
                column: "CostSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_Id_OrganizationId",
                schema: "SavingMoney",
                table: "Costs",
                columns: new[] { "Id", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_OrganizationId",
                schema: "SavingMoney",
                table: "Costs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CostSubCategories_Id_ParentId",
                schema: "SavingMoney",
                table: "CostSubCategories",
                columns: new[] { "Id", "ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostSubCategories_ParentId",
                schema: "SavingMoney",
                table: "CostSubCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictedSubcategoryCosts_CostSubCategoryId",
                schema: "SavingMoney",
                table: "PredictedSubcategoryCosts",
                column: "CostSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictedSubcategoryCosts_Id_OrganizationId",
                schema: "SavingMoney",
                table: "PredictedSubcategoryCosts",
                columns: new[] { "Id", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PredictedSubcategoryCosts_OrganizationId",
                schema: "SavingMoney",
                table: "PredictedSubcategoryCosts",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Costs",
                schema: "SavingMoney");

            migrationBuilder.DropTable(
                name: "PredictedSubcategoryCosts",
                schema: "SavingMoney");

            migrationBuilder.DropTable(
                name: "CostSubCategories",
                schema: "SavingMoney");

            migrationBuilder.DropTable(
                name: "CostCategories",
                schema: "SavingMoney");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "SavingMoney");
        }
    }
}
