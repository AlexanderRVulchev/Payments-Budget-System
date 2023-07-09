using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using System;

    internal class ConsolidatedBudgetsConfiguration : IEntityTypeConfiguration<ConsolidatedBudget>
    {
        public void Configure(EntityTypeBuilder<ConsolidatedBudget> builder)
        {
            builder.HasData(SeedConsolidatedBudgets());
        }

        private static List<ConsolidatedBudget> SeedConsolidatedBudgets()
            => new()
            {
                new ConsolidatedBudget
                {
                    Id = Guid.Parse("0315E49B-3866-4F17-62A9-08DB804A51C9"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    FiscalYear = 2023,
                    TotalLimit = 5000000.00m
                }
            };
    }
}
