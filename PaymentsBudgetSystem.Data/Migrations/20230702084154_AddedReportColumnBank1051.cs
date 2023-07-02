using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentsBudgetSystem.Data.Migrations
{
    public partial class AddedReportColumnBank1051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Bank1051",
                table: "Reports",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank1051",
                table: "Reports");
        }
    }
}
