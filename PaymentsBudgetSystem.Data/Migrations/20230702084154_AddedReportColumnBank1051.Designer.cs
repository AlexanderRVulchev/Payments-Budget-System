﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentsBudgetSystem.Data;

#nullable disable

namespace PaymentsBudgetSystem.Data.Migrations
{
    [DbContext(typeof(PBSystemDbContext))]
    [Migration("20230702084154_AddedReportColumnBank1051")]
    partial class AddedReportColumnBank1051
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAquired")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDisposed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("PaymentAssetDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ReportValue")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentAssetDetailsId");

                    b.HasIndex("UserId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Beneficiary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("nvarchar(22)");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.CashPaymentDetails", b =>
                {
                    b.Property<Guid>("CashPaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CashOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CashOrderNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CashPaymentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("CashPaymentDetails");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.ConsolidatedBudget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AssetsLimit")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("FiscalYear")
                        .HasColumnType("int");

                    b.Property<decimal>("SalariesLimit")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("SupportLimit")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("TotalLimit")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ConsolidatedBudgets");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ContractType")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEmployed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateLeft")
                        .HasColumnType("datetime2");

                    b.Property<string>("Egn")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("MonthlySalary")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.GlobalSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("SettingName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("SettingValue")
                        .HasColumnType("DECIMAL(12,4)");

                    b.HasKey("Id");

                    b.ToTable("GlobalSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SettingName = "Стопански инвентар - полезен живот в месеци",
                            SettingValue = 180m
                        },
                        new
                        {
                            Id = 2,
                            SettingName = "Стопански инвентар - процент остатъчна стойност",
                            SettingValue = 0.1m
                        },
                        new
                        {
                            Id = 3,
                            SettingName = "Техника и оборудване - полезен живот в месеци",
                            SettingValue = 60m
                        },
                        new
                        {
                            Id = 4,
                            SettingName = "Техника и оборудване - процент остатъчна стойност",
                            SettingValue = 0.15m
                        },
                        new
                        {
                            Id = 5,
                            SettingName = "Нематериални активи - полезен живот в месеци",
                            SettingValue = 12m
                        },
                        new
                        {
                            Id = 6,
                            SettingName = "Нематериални активи - процент остатъчна стойност",
                            SettingValue = 0m
                        },
                        new
                        {
                            Id = 7,
                            SettingName = "Фонд Пенсии - работодател",
                            SettingValue = 0.1372m
                        },
                        new
                        {
                            Id = 8,
                            SettingName = "Фонд Пенсии - служител",
                            SettingValue = 0.1058m
                        },
                        new
                        {
                            Id = 9,
                            SettingName = "Здравно осигуряване - работодател",
                            SettingValue = 0.048m
                        },
                        new
                        {
                            Id = 10,
                            SettingName = "Здравно осигуряване - служител",
                            SettingValue = 0.032m
                        },
                        new
                        {
                            Id = 11,
                            SettingName = "Oсигуряване в УПФ - работодател",
                            SettingValue = 0.028m
                        },
                        new
                        {
                            Id = 12,
                            SettingName = "Oсигуряване в УПФ - служител",
                            SettingValue = 0.022m
                        },
                        new
                        {
                            Id = 13,
                            SettingName = "Данък общ доход",
                            SettingValue = 0.1m
                        },
                        new
                        {
                            Id = 14,
                            SettingName = "Минимална работна заплата",
                            SettingValue = 780m
                        });
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.IndividualBudget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AssetsLimit")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("FiscalYear")
                        .HasColumnType("int");

                    b.Property<decimal>("SalariesLimit")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("SupportLimit")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IndividualBudgets");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsPending")
                        .HasColumnType("bit");

                    b.Property<int>("Paragraph")
                        .HasColumnType("int");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.PaymentAssetsDetails", b =>
                {
                    b.Property<Guid>("AssetPaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeneficiaryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssetPaymentId");

                    b.HasIndex("BeneficiaryId");

                    b.ToTable("PaymentAssetsDetails");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.PaymentSalaryDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("IncomeTax")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("InsuranceAdditionalEmployee")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("InsuranceAdditionalEmployer")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("InsuranceHealthEmployee")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("InsuranceHealthEmployer")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("InsurancePensionEmployee")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("InsurancePensionEmployer")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("NetSalaryJobContract")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("NetSalaryStateOfficial")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PaymentId");

                    b.ToTable("PaymentSalariesDetails");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.PaymentSupportDetails", b =>
                {
                    b.Property<Guid>("SupportPaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeneficiaryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupportPaymentId");

                    b.HasIndex("BeneficiaryId");

                    b.ToTable("PaymentSupportDetails");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Bank0101")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Bank0102")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Bank1015")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Bank1020")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Bank1051")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Bank5100")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Bank5200")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Bank5300")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Cash1015")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Cash1020")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Cash1051")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<bool>("IsConsolidated")
                        .HasColumnType("bit");

                    b.Property<decimal>("LimitAssets")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("LimitSalaries")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("LimitSupport")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<decimal>("Transfer0551")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Transfer0560")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Transfer0580")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal>("Transfer0590")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.UserDependancy", b =>
                {
                    b.Property<string>("PrimaryUserId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("SecondaryUserId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("PrimaryUserId", "SecondaryUserId");

                    b.ToTable("UsersDependancies");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Asset", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.PaymentAssetsDetails", "PaymentAssetsDetails")
                        .WithMany("Assets")
                        .HasForeignKey("PaymentAssetDetailsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", "User")
                        .WithMany("Assets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentAssetsDetails");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Beneficiary", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", "User")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.CashPaymentDetails", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Payment", "Payment")
                        .WithOne("CashDetails")
                        .HasForeignKey("PaymentsBudgetSystem.Data.Entities.CashPaymentDetails", "CashPaymentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.ConsolidatedBudget", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", "User")
                        .WithMany("ConsolidatedBudgets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Employee", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", "User")
                        .WithMany("Employees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.IndividualBudget", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", "User")
                        .WithMany("IndividualBudgets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Payment", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.PaymentAssetsDetails", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Payment", "Payment")
                        .WithOne("AssetsDetails")
                        .HasForeignKey("PaymentsBudgetSystem.Data.Entities.PaymentAssetsDetails", "AssetPaymentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Beneficiary", "Beneficiary")
                        .WithMany("AssetsDetails")
                        .HasForeignKey("BeneficiaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiary");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.PaymentSalaryDetails", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Employee", "Employee")
                        .WithMany("SalaryDetails")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Payment", "Payment")
                        .WithMany("SalariesDetails")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.PaymentSupportDetails", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Beneficiary", "Beneficiary")
                        .WithMany("SupportDetails")
                        .HasForeignKey("BeneficiaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentsBudgetSystem.Data.Entities.Payment", "Payment")
                        .WithOne("SupportDetails")
                        .HasForeignKey("PaymentsBudgetSystem.Data.Entities.PaymentSupportDetails", "SupportPaymentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Beneficiary");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Report", b =>
                {
                    b.HasOne("PaymentsBudgetSystem.Data.Entities.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Beneficiary", b =>
                {
                    b.Navigation("AssetsDetails");

                    b.Navigation("SupportDetails");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Employee", b =>
                {
                    b.Navigation("SalaryDetails");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.Payment", b =>
                {
                    b.Navigation("AssetsDetails")
                        .IsRequired();

                    b.Navigation("CashDetails")
                        .IsRequired();

                    b.Navigation("SalariesDetails");

                    b.Navigation("SupportDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.PaymentAssetsDetails", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("PaymentsBudgetSystem.Data.Entities.User", b =>
                {
                    b.Navigation("Assets");

                    b.Navigation("Beneficiaries");

                    b.Navigation("ConsolidatedBudgets");

                    b.Navigation("Employees");

                    b.Navigation("IndividualBudgets");

                    b.Navigation("Payments");

                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
