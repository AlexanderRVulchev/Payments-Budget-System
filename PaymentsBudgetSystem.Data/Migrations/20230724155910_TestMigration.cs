using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentsBudgetSystem.Data.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SettingValue = table.Column<decimal>(type: "DECIMAL(12,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersDependancies",
                columns: table => new
                {
                    PrimaryUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SecondaryUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDependancies", x => new { x.PrimaryUserId, x.SecondaryUserId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    BankAccount = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsolidatedBudgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FiscalYear = table.Column<int>(type: "int", nullable: false),
                    TotalLimit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsolidatedBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsolidatedBudgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Egn = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MonthlySalary = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DateEmployed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLeft = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualBudgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FiscalYear = table.Column<int>(type: "int", nullable: false),
                    SalariesLimit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    SupportLimit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    AssetsLimit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualBudgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Paragraph = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReceiverName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    IsConsolidated = table.Column<bool>(type: "bit", nullable: false),
                    Bank0101 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Bank0102 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Transfer0551 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Transfer0560 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Transfer0580 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Transfer0590 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Bank1015 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Cash1015 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Bank1020 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Cash1020 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Cash1051 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Bank1051 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Bank5100 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Bank5200 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Bank5300 = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LimitSalaries = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LimitSupport = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LimitAssets = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashPaymentDetails",
                columns: table => new
                {
                    CashPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashOrderNumber = table.Column<int>(type: "int", nullable: false),
                    CashOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPaymentDetails", x => x.CashPaymentId);
                    table.ForeignKey(
                        name: "FK_CashPaymentDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashPaymentDetails_Payments_CashPaymentId",
                        column: x => x.CashPaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentAssetsDetails",
                columns: table => new
                {
                    AssetPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAssetsDetails", x => x.AssetPaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentAssetsDetails_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentAssetsDetails_Payments_AssetPaymentId",
                        column: x => x.AssetPaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentSalariesDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetSalaryJobContract = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    NetSalaryStateOfficial = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InsurancePensionEmployer = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InsurancePensionEmployee = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InsuranceHealthEmployer = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InsuranceHealthEmployee = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InsuranceAdditionalEmployer = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InsuranceAdditionalEmployee = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    IncomeTax = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSalariesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSalariesDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentSalariesDetails_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentSupportDetails",
                columns: table => new
                {
                    SupportPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSupportDetails", x => x.SupportPaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentSupportDetails_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentSupportDetails_Payments_SupportPaymentId",
                        column: x => x.SupportPaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAquired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ReportValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PaymentAssetDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_PaymentAssetsDetails_PaymentAssetDetailsId",
                        column: x => x.PaymentAssetDetailsId,
                        principalTable: "PaymentAssetsDetails",
                        principalColumn: "AssetPaymentId");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "33fb1d42-a747-4860-b3ce-7e33a0421a0d", 0, "7ee94d07-5691-4941-b193-1600c44a63ff", "Областна администрация София", false, false, null, "Областна администрация София", null, "SF", "AQAAAAEAACcQAAAAEM86sdcAaU0BO1O6Ur5Qvv/tTQYabIyAsF4XWfRcO2R5hwmYqxXqVCQOeoC7QSlnZA==", null, false, "7218281b-b726-4590-950c-955e42049315", false, "sf" },
                    { "586513cb-2bad-4ea3-ae33-7b8954efb167", 0, "0657203a-0f3c-4255-a17c-6587d706134d", "Администратор", false, false, null, "Админитратор", null, "admin", "AQAAAAEAACcQAAAAEHxWrSwI5z6BQoTxDyN2K2K8ZErgYvK3SvaP43g6grSBG35eyjdfeKpd8gqwil1Ejw==", null, false, "7b01ce09-ccb6-4ed6-847d-0f86d0cbf294", false, "admin" },
                    { "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3", 0, "c8b357cc-640e-49e1-a214-2140640ab454", "Министерски съвет", false, false, null, "Министерски съвет", null, "MC", "AQAAAAEAACcQAAAAENwiXmlEjNkgYmdBfSLRxRhMbp6rwF2cjX9/RIHqX4+nJQWMsvXSL+CkWtWbNDjwsg==", null, false, "03ccaf30-5bb3-48c8-9f80-4af4ad248d7e", false, "mc" },
                    { "a01f638b-535d-48bc-9cee-ec31217088b9", 0, "e20a77c0-c61a-467b-aeae-b09ef13527a4", "Министерство на труда и социалната политика", false, false, null, "Министерство на труда и социалната политика", null, "MTSP", "AQAAAAEAACcQAAAAEEAMTj28FY192KKhCpZx5MkSPn7WCVk0kzB2WOidRuA0uAbXLn0/M68IyBD+UWbk8Q==", null, false, "eb7ad5dc-ea12-48b0-b11f-364ee384e02d", false, "mtsp" },
                    { "f9e9db47-f25b-411f-ad79-2b2715dd132f", 0, "b14fead2-07d8-4f73-a874-61cca078af4d", "Държавна агенция Архиви", false, false, null, "Държавна агенция Архиви", null, "DAA", "AQAAAAEAACcQAAAAEGH4hdldZ7s3pVospnVUqcRKtIvupX/FO0sIOnLd5BkF5a3nnPl0Qgxw678fr2pw1w==", null, false, "3a388c69-131b-415a-81a2-c4c75a165c12", false, "daa" }
                });

            migrationBuilder.InsertData(
                table: "GlobalSettings",
                columns: new[] { "Id", "SettingName", "SettingValue" },
                values: new object[,]
                {
                    { 1, "Стопански инвентар - полезен живот в месеци", 180m },
                    { 2, "Стопански инвентар - процент остатъчна стойност", 0.1m },
                    { 3, "Техника и оборудване - полезен живот в месеци", 60m },
                    { 4, "Техника и оборудване - процент остатъчна стойност", 0.15m },
                    { 5, "Нематериални активи - полезен живот в месеци", 12m },
                    { 6, "Нематериални активи - процент остатъчна стойност", 0m },
                    { 7, "Фонд Пенсии - работодател", 0.1372m },
                    { 8, "Фонд Пенсии - служител", 0.1058m },
                    { 9, "Здравно осигуряване - работодател", 0.048m },
                    { 10, "Здравно осигуряване - служител", 0.032m },
                    { 11, "Oсигуряване в УПФ - работодател", 0.028m },
                    { 12, "Oсигуряване в УПФ - служител", 0.022m },
                    { 13, "Данък общ доход", 0.1m },
                    { 14, "Минимална работна заплата", 780m }
                });

            migrationBuilder.InsertData(
                table: "UsersDependancies",
                columns: new[] { "PrimaryUserId", "SecondaryUserId" },
                values: new object[,]
                {
                    { "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3", "33fb1d42-a747-4860-b3ce-7e33a0421a0d" },
                    { "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3", "f9e9db47-f25b-411f-ad79-2b2715dd132f" }
                });

            migrationBuilder.InsertData(
                table: "Beneficiaries",
                columns: new[] { "Id", "Address", "BankAccount", "Identifier", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("052c29cb-b9c6-42e9-2b2a-08db80453a86"), "гр. София, ж.к. \"Обеля\", бл. 259", "BG62UNCR00060012300458", "981890789", "МЕГАСТОР АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("17b94784-428a-4b5f-2b21-08db80453a86"), "гр. София, ж.к. \"Борово\", ул. \"Пътешествена\" № 16", "BG44STSA56660103409444", "100255356", "ПОСОКА КОМ ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("1dde604d-8bae-4785-c601-08db804fefc7"), "гр. Кърджали, ул. \"Цар Симеон\" 4A", "BG61STSA00000910004134", "128000031", "УНИВЕРС ООД", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("3450ab68-623a-42de-2b22-08db80453a86"), "гр. София, бул. \"Централен\" № 2", "BG48BGSF00001400901551", "951774380", "БГ СИСТЕМС ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("3bbaa90d-9c3d-4a84-c5fe-08db804fefc7"), "гр. София, ж.к. \"Надежда 1\", ул. \"Надежда\" № 15", "BG45STSA44472051043877", "462367724", "ЕЛКОМ-БГ ООД", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("3f4bba5f-4c3a-4230-2b27-08db80453a86"), "гр. София, ж.к. \"Бъкстон\", ул. \"Водна\" № 41", "BG46BGSF40040341000635", "000358188", "СОФИЙСКА ВОДА АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("46b32cd0-8754-4b38-2b2b-08db80453a86"), "гр. София, ж.к. \"Надежда 4\", бул. \"Ломско шосе\" № 116", "BG45BGSF24085234092780", "151541387", "И-НЕЙЧЪР ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("5c1db8b9-76b6-4d89-c600-08db804fefc7"), "гр. София, ж.к. \"Бъкстон\", ул. \"Дечко Делев\" № 40", "BG66BNBG00061166810461", "000100230", "ЕЛЕКТРО-БГ АД", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("77b0827e-9ac2-4aba-2b26-08db80453a86"), "гр. София, ж.к. \"Младост 1\", ул. \"Спасовска\" № 1", "BG49STSA57860103229469", "000364891", "ЕТ СПАСОВ - ДИМИТЪР СПАСОВ", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("9405d63b-25fd-43d3-2b23-08db80453a86"), "гр. Самоков, ул. \"Генерал Пешев\" № 22", "BG10UNCR56000012305100", "952080811", "МАКСИ ЕН ЕООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("bc317b99-304e-43ba-2b29-08db80453a86"), "гр. Перник, ул. \"инж. Георги Иванов\" № 45", "BG01UNCR60209275301978", "388489198", "КОНОВ И СИЕ АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("d3bd3b31-bd00-4718-2b24-08db80453a86"), "гр. София, ж.к. \"Света Тройца\", ул. \"Автомобилна\" № 34", "BG60UNCR56770033055000", "132237700", "СОФИЯ АУТО ЕООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("daec7016-83d7-4bdb-c5ff-08db804fefc7"), "гр. София, ж.к. \"Мусагеница\", ул. \"Панайот Шипков\" № 10", "BG11BNBG30000045010508", "455424624", "ДИАНА КЕТЪРИНГ ЕООД", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("dffb36a4-6882-43fe-2b25-08db80453a86"), "гр. Пловдив, бул. \"В. Левски\" № 114", "BG63STSA30005104521000", "659012547", "ОФИС КОНСУМАТИВИ АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("e5d7258e-a7b2-46f5-2b28-08db80453a86"), "гр. София, ж.к. \"Лозенец\", ул. \"Петко войвода\" № 11", "BG09BNBG50195710913876", "546581074", "ЕНЕРГО БЪЛГАРИЯ АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" }
                });

            migrationBuilder.InsertData(
                table: "ConsolidatedBudgets",
                columns: new[] { "Id", "FiscalYear", "TotalLimit", "UserId" },
                values: new object[] { new Guid("0315e49b-3866-4f17-62a9-08db804a51c9"), 2023, 5000000.00m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ContractType", "DateEmployed", "DateLeft", "Egn", "FirstName", "LastName", "MonthlySalary", "UserId" },
                values: new object[,]
                {
                    { new Guid("08b60d14-da17-45c7-cbea-08db80474b75"), 0, new DateTime(2022, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "6907226568", "Станимир", "Кьосев", 1250.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("0d09c40d-943a-4ac2-cbf1-08db80474b75"), 0, new DateTime(2023, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "9405053536", "Цветомир", "Касабов", 2850.00m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("1ea971ba-1da9-4af5-cbe9-08db80474b75"), 0, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "0112164080", "Любомир", "Бацанов", 2400.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("21d55765-5afb-412f-cbef-08db80474b75"), 0, new DateTime(2022, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "8205015066", "Александър", "Несторов", 3600.00m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("56495a09-76b4-4828-cbe5-08db80474b75"), 0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "7906125598", "Лиана", "Михайлова", 2200.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("5768e56a-8d2f-4ea7-cbe6-08db80474b75"), 0, new DateTime(2022, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "8109183040", "Благовест", "Колев", 1550.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("57e51b62-48fd-408f-cbee-08db80474b75"), 1, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "9801027780", "Цветелина", "Иванова", 1600.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("62c88f1b-19a2-4adb-cbeb-08db80474b75"), 0, new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "6612154400", "Михаил", "Тодораков", 4000.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("87008e93-86b0-43d4-cbe7-08db80474b75"), 1, new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "7405162258", "Евгени", "Маджаров", 3200.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("951dfe17-3ed4-4ee8-cbe8-08db80474b75"), 0, new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "9405051195", "Диана", "Атанасова-Димчева", 1800.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("9b2c9857-a2fe-46f5-cbe3-08db80474b75"), 0, new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "0205123565", "Димитър", "Андонов", 1850.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("a9a85601-7847-4872-cbf0-08db80474b75"), 1, new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "7511136065", "Ивелина", "Шопова", 4700.00m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("aa78e286-fca3-42d2-cbed-08db80474b75"), 1, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "8409136080", "Момчил", "Крайски", 2600.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("d7d70d4c-ca53-4d05-cbf2-08db80474b75"), 1, new DateTime(2022, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "9211257005", "Росица", "Иванова", 2340.00m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("f7681106-88f0-4e13-cbec-08db80474b75"), 1, new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "7505301516", "Спаска", "Кротева", 1100.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("fb8e17f1-1324-4b42-cbe4-08db80474b75"), 1, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "8211307712", "Илияна", "Атанасова", 2600.00m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" }
                });

            migrationBuilder.InsertData(
                table: "IndividualBudgets",
                columns: new[] { "Id", "AssetsLimit", "FiscalYear", "SalariesLimit", "SupportLimit", "UserId" },
                values: new object[,]
                {
                    { new Guid("65490b49-1929-4323-a557-08db804a51ca"), 250000m, 2023, 2000000m, 700000m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("6ad66f5a-a320-49d2-a558-08db804a51ca"), 75000m, 2023, 300000m, 150000m, "33fb1d42-a747-4860-b3ce-7e33a0421a0d" },
                    { new Guid("bddb96e8-ea21-4391-a559-08db804a51ca"), 200000m, 2023, 800000m, 400000m, "f9e9db47-f25b-411f-ad79-2b2715dd132f" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "Date", "Description", "Paragraph", "PaymentType", "ReceiverName", "UserId" },
                values: new object[,]
                {
                    { new Guid("03b8e961-efa7-4395-5650-08db804e8f7e"), 720.00m, new DateTime(2023, 7, 9, 11, 27, 0, 191, DateTimeKind.Unspecified).AddTicks(3745), "Хостинг за сайта на агенцията за 2023 г.", 7, 3, "БГ СИСТЕМС ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("0636467d-e3aa-4b48-5643-08db804e8f7e"), 22500.00m, new DateTime(2023, 1, 8, 10, 49, 2, 57, DateTimeKind.Unspecified).AddTicks(8480), "Закупуване на дисков масив за допълнителен сторидж", 10, 4, "ЕЛКОМ-БГ ООД", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("0655dcef-51fc-4a50-564b-08db804e8f7e"), 12500.00m, new DateTime(2023, 4, 15, 11, 16, 28, 934, DateTimeKind.Unspecified).AddTicks(6507), "Канцеларски материали за дирекция ЦДА", 6, 3, "ОФИС КОНСУМАТИВИ АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("149f280b-f4cf-4e47-5642-08db804e8f7e"), 2750.00m, new DateTime(2023, 1, 21, 10, 45, 13, 158, DateTimeKind.Unspecified).AddTicks(323), "Осигуряване на кетъринг и обслужване за конференция в зала 4", 7, 3, "ДИАНА КЕТЪРИНГ ЕООД", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("178d9a33-702c-4cac-564d-08db804e8f7e"), 17895.76m, new DateTime(2023, 5, 30, 11, 18, 24, 606, DateTimeKind.Unspecified).AddTicks(5379), "Изплатени заплати за м.5 2023 г.", 0, 1, "Служители", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("1973af10-ecd7-415f-564f-08db804e8f7e"), 1650.00m, new DateTime(2023, 7, 9, 11, 26, 4, 220, DateTimeKind.Unspecified).AddTicks(7511), "Командировъчни разходи на нач. отдел \"Правен\"", 8, 3, "ПОСОКА КОМ ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("27ed4479-0a41-45ad-5646-08db804e8f7e"), 985.00m, new DateTime(2023, 2, 12, 11, 4, 16, 897, DateTimeKind.Unspecified).AddTicks(9321), "Обзавеждане на стая 101", 9, 4, "МЕГАСТОР АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "Date", "Description", "Paragraph", "PaymentType", "ReceiverName", "UserId" },
                values: new object[,]
                {
                    { new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), 2920.00m, new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), "Обзавеждане на конферентна зала", 9, 4, "УНИВЕРС ООД", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("5c5553a2-662a-41a1-5648-08db804e8f7e"), 224.00m, new DateTime(2023, 3, 9, 11, 9, 43, 95, DateTimeKind.Unspecified).AddTicks(5792), "Възстановена сума на служител за проведен курс", 7, 0, "Росица Иванова", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("5c7aa898-9b7e-4f17-5645-08db804e8f7e"), 1200.00m, new DateTime(2023, 2, 9, 11, 3, 8, 12, DateTimeKind.Unspecified).AddTicks(3728), "Закупуване на самолетни билети за командировка - Белгия", 8, 3, "ПОСОКА КОМ ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("6342590f-6f84-4654-564c-08db804e8f7e"), 29094.10m, new DateTime(2023, 5, 1, 11, 17, 43, 692, DateTimeKind.Unspecified).AddTicks(779), "Изплатени заплати за м.5 2023 г.", 0, 1, "Служители", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("7b4ec9b1-9022-4c9c-5640-08db804e8f7e"), 25050.00m, new DateTime(2023, 1, 12, 10, 38, 16, 241, DateTimeKind.Unspecified).AddTicks(2308), "Закупуване на нови лицензи за работа", 11, 4, "БГ СИСТЕМС ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("8b7cd7a0-2c74-4c8a-5641-08db804e8f7e"), 1250.00m, new DateTime(2023, 1, 15, 10, 39, 19, 751, DateTimeKind.Unspecified).AddTicks(4929), "Закупуване на материали за ремонт на стая 407", 6, 3, "КОНОВ И СИЕ АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("a26ffa37-40e5-456f-5649-08db804e8f7e"), 5574.15m, new DateTime(2023, 3, 19, 11, 11, 5, 594, DateTimeKind.Unspecified).AddTicks(6825), "Електроенергия за м. януари 2023 г.", 7, 3, "ЕНЕРГО БЪЛГАРИЯ АД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("a8e27c5c-c1ad-471a-563e-08db804e8f7e"), 9746.94m, new DateTime(2023, 1, 9, 10, 31, 50, 615, DateTimeKind.Unspecified).AddTicks(789), "Изплатени заплати за м.1 2023 г.", 0, 1, "Служители", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("c4d57ebc-2bc4-4f70-5644-08db804e8f7e"), 660.00m, new DateTime(2023, 2, 2, 11, 2, 13, 206, DateTimeKind.Unspecified).AddTicks(8344), "Почистване и ремонт на служебен автомобил СА1015ВД", 7, 0, "Евгени Маджаров", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("e24bc797-8024-47b9-563f-08db804e8f7e"), 650.00m, new DateTime(2023, 1, 11, 10, 34, 31, 210, DateTimeKind.Unspecified).AddTicks(7875), "Командировка гр. Силистра", 8, 0, "Благовест Колев", "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("ed64127e-fa2d-4e7d-5647-08db804e8f7e"), 14352.64m, new DateTime(2023, 2, 21, 11, 5, 26, 482, DateTimeKind.Unspecified).AddTicks(1122), "Изплатени заплати за м.2 2023 г.", 0, 1, "Служители", "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("ed82c4f5-4c19-44f4-564a-08db804e8f7e"), 3050.40m, new DateTime(2023, 4, 10, 11, 12, 12, 297, DateTimeKind.Unspecified).AddTicks(1277), "Мултифункционално у-ство за отдел ЧР", 10, 4, "И-НЕЙЧЪР ООД", "f9e9db47-f25b-411f-ad79-2b2715dd132f" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "Bank0101", "Bank0102", "Bank1015", "Bank1020", "Bank1051", "Bank5100", "Bank5200", "Bank5300", "Cash1015", "Cash1020", "Cash1051", "IsConsolidated", "LimitAssets", "LimitSalaries", "LimitSupport", "Month", "Transfer0551", "Transfer0560", "Transfer0580", "Transfer0590", "UserId", "Year" },
                values: new object[,]
                {
                    { new Guid("1e0ec52d-3294-4fcd-ad6e-08db80565e8f"), 7599.61m, 12672.0m, 0m, 2750.00m, 0m, 0m, 22500.0m, 0m, 0m, 224.00m, 0m, false, 250000.0m, 2000000.0m, 700000.0m, 5, 5863.59m, 1930.40m, 1206.50m, 2252.40m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3", 2023 },
                    { new Guid("35be5043-ea75-4fe2-ad70-08db80565e8f"), 21003.52m, 23451.68m, 13750.0m, 8324.15m, 1200.0m, 985.0m, 25550.40m, 25050.00m, 0m, 884.0m, 650.0m, true, 525000.0m, 3100000.0m, 1250000.0m, 5, 13081.47m, 4386.66m, 2691.66m, 4939.47m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3", 2023 },
                    { new Guid("3f810f3c-ee75-40eb-ad73-08db80565e8f"), 5179.84m, 789.68m, 1250.0m, 5574.15m, 1200.0m, 985.0m, 0m, 25050.0m, 0m, 660.0m, 650.0m, false, 200000.0m, 800000.0m, 400000.0m, 3, 1877.76m, 618.19m, 386.37m, 663.28m, "f9e9db47-f25b-411f-ad79-2b2715dd132f", 2023 },
                    { new Guid("4cf49743-8e93-4aea-ad6f-08db80565e8f"), 7599.61m, 12672.00m, 0m, 2750.00m, 0m, 2920.0m, 22500.0m, 0m, 0m, 224.00m, 0m, false, 250000.0m, 2000000.0m, 700000.0m, 6, 5863.59m, 1930.40m, 1206.50m, 2252.40m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3", 2023 },
                    { new Guid("c881b1b5-f1a3-4bfb-ad71-08db80565e8f"), 21003.52m, 23451.68m, 13750.00m, 8324.15m, 1200.0m, 3905.00m, 25550.40m, 25050.00m, 0m, 884.00m, 650.00m, true, 525000.0m, 3100000.0m, 1250000.0m, 6, 13081.47m, 4306.66m, 2691.66m, 4939.47m, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3", 2023 },
                    { new Guid("cbd747ba-528c-45a9-ad72-08db80565e8f"), 5179.84m, 789.68m, 13750.0m, 5574.15m, 1200.00m, 985.00m, 3050.40m, 25050.00m, 0m, 660.00m, 650.00m, false, 200000.0m, 800000.0m, 400000.0m, 4, 1877.76m, 618.19m, 386.37m, 663.28m, "f9e9db47-f25b-411f-ad79-2b2715dd132f", 2023 },
                    { new Guid("d0016fd5-368f-423e-ad6c-08db80565e8f"), 13403.91m, 10779.68m, 13750.00m, 5574.15m, 1200.00m, 985.00m, 3050.40m, 25050.00m, 0m, 660.00m, 650.00m, false, 200000.00m, 800000.00m, 400000.00m, 6, 7217.88m, 2376.26m, 1485.16m, 2687.07m, "f9e9db47-f25b-411f-ad79-2b2715dd132f", 2023 },
                    { new Guid("e8cf571f-6486-4cfd-ad6d-08db80565e8f"), 13403.91m, 10779.68m, 13750.00m, 5574.15m, 1200.00m, 985.00m, 3050.40m, 25050.0m, 0m, 660.00m, 650.0m, false, 200000m, 800000.00m, 400000.0m, 5, 7217.88m, 2376.26m, 1485.16m, 2687.07m, "f9e9db47-f25b-411f-ad79-2b2715dd132f", 2023 }
                });

            migrationBuilder.InsertData(
                table: "CashPaymentDetails",
                columns: new[] { "CashPaymentId", "CashOrderDate", "CashOrderNumber", "EmployeeId" },
                values: new object[,]
                {
                    { new Guid("5c5553a2-662a-41a1-5648-08db804e8f7e"), new DateTime(2023, 7, 9, 11, 9, 43, 95, DateTimeKind.Unspecified).AddTicks(5819), 15, new Guid("d7d70d4c-ca53-4d05-cbf2-08db80474b75") },
                    { new Guid("c4d57ebc-2bc4-4f70-5644-08db804e8f7e"), new DateTime(2023, 7, 9, 11, 2, 13, 206, DateTimeKind.Unspecified).AddTicks(8371), 2, new Guid("87008e93-86b0-43d4-cbe7-08db80474b75") },
                    { new Guid("e24bc797-8024-47b9-563f-08db804e8f7e"), new DateTime(2023, 7, 9, 10, 34, 31, 210, DateTimeKind.Unspecified).AddTicks(8796), 1, new Guid("5768e56a-8d2f-4ea7-cbe6-08db80474b75") }
                });

            migrationBuilder.InsertData(
                table: "PaymentAssetsDetails",
                columns: new[] { "AssetPaymentId", "BeneficiaryId", "DeliveryDate", "InvoiceDate", "InvoiceNumber" },
                values: new object[,]
                {
                    { new Guid("0636467d-e3aa-4b48-5643-08db804e8f7e"), new Guid("3bbaa90d-9c3d-4a84-c5fe-08db804fefc7"), new DateTime(2023, 1, 8, 10, 49, 2, 57, DateTimeKind.Unspecified).AddTicks(8480), new DateTime(2022, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000032660" },
                    { new Guid("27ed4479-0a41-45ad-5646-08db804e8f7e"), new Guid("052c29cb-b9c6-42e9-2b2a-08db80453a86"), new DateTime(2023, 2, 12, 11, 4, 16, 897, DateTimeKind.Unspecified).AddTicks(9321), new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000004521" },
                    { new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), new Guid("1dde604d-8bae-4785-c601-08db804fefc7"), new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "1000602000" },
                    { new Guid("7b4ec9b1-9022-4c9c-5640-08db804e8f7e"), new Guid("3450ab68-623a-42de-2b22-08db80453a86"), new DateTime(2023, 1, 12, 10, 38, 16, 241, DateTimeKind.Unspecified).AddTicks(2308), new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "0005189987" },
                    { new Guid("ed82c4f5-4c19-44f4-564a-08db804e8f7e"), new Guid("46b32cd0-8754-4b38-2b2b-08db80453a86"), new DateTime(2023, 4, 10, 11, 12, 12, 297, DateTimeKind.Unspecified).AddTicks(1277), new DateTime(2023, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "0100000012" }
                });

            migrationBuilder.InsertData(
                table: "PaymentSalariesDetails",
                columns: new[] { "Id", "EmployeeId", "IncomeTax", "InsuranceAdditionalEmployee", "InsuranceAdditionalEmployer", "InsuranceHealthEmployee", "InsuranceHealthEmployer", "InsurancePensionEmployee", "InsurancePensionEmployer", "NetSalaryJobContract", "NetSalaryStateOfficial", "PaymentId" },
                values: new object[,]
                {
                    { new Guid("07b84d2e-754a-4da4-7420-08db804e8f82"), new Guid("5768e56a-8d2f-4ea7-cbe6-08db80474b75"), 130.23m, 34.10m, 43.40m, 49.60m, 74.40m, 163.99m, 212.66m, 1172.08m, 0m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("0a7f2190-3bec-45a2-7417-08db804e8f82"), new Guid("5768e56a-8d2f-4ea7-cbe6-08db80474b75"), 130.23m, 34.10m, 43.40m, 49.60m, 74.40m, 163.99m, 212.66m, 1172.08m, 0.00m, new Guid("a8e27c5c-c1ad-471a-563e-08db804e8f7e") },
                    { new Guid("1eeebe3d-a0c6-40e0-742a-08db804e8f82"), new Guid("0d09c40d-943a-4ac2-cbf1-08db80474b75"), 239.46m, 62.70m, 79.80m, 91.20m, 136.80m, 301.53m, 391.02m, 2155.11m, 0m, new Guid("178d9a33-702c-4cac-564d-08db804e8f7e") },
                    { new Guid("3211970a-7f4f-4750-741a-08db804e8f82"), new Guid("21d55765-5afb-412f-cbef-08db80474b75"), 302.47m, 79.20m, 100.80m, 115.20m, 172.80m, 380.88m, 493.92m, 2722.25m, 0.00m, new Guid("ed64127e-fa2d-4e7d-5647-08db804e8f7e") },
                    { new Guid("48e5d1f8-adad-49a1-7427-08db804e8f82"), new Guid("57e51b62-48fd-408f-cbee-08db80474b75"), 160.00m, 0m, 80.00m, 0m, 128.00m, 0m, 388.80m, 0m, 1440.00m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("4c1ed72a-afa5-406a-741d-08db804e8f82"), new Guid("9b2c9857-a2fe-46f5-cbe3-08db80474b75"), 155.44m, 40.70m, 51.80m, 59.20m, 88.80m, 195.73m, 253.82m, 1398.93m, 0m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("525f62b7-690a-4736-7419-08db804e8f82"), new Guid("57e51b62-48fd-408f-cbee-08db80474b75"), 87.74m, 0.00m, 43.87m, 0.00m, 70.19m, 0.00m, 213.21m, 0.00m, 789.68m, new Guid("a8e27c5c-c1ad-471a-563e-08db804e8f7e") },
                    { new Guid("6b6a4d04-f583-447d-7429-08db804e8f82"), new Guid("a9a85601-7847-4872-cbf0-08db80474b75"), 470.00m, 0m, 235.00m, 0m, 376.00m, 0m, 1142.10m, 0m, 4230.00m, new Guid("178d9a33-702c-4cac-564d-08db804e8f7e") },
                    { new Guid("6d21aa45-7bef-4360-742b-08db804e8f82"), new Guid("d7d70d4c-ca53-4d05-cbf2-08db80474b75"), 234.00m, 0m, 117.00m, 0m, 187.20m, 0m, 568.62m, 0m, 2106.00m, new Guid("178d9a33-702c-4cac-564d-08db804e8f7e") },
                    { new Guid("7b9a2403-61af-4ef8-741c-08db804e8f82"), new Guid("d7d70d4c-ca53-4d05-cbf2-08db80474b75"), 234.00m, 0m, 117.00m, 0m, 187.20m, 0m, 568.62m, 0m, 2106.00m, new Guid("ed64127e-fa2d-4e7d-5647-08db804e8f7e") },
                    { new Guid("7f5d827d-5730-453d-7426-08db804e8f82"), new Guid("aa78e286-fca3-42d2-cbed-08db80474b75"), 260.00m, 0m, 130.00m, 0m, 208.00m, 0m, 631.80m, 0m, 2340.00m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("a4e0b3ae-7033-456a-7424-08db804e8f82"), new Guid("08b60d14-da17-45c7-cbea-08db80474b75"), 105.03m, 27.50m, 35.00m, 40.00m, 60.00m, 132.25m, 171.50m, 945.23m, 0m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("a800f7aa-de8d-48a4-7416-08db804e8f82"), new Guid("56495a09-76b4-4828-cbe5-08db80474b75"), 184.84m, 48.40m, 61.60m, 70.40m, 105.60m, 232.76m, 301.84m, 1663.60m, 0.00m, new Guid("a8e27c5c-c1ad-471a-563e-08db804e8f7e") },
                    { new Guid("b5190512-3d13-484e-7422-08db804e8f82"), new Guid("951dfe17-3ed4-4ee8-cbe8-08db80474b75"), 136.60m, 35.77m, 45.52m, 52.03m, 78.04m, 172.01m, 223.06m, 1229.40m, 0m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("bf69ae2a-328d-4269-7425-08db804e8f82"), new Guid("f7681106-88f0-4e13-cbec-08db80474b75"), 110.00m, 0m, 55.00m, 0m, 88.00m, 0m, 267.30m, 0m, 990.00m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("c256288a-8b6d-48d0-7428-08db804e8f82"), new Guid("21d55765-5afb-412f-cbef-08db80474b75"), 302.47m, 79.20m, 100.80m, 115.20m, 172.80m, 380.88m, 493.92m, 2722.25m, 0m, new Guid("178d9a33-702c-4cac-564d-08db804e8f7e") },
                    { new Guid("cc4520c2-5d8d-4760-741e-08db804e8f82"), new Guid("fb8e17f1-1324-4b42-cbe4-08db80474b75"), 260.00m, 0m, 130.00m, 0m, 208.00m, 0m, 431.80m, 0m, 2340.00m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("ccbdddc2-8332-4d6c-7418-08db804e8f82"), new Guid("08b60d14-da17-45c7-cbea-08db80474b75"), 105.03m, 27.50m, 35.00m, 40.00m, 60.00m, 132.25m, 171.50m, 945.23m, 0.00m, new Guid("a8e27c5c-c1ad-471a-563e-08db804e8f7e") },
                    { new Guid("d2522c0c-72bf-48e7-741b-08db804e8f82"), new Guid("a9a85601-7847-4872-cbf0-08db80474b75"), 470.00m, 0.00m, 235.00m, 0m, 376.0m, 0m, 1142.10m, 0m, 4230.00m, new Guid("ed64127e-fa2d-4e7d-5647-08db804e8f7e") },
                    { new Guid("e5df6be6-7256-4eaa-7415-08db804e8f82"), new Guid("9b2c9857-a2fe-46f5-cbe3-08db80474b75"), 155.44m, 40.70m, 51.80m, 59.20m, 88.80m, 195.73m, 253.82m, 1398.93m, 0m, new Guid("a8e27c5c-c1ad-471a-563e-08db804e8f7e") },
                    { new Guid("e664a122-c3c8-449b-7421-08db804e8f82"), new Guid("87008e93-86b0-43d4-cbe7-08db80474b75"), 320.00m, 0m, 160.00m, 0m, 256.00m, 0m, 777.60m, 0m, 2880.00m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("f81ce272-d8de-4a0e-7423-08db804e8f82"), new Guid("1ea971ba-1da9-4af5-cbe9-08db80474b75"), 201.65m, 52.80m, 67.20m, 76.80m, 115.20m, 253.92m, 329.28m, 1814.83m, 0m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") },
                    { new Guid("ff783464-04c7-49cf-741f-08db804e8f82"), new Guid("56495a09-76b4-4828-cbe5-08db80474b75"), 184.84m, 48.40m, 61.60m, 70.40m, 105.60m, 232.76m, 301.84m, 1663.60m, 0m, new Guid("6342590f-6f84-4654-564c-08db804e8f7e") }
                });

            migrationBuilder.InsertData(
                table: "PaymentSupportDetails",
                columns: new[] { "SupportPaymentId", "BeneficiaryId", "InvoiceDate", "InvoiceNumber" },
                values: new object[,]
                {
                    { new Guid("03b8e961-efa7-4395-5650-08db804e8f7e"), new Guid("3450ab68-623a-42de-2b22-08db80453a86"), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000054848" },
                    { new Guid("0655dcef-51fc-4a50-564b-08db804e8f7e"), new Guid("dffb36a4-6882-43fe-2b25-08db80453a86"), new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "6684894894" },
                    { new Guid("149f280b-f4cf-4e47-5642-08db804e8f7e"), new Guid("daec7016-83d7-4bdb-c5ff-08db804fefc7"), new DateTime(2022, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "1000602346" },
                    { new Guid("1973af10-ecd7-415f-564f-08db804e8f7e"), new Guid("17b94784-428a-4b5f-2b21-08db80453a86"), new DateTime(2023, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "5000050684" },
                    { new Guid("5c7aa898-9b7e-4f17-5645-08db804e8f7e"), new Guid("17b94784-428a-4b5f-2b21-08db80453a86"), new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "0100000576" },
                    { new Guid("8b7cd7a0-2c74-4c8a-5641-08db804e8f7e"), new Guid("bc317b99-304e-43ba-2b29-08db80453a86"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000000221" },
                    { new Guid("a26ffa37-40e5-456f-5649-08db804e8f7e"), new Guid("e5d7258e-a7b2-46f5-2b28-08db80453a86"), new DateTime(2023, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "9180795109" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "DateAquired", "Description", "PaymentAssetDetailsId", "ReportValue", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d546759-7099-4215-0fcd-08db804f7559"), new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), "Посетителски стол", new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), 120.00m, 9, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("0eb104d0-c006-4fbc-0fcc-08db804f7559"), new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), "Конферентна маса", new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), 1220.00m, 9, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("2d0b34c6-db91-4e5a-0fc4-08db804f7559"), new DateTime(2023, 1, 8, 10, 49, 2, 57, DateTimeKind.Unspecified).AddTicks(8480), "Дисков масив QSAN SMB", new Guid("0636467d-e3aa-4b48-5643-08db804e8f7e"), 22500.00m, 10, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("2d40dd08-22bf-4ec1-0fc5-08db804f7559"), new DateTime(2023, 2, 12, 11, 4, 16, 897, DateTimeKind.Unspecified).AddTicks(9321), "Офис стол", new Guid("27ed4479-0a41-45ad-5646-08db804e8f7e"), 220.00m, 9, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("3ef2a82f-da80-427e-0fc3-08db804f7559"), new DateTime(2023, 1, 12, 10, 38, 16, 241, DateTimeKind.Unspecified).AddTicks(2308), "MS SQL Server Enterprise", new Guid("7b4ec9b1-9022-4c9c-5640-08db804e8f7e"), 24000.00m, 11, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("7b6acfe4-941f-4cf4-0fd0-08db804f7559"), new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), "Посетителски стол", new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), 120.00m, 9, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("7e806068-961a-4d44-0fc7-08db804f7559"), new DateTime(2023, 2, 12, 11, 4, 16, 897, DateTimeKind.Unspecified).AddTicks(9321), "Офис стол", new Guid("27ed4479-0a41-45ad-5646-08db804e8f7e"), 220.00m, 9, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("89e250b8-9453-4099-0fc0-08db804f7559"), new DateTime(2023, 1, 12, 10, 38, 16, 241, DateTimeKind.Unspecified).AddTicks(2308), "Windows 10 Enterprise", new Guid("7b4ec9b1-9022-4c9c-5640-08db804e8f7e"), 350.00m, 11, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("90c86545-107a-4182-0fc1-08db804f7559"), new DateTime(2023, 1, 12, 10, 38, 16, 241, DateTimeKind.Unspecified).AddTicks(2308), "Windows 10 Enterprise", new Guid("7b4ec9b1-9022-4c9c-5640-08db804e8f7e"), 350.00m, 11, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("9b623b4f-8fb0-425c-0fcf-08db804f7559"), new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), "Посетителски стол", new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), 120.00m, 9, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("9fce69a3-6e35-4b3d-0fc8-08db804f7559"), new DateTime(2023, 2, 12, 11, 4, 16, 897, DateTimeKind.Unspecified).AddTicks(9321), "Бюро", new Guid("27ed4479-0a41-45ad-5646-08db804e8f7e"), 325.00m, 9, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("a0369632-8aff-411f-0fce-08db804f7559"), new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), "Посетителски стол", new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), 120.00m, 9, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("a270f15d-1520-425a-0fc9-08db804f7559"), new DateTime(2023, 4, 10, 11, 12, 12, 297, DateTimeKind.Unspecified).AddTicks(1277), "МФУ Brother", new Guid("ed82c4f5-4c19-44f4-564a-08db804e8f7e"), 1525.20m, 10, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("a4823c15-8389-4842-0fc2-08db804f7559"), new DateTime(2023, 1, 12, 10, 38, 16, 241, DateTimeKind.Unspecified).AddTicks(2308), "Windows 10 Enterprise", new Guid("7b4ec9b1-9022-4c9c-5640-08db804e8f7e"), 350.00m, 11, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("da5a56fa-d860-400e-0fca-08db804f7559"), new DateTime(2023, 4, 10, 11, 12, 12, 297, DateTimeKind.Unspecified).AddTicks(1277), "МФУ Brother", new Guid("ed82c4f5-4c19-44f4-564a-08db804e8f7e"), 1525.20m, 10, "f9e9db47-f25b-411f-ad79-2b2715dd132f" },
                    { new Guid("e8045a2c-7d67-40ce-0fcb-08db804f7559"), new DateTime(2023, 6, 2, 11, 21, 58, 94, DateTimeKind.Unspecified).AddTicks(7357), "Конферентна маса", new Guid("4bc8062d-af15-47fe-564e-08db804e8f7e"), 1220.00m, 9, "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3" },
                    { new Guid("f2249732-90f1-4793-0fc6-08db804f7559"), new DateTime(2023, 2, 12, 11, 4, 16, 897, DateTimeKind.Unspecified).AddTicks(9321), "Офис стол", new Guid("27ed4479-0a41-45ad-5646-08db804e8f7e"), 220.00m, 9, "f9e9db47-f25b-411f-ad79-2b2715dd132f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_PaymentAssetDetailsId",
                table: "Assets",
                column: "PaymentAssetDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UserId",
                table: "Assets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_UserId",
                table: "Beneficiaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPaymentDetails_EmployeeId",
                table: "CashPaymentDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsolidatedBudgets_UserId",
                table: "ConsolidatedBudgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBudgets_UserId",
                table: "IndividualBudgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAssetsDetails_BeneficiaryId",
                table: "PaymentAssetsDetails",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSalariesDetails_EmployeeId",
                table: "PaymentSalariesDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSalariesDetails_PaymentId",
                table: "PaymentSalariesDetails",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSupportDetails_BeneficiaryId",
                table: "PaymentSupportDetails",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "CashPaymentDetails");

            migrationBuilder.DropTable(
                name: "ConsolidatedBudgets");

            migrationBuilder.DropTable(
                name: "GlobalSettings");

            migrationBuilder.DropTable(
                name: "IndividualBudgets");

            migrationBuilder.DropTable(
                name: "PaymentSalariesDetails");

            migrationBuilder.DropTable(
                name: "PaymentSupportDetails");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "UsersDependancies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PaymentAssetsDetails");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
