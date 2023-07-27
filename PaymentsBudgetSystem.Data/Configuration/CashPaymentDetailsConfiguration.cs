using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using System;

    internal class CashPaymentDetailsConfiguration : IEntityTypeConfiguration<CashPaymentDetails>
    {
        public void Configure(EntityTypeBuilder<CashPaymentDetails> builder)
        {
            builder
                .HasOne(c => c.Payment)
                .WithOne(p => p.CashDetails)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(SeedCashPaymentDetails());
        }

        private static List<CashPaymentDetails> SeedCashPaymentDetails()
            => new()
            {
                new CashPaymentDetails
                {
                    CashPaymentId = Guid.Parse("E24BC797-8024-47B9-563F-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("5768E56A-8D2F-4EA7-CBE6-08DB80474B75"),
                    CashOrderNumber = 1,
                    CashOrderDate = DateTime.Parse("2023-07-09 10:34:31.2108796"),
                },
                new CashPaymentDetails
                {
                    CashPaymentId = Guid.Parse("C4D57EBC-2BC4-4F70-5644-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("87008E93-86B0-43D4-CBE7-08DB80474B75"),
                    CashOrderNumber = 2,
                    CashOrderDate = DateTime.Parse("2023-07-09 11:02:13.2068371"),
                },
                new CashPaymentDetails
                {
                    CashPaymentId = Guid.Parse("5C5553A2-662A-41A1-5648-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("D7D70D4C-CA53-4D05-CBF2-08DB80474B75"),
                    CashOrderNumber = 15,
                    CashOrderDate = DateTime.Parse("2023-07-09 11:09:43.0955819"),
                },
            };
    }
}
