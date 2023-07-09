using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using System;

    internal class PaymentAssetDetailsConfiguration : IEntityTypeConfiguration<PaymentAssetsDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentAssetsDetails> builder)
        {
            builder.HasData(SeedPaymentAssetDetails());
        }

        private static List<PaymentAssetsDetails> SeedPaymentAssetDetails()
            => new()
            {
                new PaymentAssetsDetails
                {
                    AssetPaymentId = Guid.Parse("7B4EC9B1-9022-4C9C-5640-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("3450AB68-623A-42DE-2B22-08DB80453A86"),
                    InvoiceNumber = "0005189987",
                    InvoiceDate = DateTime.Parse("2022-12-12 00:00:00.0000000"),
                    DeliveryDate = DateTime.Parse("2023-01-12 10:38:16.2412308")
                },
                new PaymentAssetsDetails
                {
                    AssetPaymentId = Guid.Parse("0636467D-E3AA-4B48-5643-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("3BBAA90D-9C3D-4A84-C5FE-08DB804FEFC7"),
                    InvoiceNumber = null,
                    InvoiceDate = null,
                    DeliveryDate = DateTime.Parse("2023-01-08 10:49:02.0578480")
                },
                new PaymentAssetsDetails
                {
                    AssetPaymentId = Guid.Parse("27ED4479-0A41-45AD-5646-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("052C29CB-B9C6-42E9-2B2A-08DB80453A86"),
                    InvoiceNumber = "0000004521",
                    InvoiceDate = DateTime.Parse("2022-11-14 00:00:00.0000000"),
                    DeliveryDate = DateTime.Parse("2023-02-12 11:04:16.8979321")
                },
                new PaymentAssetsDetails
                {
                    AssetPaymentId = Guid.Parse("ED82C4F5-4C19-44F4-564A-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("46B32CD0-8754-4B38-2B2B-08DB80453A86"),
                    InvoiceNumber = null,
                    InvoiceDate = null,
                    DeliveryDate = DateTime.Parse("2023-04-10 11:12:12.2971277")
                },
                new PaymentAssetsDetails
                {
                    AssetPaymentId = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                    BeneficiaryId = Guid.Parse("1DDE604D-8BAE-4785-C601-08DB804FEFC7"),
                    InvoiceNumber = "1000602000",
                    InvoiceDate = DateTime.Parse("2023-05-05 00:00:00.0000000"),
                    DeliveryDate = DateTime.Parse("2023-06-02 11:21:58.0947357")
                },
            };
    }
}
