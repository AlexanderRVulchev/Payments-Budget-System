using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using Entities.Enums;

    internal class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(SeedEmployees());
        }

        private static List<Employee> SeedEmployees()
            => new()
            {
                new Employee
                {
                    Id = Guid.Parse("9B2C9857-A2FE-46F5-CBE3-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Димитър",
                    LastName = "Андонов",
                    Egn = "0205123565",
                    MonthlySalary = 1850.00m,
                    DateEmployed = DateTime.Parse("2022-12-14 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("FB8E17F1-1324-4B42-CBE4-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Илияна",
                    LastName = "Атанасова",
                    Egn = "8211307712",
                    MonthlySalary = 2600.00m,
                    DateEmployed = DateTime.Parse("2023-02-05 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)1
                },
                new Employee
                {
                    Id = Guid.Parse("56495A09-76B4-4828-CBE5-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Лиана",
                    LastName = "Михайлова",
                    Egn = "7906125598",
                    MonthlySalary = 2200.00m,
                    DateEmployed = DateTime.Parse("2023-01-01 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("5768E56A-8D2F-4EA7-CBE6-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Благовест",
                    LastName = "Колев",
                    Egn = "8109183040",
                    MonthlySalary = 1550.00m,
                    DateEmployed = DateTime.Parse("2022-10-09 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("87008E93-86B0-43D4-CBE7-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Евгени",
                    LastName = "Маджаров",
                    Egn = "7405162258",
                    MonthlySalary = 3200.00m,
                    DateEmployed = DateTime.Parse("2023-03-14 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)1
                },
                new Employee
                {
                    Id = Guid.Parse("951DFE17-3ED4-4EE8-CBE8-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Диана",
                    LastName = "Атанасова-Димчева",
                    Egn = "9405051195",
                    MonthlySalary = 1800.00m,
                    DateEmployed = DateTime.Parse("2023-05-04 00:00:00.0000000"),
                    DateLeft = DateTime.Parse("2023-08-06 00:00:00.0000000"),
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("1EA971BA-1DA9-4AF5-CBE9-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Любомир",
                    LastName = "Бацанов",
                    Egn = "0112164080",
                    MonthlySalary = 2400.00m,
                    DateEmployed = DateTime.Parse("2023-04-04 00:00:00.0000000"),
                    DateLeft = DateTime.Parse("2023-08-31 00:00:00.0000000"),
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("08B60D14-DA17-45C7-CBEA-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Станимир",
                    LastName = "Кьосев",
                    Egn = "6907226568",
                    MonthlySalary = 1250.00m,
                    DateEmployed = DateTime.Parse("2022-05-08 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("62C88F1B-19A2-4ADB-CBEB-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Михаил",
                    LastName = "Тодораков",
                    Egn = "6612154400",
                    MonthlySalary = 4000.00m,
                    DateEmployed = DateTime.Parse("2023-06-06 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("F7681106-88F0-4E13-CBEC-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Спаска",
                    LastName = "Кротева",
                    Egn = "7505301516",
                    MonthlySalary = 1100.00m,
                    DateEmployed = DateTime.Parse("2023-02-02 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)1
                },
                new Employee
                {
                    Id = Guid.Parse("AA78E286-FCA3-42D2-CBED-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Момчил",
                    LastName = "Крайски",
                    Egn = "8409136080",
                    MonthlySalary = 2600.00m,
                    DateEmployed = DateTime.Parse("2023-04-03 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)1
                },
                new Employee
                {
                    Id = Guid.Parse("57E51B62-48FD-408F-CBEE-08DB80474B75"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    FirstName = "Цветелина",
                    LastName = "Иванова",
                    Egn = "9801027780",
                    MonthlySalary = 1600.00m,
                    DateEmployed = DateTime.Parse("2023-01-15 00:00:00.0000000"),
                    DateLeft = DateTime.Parse("2023-06-21 00:00:00.0000000"),
                    ContractType = (ContractType)1
                },
                new Employee
                {
                    Id = Guid.Parse("21D55765-5AFB-412F-CBEF-08DB80474B75"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    FirstName = "Александър",
                    LastName = "Несторов",
                    Egn = "8205015066",
                    MonthlySalary = 3600.00m,
                    DateEmployed = DateTime.Parse("2022-08-06 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("A9A85601-7847-4872-CBF0-08DB80474B75"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    FirstName = "Ивелина",
                    LastName = "Шопова",
                    Egn = "7511136065",
                    MonthlySalary = 4700.00m,
                    DateEmployed = DateTime.Parse("2023-01-12 00:00:00.0000000"),
                    DateLeft = DateTime.Parse("2023-07-04 00:00:00.0000000"),
                    ContractType = (ContractType)1
                },
                new Employee
                {
                    Id = Guid.Parse("0D09C40D-943A-4AC2-CBF1-08DB80474B75"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    FirstName = "Цветомир",
                    LastName = "Касабов",
                    Egn = "9405053536",
                    MonthlySalary = 2850.00m,
                    DateEmployed = DateTime.Parse("2023-03-03 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)0
                },
                new Employee
                {
                    Id = Guid.Parse("D7D70D4C-CA53-4D05-CBF2-08DB80474B75"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    FirstName = "Росица",
                    LastName = "Иванова",
                    Egn = "9211257005",
                    MonthlySalary = 2340.00m,
                    DateEmployed = DateTime.Parse("2022-12-15 00:00:00.0000000"),
                    DateLeft = null,
                    ContractType = (ContractType)1
                },
            };
    }
}
