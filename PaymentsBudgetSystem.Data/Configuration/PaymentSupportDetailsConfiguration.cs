using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using System;

    internal class PaymentSupportDetailsConfiguration : IEntityTypeConfiguration<PaymentSupportDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentSupportDetails> builder)
        {
            builder
              .HasOne(c => c.Payment)
              .WithOne(p => p.SupportDetails)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(SeedPaymentSupportDetails());
        }

        private static List<PaymentSupportDetails> SeedPaymentSupportDetails()
            => new()
            {
                new PaymentSupportDetails
                {
                    SupportPaymentId = Guid.Parse("8B7CD7A0-2C74-4C8A-5641-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("BC317B99-304E-43BA-2B29-08DB80453A86"),
                    InvoiceNumber = "0000000221",
                    InvoiceDate = DateTime.Parse("2023-01-01 00:00:00.0000000")
                },
                new PaymentSupportDetails
                {
                    SupportPaymentId = Guid.Parse("149F280B-F4CF-4E47-5642-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("DAEC7016-83D7-4BDB-C5FF-08DB804FEFC7"),
                    InvoiceNumber = "1000602346",
                    InvoiceDate = DateTime.Parse("2022-12-30 00:00:00.0000000")
                },
                new PaymentSupportDetails
                {
                    SupportPaymentId = Guid.Parse("5C7AA898-9B7E-4F17-5645-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("17B94784-428A-4B5F-2B21-08DB80453A86"),
                    InvoiceNumber = "0100000576",
                    InvoiceDate = DateTime.Parse("2023-01-20 00:00:00.0000000")
                },
                new PaymentSupportDetails
                {
                    SupportPaymentId = Guid.Parse("A26FFA37-40E5-456F-5649-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("E5D7258E-A7B2-46F5-2B28-08DB80453A86"),
                    InvoiceNumber = "9180795109",
                    InvoiceDate = DateTime.Parse("2023-01-31 00:00:00.0000000")
                },
                new PaymentSupportDetails
                {
                    SupportPaymentId = Guid.Parse("0655DCEF-51FC-4A50-564B-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("DFFB36A4-6882-43FE-2B25-08DB80453A86"),
                    InvoiceNumber = "6684894894",
                    InvoiceDate = DateTime.Parse("2023-04-04 00:00:00.0000000")
                },
                new PaymentSupportDetails
                {
                    SupportPaymentId = Guid.Parse("1973AF10-ECD7-415F-564F-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("17B94784-428A-4B5F-2B21-08DB80453A86"),
                    InvoiceNumber = "5000050684",
                    InvoiceDate = DateTime.Parse("2023-05-24 00:00:00.0000000")
                },
                new PaymentSupportDetails
                {
                    SupportPaymentId = Guid.Parse("03B8E961-EFA7-4395-5650-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("3450AB68-623A-42DE-2B22-08DB80453A86"),
                    InvoiceNumber = "0000054848",
                    InvoiceDate = DateTime.Parse("2023-06-01 00:00:00.0000000")
                },
            };
    }
}
