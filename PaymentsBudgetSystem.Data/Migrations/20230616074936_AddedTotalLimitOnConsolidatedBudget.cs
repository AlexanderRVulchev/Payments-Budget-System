using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentsBudgetSystem.Data.Migrations
{
    public partial class AddedTotalLimitOnConsolidatedBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalLimit",
                table: "ConsolidatedBudgets",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalLimit",
                table: "ConsolidatedBudgets");
        }
    }
}
