using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using Entities.Enums;

    internal class AssetsConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder
                .HasOne(a => a.PaymentAssetsDetails)
                .WithMany(p => p.Assets)
                .HasForeignKey(a => a.PaymentAssetDetailsId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(SeedAssets());
        }

        private static List<Asset> SeedAssets()
            => new()
            {
                new Asset
                {
                    Id = Guid.Parse("89E250B8-9453-4099-0FC0-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-01-12 10:38:16.2412308"),
                    Type = (ParagraphType)11,
                    ReportValue = 350.00m,
                    Description = "Windows 10 Enterprise",
                    PaymentAssetDetailsId = Guid.Parse("7B4EC9B1-9022-4C9C-5640-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("90C86545-107A-4182-0FC1-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-01-12 10:38:16.2412308"),
                    Type = (ParagraphType)11,
                    ReportValue = 350.00m,
                    Description = "Windows 10 Enterprise",
                    PaymentAssetDetailsId = Guid.Parse("7B4EC9B1-9022-4C9C-5640-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("A4823C15-8389-4842-0FC2-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-01-12 10:38:16.2412308"),
                    Type = (ParagraphType)11,
                    ReportValue = 350.00m,
                    Description = "Windows 10 Enterprise",
                    PaymentAssetDetailsId = Guid.Parse("7B4EC9B1-9022-4C9C-5640-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("3EF2A82F-DA80-427E-0FC3-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-01-12 10:38:16.2412308"),
                    Type = (ParagraphType)11,
                    ReportValue = 24000.00m,
                    Description = "MS SQL Server Enterprise",
                    PaymentAssetDetailsId = Guid.Parse("7B4EC9B1-9022-4C9C-5640-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("2D0B34C6-DB91-4E5A-0FC4-08DB804F7559"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    DateAquired = DateTime.Parse("2023-01-08 10:49:02.0578480"),
                    Type = (ParagraphType)10,
                    ReportValue = 22500.00m,
                    Description = "Дисков масив QSAN SMB",
                    PaymentAssetDetailsId = Guid.Parse("0636467D-E3AA-4B48-5643-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("2D40DD08-22BF-4EC1-0FC5-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-02-12 11:04:16.8979321"),
                    Type = (ParagraphType)9,
                    ReportValue = 220.00m,
                    Description = "Офис стол",
                    PaymentAssetDetailsId = Guid.Parse("27ED4479-0A41-45AD-5646-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("F2249732-90F1-4793-0FC6-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-02-12 11:04:16.8979321"),
                    Type = (ParagraphType)9,
                    ReportValue = 220.00m,
                    Description = "Офис стол",
                    PaymentAssetDetailsId = Guid.Parse("27ED4479-0A41-45AD-5646-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("7E806068-961A-4D44-0FC7-08DB804F7559"), 
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-02-12 11:04:16.8979321"),
                    Type = (ParagraphType)9,
                    ReportValue = 220.00m,
                    Description = "Офис стол",
                    PaymentAssetDetailsId = Guid.Parse("27ED4479-0A41-45AD-5646-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("9FCE69A3-6E35-4B3D-0FC8-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-02-12 11:04:16.8979321"),
                    Type = (ParagraphType)9,
                    ReportValue = 325.00m,
                    Description = "Бюро",
                    PaymentAssetDetailsId = Guid.Parse("27ED4479-0A41-45AD-5646-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("A270F15D-1520-425A-0FC9-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-04-10 11:12:12.2971277"),
                    Type = (ParagraphType)10,
                    ReportValue = 1525.20m,
                    Description = "МФУ Brother",
                    PaymentAssetDetailsId = Guid.Parse("ED82C4F5-4C19-44F4-564A-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("DA5A56FA-D860-400E-0FCA-08DB804F7559"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    DateAquired = DateTime.Parse("2023-04-10 11:12:12.2971277"),
                    Type = (ParagraphType)10,
                    ReportValue = 1525.20m,
                    Description = "МФУ Brother",
                    PaymentAssetDetailsId = Guid.Parse("ED82C4F5-4C19-44F4-564A-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("E8045A2C-7D67-40CE-0FCB-08DB804F7559"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    DateAquired = DateTime.Parse("2023-06-02 11:21:58.0947357"),
                    Type = (ParagraphType)9,
                    ReportValue = 1220.00m,
                    Description = "Конферентна маса",
                    PaymentAssetDetailsId = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("0EB104D0-C006-4FBC-0FCC-08DB804F7559"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    DateAquired = DateTime.Parse("2023-06-02 11:21:58.0947357"),
                    Type = (ParagraphType)9,
                    ReportValue = 1220.00m,
                    Description = "Конферентна маса",
                    PaymentAssetDetailsId = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("0D546759-7099-4215-0FCD-08DB804F7559"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    DateAquired = DateTime.Parse("2023-06-02 11:21:58.0947357"),
                    Type = (ParagraphType)9,
                    ReportValue = 120.00m,
                    Description = "Посетителски стол",
                    PaymentAssetDetailsId = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("A0369632-8AFF-411F-0FCE-08DB804F7559"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    DateAquired = DateTime.Parse("2023-06-02 11:21:58.0947357"),
                    Type = (ParagraphType)9,
                    ReportValue = 120.00m,
                    Description = "Посетителски стол",
                    PaymentAssetDetailsId = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("9B623B4F-8FB0-425C-0FCF-08DB804F7559"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    DateAquired = DateTime.Parse("2023-06-02 11:21:58.0947357"),
                    Type = (ParagraphType)9,
                    ReportValue = 120.00m,
                    Description = "Посетителски стол",
                    PaymentAssetDetailsId = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                },
                new Asset
                {
                    Id = Guid.Parse("7B6ACFE4-941F-4CF4-0FD0-08DB804F7559"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    DateAquired = DateTime.Parse("2023-06-02 11:21:58.0947357"),
                    Type = (ParagraphType)9,
                    ReportValue = 120.00m,
                    Description = "Посетителски стол",
                    PaymentAssetDetailsId = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                },
            };
    }
}
