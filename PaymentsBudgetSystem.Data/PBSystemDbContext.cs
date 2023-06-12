﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Data
{
    using Data.Entities;

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
                .Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<CashPaymentDetails>()
                .HasOne(c => c.Payment)
                .WithOne(p => p.CashDetails)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<PaymentSalaryDetails>()
                .HasOne(c => c.Payment)
                .WithMany(p => p.SalariesDetails)
                .HasForeignKey(c => c.EmployeeId)
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

            base.OnModelCreating(builder);
        }

        public DbSet<Asset> Assets { get; set; } = null!;

        public DbSet<Beneficiary> Beneficiaries { get; set; } = null!;
        
        public DbSet<CashPaymentDetails> CashPaymentDetails { get; set; } = null!;
        
        public DbSet<ConsolidatedBudget> ConsolidatedBudgets { get; set; } = null!;
        
        public DbSet<Employee> Employees { get; set; } = null!;
        
        public DbSet<GlobalSetting> GlobalSettings { get; set; } = null!;
        
        public DbSet<IndividualBudget> IndividualBudgets { get; set; } = null!;
        
        public DbSet<Message> Messages { get; set; } = null!;
        
        public DbSet<Payment> Payments { get; set; } = null!;
        
        public DbSet<PaymentAssetsDetails> PaymentAssetsDetails { get; set; } = null!;
        
        public DbSet<PaymentSalaryDetails> PaymentSalariesDetails { get; set; } = null!;
        
        public DbSet<PaymentSupportDetails> PaymentSupportDetails { get; set; } = null!;
        
        public DbSet<Report> Reports { get; set; } = null!;
        
        public DbSet<UserFile> UserFiles { get; set; } = null!;

        public DbSet<UserDependancy> UsersDependancies { get; set; } = null!;
    }
}