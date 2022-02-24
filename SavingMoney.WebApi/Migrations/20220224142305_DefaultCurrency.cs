using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingMoney.WebApi.Migrations
{
    public partial class DefaultCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultCurrency",
                table: "Organizations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "CostCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCurrency",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "CostCategories");
        }
    }
}
