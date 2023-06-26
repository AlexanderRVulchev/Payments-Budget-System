using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentsBudgetSystem.Data.Migrations
{
    public partial class AlteredPaymentSalaryDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsurancePension",
                table: "PaymentSalariesDetails",
                newName: "InsurancePensionEmployer");

            migrationBuilder.RenameColumn(
                name: "InsuranceHealth",
                table: "PaymentSalariesDetails",
                newName: "InsurancePensionEmployee");

            migrationBuilder.RenameColumn(
                name: "InsuranceAdditional",
                table: "PaymentSalariesDetails",
                newName: "InsuranceHealthEmployer");

            migrationBuilder.AddColumn<decimal>(
                name: "InsuranceAdditionalEmployee",
                table: "PaymentSalariesDetails",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InsuranceAdditionalEmployer",
                table: "PaymentSalariesDetails",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InsuranceHealthEmployee",
                table: "PaymentSalariesDetails",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "GlobalSettings",
                columns: new[] { "Id", "SettingName", "SettingValue" },
                values: new object[,]
                {
                    { 7, "Фонд Пенсии - работодател", 0.1372m },
                    { 8, "Фонд Пенсии - служител", 0.1058m },
                    { 9, "Здравно осигуряване - работодател", 0.048m },
                    { 10, "Здравно осигуряване - служител", 0.032m },
                    { 11, "Oсигуряване в УПФ - работодател", 0.028m },
                    { 12, "Oсигуряване в УПФ - служител", 0.022m },
                    { 13, "Данък общ доход", 0.1m },
                    { 14, "Минимална работна заплата", 780m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "GlobalSettings",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DropColumn(
                name: "InsuranceAdditionalEmployee",
                table: "PaymentSalariesDetails");

            migrationBuilder.DropColumn(
                name: "InsuranceAdditionalEmployer",
                table: "PaymentSalariesDetails");

            migrationBuilder.DropColumn(
                name: "InsuranceHealthEmployee",
                table: "PaymentSalariesDetails");

            migrationBuilder.RenameColumn(
                name: "InsurancePensionEmployer",
                table: "PaymentSalariesDetails",
                newName: "InsurancePension");

            migrationBuilder.RenameColumn(
                name: "InsurancePensionEmployee",
                table: "PaymentSalariesDetails",
                newName: "InsuranceHealth");

            migrationBuilder.RenameColumn(
                name: "InsuranceHealthEmployer",
                table: "PaymentSalariesDetails",
                newName: "InsuranceAdditional");
        }
    }
}
