﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;

    internal class ReportsConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasData(SeedReports());
        }

        private static List<Report> SeedReports()
            => new()
            {
                new Report
                {
                    Id = Guid.Parse("D0016FD5-368F-423E-AD6C-08DB80565E8F"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Year = 2023,
                    Month = 6,
                    IsConsolidated = false,
                    Bank0101 = 13403.91m,
                    Bank0102 = 10779.68m,
                    Transfer0551 = 7217.88m,
                    Transfer0560 = 2376.26m,
                    Transfer0580 = 1485.16m,
                    Transfer0590 = 2687.07m,
                    Bank1015 = 13750.00m,
                    Cash1015 = 0m,
                    Bank1020 = 5574.15m,
                    Cash1020 = 660.00m,
                    Cash1051 = 650.00m,
                    Bank1051 = 1200.00m,
                    Bank5100 = 985.00m,
                    Bank5200 = 3050.40m,
                    Bank5300 = 25050.00m,
                    LimitSalaries = 800000.00m,
                    LimitSupport = 400000.00m,
                    LimitAssets = 200000.00m,
                },
                new Report
                {
                    Id = Guid.Parse("E8CF571F-6486-4CFD-AD6D-08DB80565E8F"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Year = 2023,
                    Month = 5,
                    IsConsolidated = false,
                    Bank0101 = 13403.91m,
                    Bank0102 = 10779.68m,
                    Transfer0551 = 7217.88m,
                    Transfer0560 = 2376.26m,
                    Transfer0580 = 1485.16m,
                    Transfer0590 = 2687.07m,
                    Bank1015 = 13750.00m,
                    Cash1015 = 0m,
                    Bank1020 = 5574.15m,
                    Cash1020 = 660.00m,
                    Cash1051 = 650.0m,
                    Bank1051 = 1200.00m,
                    Bank5100 = 985.00m,
                    Bank5200 = 3050.40m,
                    Bank5300 = 25050.0m,
                    LimitSalaries = 800000.00m,
                    LimitSupport = 400000.0m,
                    LimitAssets = 200000m,
                },
                new Report
                {
                    Id = Guid.Parse("1E0EC52D-3294-4FCD-AD6E-08DB80565E8F"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Year = 2023,
                    Month = 5,
                    IsConsolidated = false,
                    Bank0101 = 7599.61m,
                    Bank0102 = 12672.0m,
                    Transfer0551 = 5863.59m,
                    Transfer0560 = 1930.40m,
                    Transfer0580 = 1206.50m,
                    Transfer0590 = 2252.40m,
                    Bank1015 = 0m,
                    Cash1015 = 0m,
                    Bank1020 = 2750.00m,
                    Cash1020 = 224.00m,
                    Cash1051 = 0m,
                    Bank1051 = 0m,
                    Bank5100 = 0m,
                    Bank5200 = 22500.0m,
                    Bank5300 = 0m,
                    LimitSalaries = 2000000.0m,
                    LimitSupport = 700000.0m,
                    LimitAssets = 250000.0m,
                },
                new Report
                {
                    Id = Guid.Parse("4CF49743-8E93-4AEA-AD6F-08DB80565E8F"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Year = 2023,
                    Month = 6,
                    IsConsolidated = false,
                    Bank0101 = 7599.61m,
                    Bank0102 = 12672.00m,
                    Transfer0551 = 5863.59m,
                    Transfer0560 = 1930.40m,
                    Transfer0580 = 1206.50m,
                    Transfer0590 = 2252.40m,
                    Bank1015 = 0m,
                    Cash1015 = 0m,
                    Bank1020 = 2750.00m,
                    Cash1020 = 224.00m,
                    Cash1051 = 0m,
                    Bank1051 = 0m,
                    Bank5100 = 2920.0m,
                    Bank5200 = 22500.0m,
                    Bank5300 = 0m,
                    LimitSalaries = 2000000.0m,
                    LimitSupport = 700000.0m,
                    LimitAssets = 250000.0m,
                },
                new Report
                {
                    Id = Guid.Parse("35BE5043-EA75-4FE2-AD70-08DB80565E8F"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Year = 2023,
                    Month = 5,
                    IsConsolidated = true,
                    Bank0101 = 21003.52m,
                    Bank0102 = 23451.68m,
                    Transfer0551 = 13081.47m,
                    Transfer0560 = 4386.66m,
                    Transfer0580 = 2691.66m,
                    Transfer0590 = 4939.47m,
                    Bank1015 = 13750.0m,
                    Cash1015 = 0m,
                    Bank1020 = 8324.15m,
                    Cash1020 = 884.0m,
                    Cash1051 = 650.0m,
                    Bank1051 = 1200.0m,
                    Bank5100 = 985.0m,
                    Bank5200 = 25550.40m,
                    Bank5300 = 25050.00m,
                    LimitSalaries = 3100000.0m,
                    LimitSupport = 1250000.0m,
                    LimitAssets = 525000.0m,
                },
                new Report
                {
                    Id = Guid.Parse("C881B1B5-F1A3-4BFB-AD71-08DB80565E8F"),
                    UserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    Year = 2023,
                    Month = 6,
                    IsConsolidated = true,
                    Bank0101 = 21003.52m,
                    Bank0102 = 23451.68m,
                    Transfer0551 = 13081.47m,
                    Transfer0560 = 4306.66m,
                    Transfer0580 = 2691.66m,
                    Transfer0590 = 4939.47m,
                    Bank1015 = 13750.00m,
                    Cash1015 = 0m,
                    Bank1020 = 8324.15m,
                    Cash1020 = 884.00m,
                    Cash1051 = 650.00m,
                    Bank1051 = 1200.0m,
                    Bank5100 = 3905.00m,
                    Bank5200 = 25550.40m,
                    Bank5300 = 25050.00m,
                    LimitSalaries = 3100000.0m,
                    LimitSupport = 1250000.0m,
                    LimitAssets = 525000.0m,
                },
                new Report
                {
                    Id = Guid.Parse("CBD747BA-528C-45A9-AD72-08DB80565E8F"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Year = 2023,
                    Month = 4,
                    IsConsolidated = false,
                    Bank0101 = 5179.84m,
                    Bank0102 = 789.68m,
                    Transfer0551 = 1877.76m,
                    Transfer0560 = 618.19m,
                    Transfer0580 = 386.37m,
                    Transfer0590 = 663.28m,
                    Bank1015 = 13750.0m,
                    Cash1015 = 0m,
                    Bank1020 = 5574.15m,
                    Cash1020 = 660.00m,
                    Cash1051 = 650.00m,
                    Bank1051 = 1200.00m,
                    Bank5100 = 985.00m,
                    Bank5200 = 3050.40m,
                    Bank5300 = 25050.00m,
                    LimitSalaries = 800000.0m,
                    LimitSupport = 400000.0m,
                    LimitAssets = 200000.0m,
                },
                new Report
                {
                    Id = Guid.Parse("3F810F3C-EE75-40EB-AD73-08DB80565E8F"),
                    UserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                    Year = 2023,
                    Month = 3,
                    IsConsolidated = false,
                    Bank0101 = 5179.84m,
                    Bank0102 = 789.68m,
                    Transfer0551 = 1877.76m,
                    Transfer0560 = 618.19m,
                    Transfer0580 = 386.37m,
                    Transfer0590 = 663.28m,
                    Bank1015 = 1250.0m,
                    Cash1015 = 0m,
                    Bank1020 = 5574.15m,
                    Cash1020 = 660.0m,
                    Cash1051 = 650.0m,
                    Bank1051 = 1200.0m,
                    Bank5100 = 985.0m,
                    Bank5200 = 0m,
                    Bank5300 = 25050.0m,
                    LimitSalaries = 800000.0m,
                    LimitSupport = 400000.0m,
                    LimitAssets = 200000.0m,
                }
            };
    }
}
