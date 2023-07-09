using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;

    internal class BeneficiariesConfiguration : IEntityTypeConfiguration<Beneficiary>
    {
        public void Configure(EntityTypeBuilder<Beneficiary> builder)
        {
            builder.HasData(SeedBeneficiaries());
        }

        private static List<Beneficiary> SeedBeneficiaries()
            => new()
            {
                new Beneficiary
                {
                    Id = Guid.Parse("17B94784-428A-4B5F-2B21-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "ПОСОКА КОМ ООД",
                    Identifier = "100255356",
                    BankAccount = "BG44STSA56660103409444",
                    Address = "гр. София, ж.к. \"Борово\", ул. \"Пътешествена\" № 16"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("3450AB68-623A-42DE-2B22-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "БГ СИСТЕМС ООД",
                    Identifier = "951774380",
                    BankAccount = "BG48BGSF00001400901551",
                    Address = "гр. София, бул. \"Централен\" № 2"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("9405D63B-25FD-43D3-2B23-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "МАКСИ ЕН ЕООД",
                    Identifier = "952080811",
                    BankAccount = "BG10UNCR56000012305100",
                    Address = "гр. Самоков, ул. \"Генерал Пешев\" № 22"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("D3BD3B31-BD00-4718-2B24-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "СОФИЯ АУТО ЕООД",
                    Identifier = "132237700",
                    BankAccount = "BG60UNCR56770033055000",
                    Address = "гр. София, ж.к. \"Света Тройца\", ул. \"Автомобилна\" № 34"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("DFFB36A4-6882-43FE-2B25-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "ОФИС КОНСУМАТИВИ АД",
                    Identifier = "659012547",
                    BankAccount = "BG63STSA30005104521000",
                    Address = "гр. Пловдив, бул. \"В. Левски\" № 114"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("77B0827E-9AC2-4ABA-2B26-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "ЕТ СПАСОВ - ДИМИТЪР СПАСОВ",
                    Identifier = "000364891",
                    BankAccount = "BG49STSA57860103229469",
                    Address = "гр. София, ж.к. \"Младост 1\", ул. \"Спасовска\" № 1"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("3F4BBA5F-4C3A-4230-2B27-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "СОФИЙСКА ВОДА АД",
                    Identifier = "000358188",
                    BankAccount = "BG46BGSF40040341000635",
                    Address = "гр. София, ж.к. \"Бъкстон\", ул. \"Водна\" № 41"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("E5D7258E-A7B2-46F5-2B28-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "ЕНЕРГО БЪЛГАРИЯ АД",
                    Identifier = "546581074",
                    BankAccount = "BG09BNBG50195710913876",
                    Address = "гр. София, ж.к. \"Лозенец\", ул. \"Петко войвода\" № 11"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("BC317B99-304E-43BA-2B29-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "КОНОВ И СИЕ АД",
                    Identifier = "388489198",
                    BankAccount = "BG01UNCR60209275301978",
                    Address = "гр. Перник, ул. \"инж. Георги Иванов\" № 45"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("052C29CB-B9C6-42E9-2B2A-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "МЕГАСТОР АД",
                    Identifier = "981890789",
                    BankAccount = "BG62UNCR00060012300458",
                    Address = "гр. София, ж.к. \"Обеля\", бл. 259"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("46B32CD0-8754-4B38-2B2B-08DB80453A86"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Name = "И-НЕЙЧЪР ООД",
                    Identifier = "151541387",
                    BankAccount = "BG45BGSF24085234092780",
                    Address = "гр. София, ж.к. \"Надежда 4\", бул. \"Ломско шосе\" № 116"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("3BBAA90D-9C3D-4A84-C5FE-08DB804FEFC7"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Name = "ЕЛКОМ-БГ ООД",
                    Identifier = "462367724",
                    BankAccount = "BG45STSA44472051043877",
                    Address = "гр. София, ж.к. \"Надежда 1\", ул. \"Надежда\" № 15"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("DAEC7016-83D7-4BDB-C5FF-08DB804FEFC7"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Name = "ДИАНА КЕТЪРИНГ ЕООД",
                    Identifier = "455424624",
                    BankAccount = "BG11BNBG30000045010508",
                    Address = "гр. София, ж.к. \"Мусагеница\", ул. \"Панайот Шипков\" № 10"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("5C1DB8B9-76B6-4D89-C600-08DB804FEFC7"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Name = "ЕЛЕКТРО-БГ АД",
                    Identifier = "000100230",
                    BankAccount = "BG66BNBG00061166810461",
                    Address = "гр. София, ж.к. \"Бъкстон\", ул. \"Дечко Делев\" № 40"
                },
                new Beneficiary
                {
                    Id = Guid.Parse("1DDE604D-8BAE-4785-C601-08DB804FEFC7"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Name = "УНИВЕРС ООД",
                    Identifier = "128000031",
                    BankAccount = "BG61STSA00000910004134",
                    Address = "гр. Кърджали, ул. \"Цар Симеон\" 4A"
                }
            };
    }
}
