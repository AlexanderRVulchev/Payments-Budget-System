using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using Entities.Enums;

    internal class PaymentsConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasData(SeedPayments());
        }

        private static List<Payment> SeedPayments()
            => new()
            {
                new Payment
                {
                    Id = Guid.Parse("A8E27C5C-C1AD-471A-563E-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 9746.94m,
                    PaymentType = (PaymentType)1,
                    Date = DateTime.Parse("2023-01-09 10:31:50.6150789"),
                    Paragraph = (ParagraphType)0,
                    Description = "Изплатени заплати за м.1 2023 г.",
                    ReceiverName = "Служители"
                },
                new Payment
                {
                    Id = Guid.Parse("E24BC797-8024-47B9-563F-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 650.00m,
                    PaymentType = (PaymentType)0,
                    Date = DateTime.Parse("2023-01-11 10:34:31.2107875"),
                    Paragraph = (ParagraphType)8,
                    Description = "Командировка гр. Силистра",
                    ReceiverName = "Благовест Колев"
                },
                new Payment
                {
                    Id = Guid.Parse("7B4EC9B1-9022-4C9C-5640-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 25050.00m,
                    PaymentType = (PaymentType)4,
                    Date = DateTime.Parse("2023-01-12 10:38:16.2412308"),
                    Paragraph = (ParagraphType)11,
                    Description = "Закупуване на нови лицензи за работа",
                    ReceiverName = "БГ СИСТЕМС ООД"
                },
                new Payment
                {
                    Id = Guid.Parse("8B7CD7A0-2C74-4C8A-5641-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 1250.00m,
                    PaymentType = (PaymentType)3,
                    Date = DateTime.Parse("2023-01-15 10:39:19.7514929"),
                    Paragraph = (ParagraphType)6,
                    Description = "Закупуване на материали за ремонт на стая 407",
                    ReceiverName = "КОНОВ И СИЕ АД"
                },
                new Payment
                {
                    Id = Guid.Parse("149F280B-F4CF-4E47-5642-08DB804E8F7E"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Amount = 2750.00m,
                    PaymentType = (PaymentType)3,
                    Date = DateTime.Parse("2023-01-21 10:45:13.1580323"),
                    Paragraph = (ParagraphType)7,
                    Description = "Осигуряване на кетъринг и обслужване за конференция в зала 4",
                    ReceiverName = "ДИАНА КЕТЪРИНГ ЕООД"
                },
                new Payment
                {
                    Id = Guid.Parse("0636467D-E3AA-4B48-5643-08DB804E8F7E"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Amount = 22500.00m,
                    PaymentType = (PaymentType)4,
                    Date = DateTime.Parse("2023-01-08 10:49:02.0578480"),
                    Paragraph = (ParagraphType)10,
                    Description = "Закупуване на дисков масив за допълнителен сторидж",
                    ReceiverName = "ЕЛКОМ-БГ ООД"
                },
                new Payment
                {
                    Id = Guid.Parse("C4D57EBC-2BC4-4F70-5644-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 660.00m,
                    PaymentType = (PaymentType)0,
                    Date = DateTime.Parse("2023-02-02 11:02:13.2068344"),
                    Paragraph = (ParagraphType)7,
                    Description = "Почистване и ремонт на служебен автомобил СА1015ВД",
                    ReceiverName = "Евгени Маджаров"
                },
                new Payment
                {
                    Id = Guid.Parse("5C7AA898-9B7E-4F17-5645-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 1200.00m,
                    PaymentType = (PaymentType)3,
                    Date = DateTime.Parse("2023-02-09 11:03:08.0123728"),
                    Paragraph = (ParagraphType)8,
                    Description = "Закупуване на самолетни билети за командировка - Белгия",
                    ReceiverName = "ПОСОКА КОМ ООД"
                },
                new Payment
                {
                    Id = Guid.Parse("27ED4479-0A41-45AD-5646-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 985.00m,
                    PaymentType = (PaymentType)4,
                    Date = DateTime.Parse("2023-02-12 11:04:16.8979321"),
                    Paragraph = (ParagraphType)9,
                    Description = "Обзавеждане на стая 101",
                    ReceiverName = "МЕГАСТОР АД"
                },
                new Payment
                {
                    Id = Guid.Parse("ED64127E-FA2D-4E7D-5647-08DB804E8F7E"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Amount = 14352.64m,
                    PaymentType = (PaymentType)1,
                    Date = DateTime.Parse("2023-02-21 11:05:26.4821122"),
                    Paragraph = (ParagraphType)0,
                    Description = "Изплатени заплати за м.2 2023 г.",
                    ReceiverName = "Служители"
                },
                new Payment
                {
                    Id = Guid.Parse("5C5553A2-662A-41A1-5648-08DB804E8F7E"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Amount = 224.00m,
                    PaymentType = (PaymentType)0,
                    Date = DateTime.Parse("2023-03-09 11:09:43.0955792"),
                    Paragraph = (ParagraphType)7,
                    Description = "Възстановена сума на служител за проведен курс",
                    ReceiverName = "Росица Иванова"
                },
                new Payment
                {
                    Id = Guid.Parse("A26FFA37-40E5-456F-5649-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 5574.15m,
                    PaymentType = (PaymentType)3,
                    Date = DateTime.Parse("2023-03-19 11:11:05.5946825"),
                    Paragraph = (ParagraphType)7,
                    Description = "Електроенергия за м. януари 2023 г.",
                    ReceiverName = "ЕНЕРГО БЪЛГАРИЯ АД"
                },
                new Payment
                {
                    Id = Guid.Parse("ED82C4F5-4C19-44F4-564A-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 3050.40m,
                    PaymentType = (PaymentType)4,
                    Date = DateTime.Parse("2023-04-10 11:12:12.2971277"),
                    Paragraph = (ParagraphType)10,
                    Description = "Мултифункционално у-ство за отдел ЧР",
                    ReceiverName = "И-НЕЙЧЪР ООД"
                },
                new Payment
                {
                    Id = Guid.Parse("0655DCEF-51FC-4A50-564B-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 12500.00m,
                    PaymentType = (PaymentType)3,
                    Date = DateTime.Parse("2023-04-15 11:16:28.9346507"),
                    Paragraph = (ParagraphType)6,
                    Description = "Канцеларски материали за дирекция ЦДА",
                    ReceiverName = "ОФИС КОНСУМАТИВИ АД"
                },
                new Payment
                {
                    Id = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 29094.10m,
                    PaymentType = (PaymentType)1,
                    Date = DateTime.Parse("2023-05-01 11:17:43.6920779"),
                    Paragraph = (ParagraphType)0,
                    Description = "Изплатени заплати за м.5 2023 г.",
                    ReceiverName = "Служители"
                },
                new Payment
                {
                    Id = Guid.Parse("178D9A33-702C-4CAC-564D-08DB804E8F7E"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Amount = 17895.76m,
                    PaymentType = (PaymentType)1,
                    Date = DateTime.Parse("2023-05-30 11:18:24.6065379"),
                    Paragraph = (ParagraphType)0,
                    Description = "Изплатени заплати за м.5 2023 г.",
                    ReceiverName = "Служители"
                },
                new Payment
                {
                    Id = Guid.Parse("4BC8062D-AF15-47FE-564E-08DB804E8F7E"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Amount = 2920.00m,
                    PaymentType = (PaymentType)4,
                    Date = DateTime.Parse("2023-06-02 11:21:58.0947357"),
                    Paragraph = (ParagraphType)9,
                    Description = "Обзавеждане на конферентна зала",
                    ReceiverName = "УНИВЕРС ООД"
                },
                new Payment
                {
                    Id = Guid.Parse("1973AF10-ECD7-415F-564F-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 1650.00m,
                    PaymentType = (PaymentType)3,
                    Date = DateTime.Parse("2023-07-09 11:26:04.2207511"),
                    Paragraph = (ParagraphType)8,
                    Description = "Командировъчни разходи на нач. отдел \"Правен\"",
                    ReceiverName = "ПОСОКА КОМ ООД"
                },
                new Payment
                {
                    Id = Guid.Parse("03B8E961-EFA7-4395-5650-08DB804E8F7E"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Amount = 720.00m,
                    PaymentType = (PaymentType)3,
                    Date = DateTime.Parse("2023-07-09 11:27:00.1913745"),
                    Paragraph = (ParagraphType)7,
                    Description = "Хостинг за сайта на агенцията за 2023 г.",
                    ReceiverName = "БГ СИСТЕМС ООД"
                },
            };
    }
}
