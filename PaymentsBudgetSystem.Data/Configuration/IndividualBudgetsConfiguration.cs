using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;

    internal class IndividualBudgetsConfiguration : IEntityTypeConfiguration<IndividualBudget>
    {
        public void Configure(EntityTypeBuilder<IndividualBudget> builder)
        {
            builder.HasData(SeedIndividualBudgets());
        }

        private static List<IndividualBudget> SeedIndividualBudgets()
            => new()
            {
                new IndividualBudget
                {
                    Id = Guid.Parse("65490B49-1929-4323-A557-08DB804A51CA"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    FiscalYear = 2023,
                    SalariesLimit = 2000000,
                    SupportLimit = 700000,
                    AssetsLimit = 250000,
                },
                new IndividualBudget
                {
                    Id = Guid.Parse("6AD66F5A-A320-49D2-A558-08DB804A51CA"),
                    UserId = "33fb1d42-a747-4860-b3ce-7e33a0421a0d",
                    FiscalYear = 2023,
                    SalariesLimit = 300000,
                    SupportLimit = 150000,
                    AssetsLimit = 75000,
                },
                new IndividualBudget
                {
                    Id = Guid.Parse("BDDB96E8-EA21-4391-A559-08DB804A51CA"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FiscalYear = 2023,
                    SalariesLimit = 800000,
                    SupportLimit = 400000,
                    AssetsLimit = 200000,
                }
            };
    }
}
