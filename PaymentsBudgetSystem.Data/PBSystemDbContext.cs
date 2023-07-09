using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Data
{
    using Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using PaymentsBudgetSystem.Data.Configuration;

    public class PBSystemDbContext : IdentityDbContext<User>
    {
        public PBSystemDbContext(DbContextOptions<PBSystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<UserDependancy>()
                .HasKey(ud => new { ud.PrimaryUserId, ud.SecondaryUserId });

            builder
                .Entity<CashPaymentDetails>()
                .HasOne(c => c.Payment)
                .WithOne(p => p.CashDetails)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<PaymentSalaryDetails>()
                .HasOne(c => c.Payment)
                .WithMany(p => p.SalariesDetails)
                .HasForeignKey(c => c.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<PaymentSupportDetails>()
                .HasOne(c => c.Payment)
                .WithOne(p => p.SupportDetails)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<PaymentAssetsDetails>()
                .HasOne(c => c.Payment)
                .WithOne(p => p.AssetsDetails)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Asset>()
                .HasOne(a => a.PaymentAssetsDetails)
                .WithMany(p => p.Assets)
                .HasForeignKey(a => a.PaymentAssetDetailsId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new GlobalSettingsConfiguration());
            builder.ApplyConfiguration(new ConsolidatedBudgetsConfiguration());
            builder.ApplyConfiguration(new IndividualBudgetsConfiguration());
            builder.ApplyConfiguration(new UsersDependanciesConfiguration());
            builder.ApplyConfiguration(new ReportsConfiguration());
            builder.ApplyConfiguration(new PaymentsConfiguration());
            builder.ApplyConfiguration(new BeneficiariesConfiguration());
            builder.ApplyConfiguration(new EmployeesConfiguration());
            builder.ApplyConfiguration(new CashPaymentDetailsConfiguration());
            builder.ApplyConfiguration(new PaymentAssetDetailsConfiguration());
            builder.ApplyConfiguration(new PaymentSalariesDetailsConfiguration());
            builder.ApplyConfiguration(new PaymentSupportDetailsConfiguration());
            builder.ApplyConfiguration(new AssetsConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Asset> Assets { get; set; } = null!;

        public DbSet<Beneficiary> Beneficiaries { get; set; } = null!;

        public DbSet<CashPaymentDetails> CashPaymentDetails { get; set; } = null!;

        public DbSet<ConsolidatedBudget> ConsolidatedBudgets { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<GlobalSetting> GlobalSettings { get; set; } = null!;

        public DbSet<IndividualBudget> IndividualBudgets { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;

        public DbSet<PaymentAssetsDetails> PaymentAssetsDetails { get; set; } = null!;

        public DbSet<PaymentSalaryDetails> PaymentSalariesDetails { get; set; } = null!;

        public DbSet<PaymentSupportDetails> PaymentSupportDetails { get; set; } = null!;

        public DbSet<Report> Reports { get; set; } = null!;

        public DbSet<UserDependancy> UsersDependancies { get; set; } = null!;
    }
}