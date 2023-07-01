using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentsBudgetSystem.Data.Migrations
{
    public partial class ReceiverNameAddedToPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Payments");
        }
    }
}
