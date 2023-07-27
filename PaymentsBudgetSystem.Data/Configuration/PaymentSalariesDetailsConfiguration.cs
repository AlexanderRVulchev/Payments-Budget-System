﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;

    internal class PaymentSalariesDetailsConfiguration : IEntityTypeConfiguration<PaymentSalaryDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentSalaryDetails> builder)
        {
            builder
                .HasOne(c => c.Payment)
                .WithMany(p => p.SalariesDetails)
                .HasForeignKey(c => c.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(SeedPaymentSalariesDetails());
        }

        private static List<PaymentSalaryDetails> SeedPaymentSalariesDetails()
            => new()
            {
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("E5DF6BE6-7256-4EAA-7415-08DB804E8F82"),
                    PaymentId = Guid.Parse("A8E27C5C-C1AD-471A-563E-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("9B2C9857-A2FE-46F5-CBE3-08DB80474B75"),
                    NetSalaryJobContract = 1398.93m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 253.82m,
                    InsurancePensionEmployee = 195.73m,
                    InsuranceHealthEmployer = 88.80m,
                    InsuranceHealthEmployee = 59.20m,
                    InsuranceAdditionalEmployer = 51.80m,
                    InsuranceAdditionalEmployee = 40.70m,
                    IncomeTax = 155.44m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("A800F7AA-DE8D-48A4-7416-08DB804E8F82"),
                    PaymentId = Guid.Parse("A8E27C5C-C1AD-471A-563E-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("56495A09-76B4-4828-CBE5-08DB80474B75"),
                    NetSalaryJobContract = 1663.60m,
                    NetSalaryStateOfficial = 0.00m,
                    InsurancePensionEmployer = 301.84m,
                    InsurancePensionEmployee = 232.76m,
                    InsuranceHealthEmployer = 105.60m,
                    InsuranceHealthEmployee = 70.40m,
                    InsuranceAdditionalEmployer = 61.60m,
                    InsuranceAdditionalEmployee = 48.40m,
                    IncomeTax = 184.84m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("0A7F2190-3BEC-45A2-7417-08DB804E8F82"),
                    PaymentId = Guid.Parse("A8E27C5C-C1AD-471A-563E-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("5768E56A-8D2F-4EA7-CBE6-08DB80474B75"),
                    NetSalaryJobContract = 1172.08m,
                    NetSalaryStateOfficial = 0.00m,
                    InsurancePensionEmployer = 212.66m,
                    InsurancePensionEmployee = 163.99m,
                    InsuranceHealthEmployer = 74.40m,
                    InsuranceHealthEmployee = 49.60m,
                    InsuranceAdditionalEmployer = 43.40m,
                    InsuranceAdditionalEmployee = 34.10m,
                    IncomeTax = 130.23m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("CCBDDDC2-8332-4D6C-7418-08DB804E8F82"),
                    PaymentId = Guid.Parse("A8E27C5C-C1AD-471A-563E-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("08B60D14-DA17-45C7-CBEA-08DB80474B75"),
                    NetSalaryJobContract = 945.23m,
                    NetSalaryStateOfficial = 0.00m,
                    InsurancePensionEmployer = 171.50m,
                    InsurancePensionEmployee = 132.25m,
                    InsuranceHealthEmployer = 60.00m,
                    InsuranceHealthEmployee = 40.00m,
                    InsuranceAdditionalEmployer = 35.00m,
                    InsuranceAdditionalEmployee = 27.50m,
                    IncomeTax = 105.03m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("525F62B7-690A-4736-7419-08DB804E8F82"),
                    PaymentId = Guid.Parse("A8E27C5C-C1AD-471A-563E-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("57E51B62-48FD-408F-CBEE-08DB80474B75"),
                    NetSalaryJobContract = 0.00m,
                    NetSalaryStateOfficial = 789.68m,
                    InsurancePensionEmployer = 213.21m,
                    InsurancePensionEmployee = 0.00m,
                    InsuranceHealthEmployer = 70.19m,
                    InsuranceHealthEmployee = 0.00m,
                    InsuranceAdditionalEmployer = 43.87m,
                    InsuranceAdditionalEmployee = 0.00m,
                    IncomeTax = 87.74m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("3211970A-7F4F-4750-741A-08DB804E8F82"),
                    PaymentId = Guid.Parse("ED64127E-FA2D-4E7D-5647-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("21D55765-5AFB-412F-CBEF-08DB80474B75"),
                    NetSalaryJobContract = 2722.25m,
                    NetSalaryStateOfficial = 0.00m,
                    InsurancePensionEmployer = 493.92m,
                    InsurancePensionEmployee = 380.88m,
                    InsuranceHealthEmployer = 172.80m,
                    InsuranceHealthEmployee = 115.20m,
                    InsuranceAdditionalEmployer = 100.80m,
                    InsuranceAdditionalEmployee = 79.20m,
                    IncomeTax = 302.47m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("D2522C0C-72BF-48E7-741B-08DB804E8F82"),
                    PaymentId = Guid.Parse("ED64127E-FA2D-4E7D-5647-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("A9A85601-7847-4872-CBF0-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 4230.00m,
                    InsurancePensionEmployer = 1142.10m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 376.0m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 235.00m,
                    InsuranceAdditionalEmployee = 0.00m,
                    IncomeTax = 470.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("7B9A2403-61AF-4EF8-741C-08DB804E8F82"),
                    PaymentId = Guid.Parse("ED64127E-FA2D-4E7D-5647-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("D7D70D4C-CA53-4D05-CBF2-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 2106.00m,
                    InsurancePensionEmployer = 568.62m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 187.20m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 117.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 234.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("4C1ED72A-AFA5-406A-741D-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("9B2C9857-A2FE-46F5-CBE3-08DB80474B75"),
                    NetSalaryJobContract = 1398.93m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 253.82m,
                    InsurancePensionEmployee = 195.73m,
                    InsuranceHealthEmployer = 88.80m,
                    InsuranceHealthEmployee = 59.20m,
                    InsuranceAdditionalEmployer = 51.80m,
                    InsuranceAdditionalEmployee = 40.70m,
                    IncomeTax = 155.44m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("CC4520C2-5D8D-4760-741E-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("FB8E17F1-1324-4B42-CBE4-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 2340.00m,
                    InsurancePensionEmployer = 431.80m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 208.00m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 130.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 260.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("FF783464-04C7-49CF-741F-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("56495A09-76B4-4828-CBE5-08DB80474B75"),
                    NetSalaryJobContract = 1663.60m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 301.84m,
                    InsurancePensionEmployee = 232.76m,
                    InsuranceHealthEmployer = 105.60m,
                    InsuranceHealthEmployee = 70.40m,
                    InsuranceAdditionalEmployer = 61.60m,
                    InsuranceAdditionalEmployee = 48.40m,
                    IncomeTax = 184.84m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("07B84D2E-754A-4DA4-7420-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("5768E56A-8D2F-4EA7-CBE6-08DB80474B75"),
                    NetSalaryJobContract = 1172.08m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 212.66m,
                    InsurancePensionEmployee = 163.99m,
                    InsuranceHealthEmployer = 74.40m,
                    InsuranceHealthEmployee = 49.60m,
                    InsuranceAdditionalEmployer = 43.40m,
                    InsuranceAdditionalEmployee = 34.10m,
                    IncomeTax = 130.23m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("E664A122-C3C8-449B-7421-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("87008E93-86B0-43D4-CBE7-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 2880.00m,
                    InsurancePensionEmployer = 777.60m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 256.00m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 160.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 320.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("B5190512-3D13-484E-7422-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("951DFE17-3ED4-4EE8-CBE8-08DB80474B75"),
                    NetSalaryJobContract = 1229.40m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 223.06m,
                    InsurancePensionEmployee = 172.01m,
                    InsuranceHealthEmployer = 78.04m,
                    InsuranceHealthEmployee = 52.03m,
                    InsuranceAdditionalEmployer = 45.52m,
                    InsuranceAdditionalEmployee = 35.77m,
                    IncomeTax = 136.60m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("F81CE272-D8DE-4A0E-7423-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("1EA971BA-1DA9-4AF5-CBE9-08DB80474B75"),
                    NetSalaryJobContract = 1814.83m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 329.28m,
                    InsurancePensionEmployee = 253.92m,
                    InsuranceHealthEmployer = 115.20m,
                    InsuranceHealthEmployee = 76.80m,
                    InsuranceAdditionalEmployer = 67.20m,
                    InsuranceAdditionalEmployee = 52.80m,
                    IncomeTax = 201.65m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("A4E0B3AE-7033-456A-7424-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("08B60D14-DA17-45C7-CBEA-08DB80474B75"),
                    NetSalaryJobContract = 945.23m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 171.50m,
                    InsurancePensionEmployee = 132.25m,
                    InsuranceHealthEmployer = 60.00m,
                    InsuranceHealthEmployee = 40.00m,
                    InsuranceAdditionalEmployer = 35.00m,
                    InsuranceAdditionalEmployee = 27.50m,
                    IncomeTax = 105.03m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("BF69AE2A-328D-4269-7425-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("F7681106-88F0-4E13-CBEC-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 990.00m,
                    InsurancePensionEmployer = 267.30m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 88.00m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 55.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 110.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("7F5D827D-5730-453D-7426-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("AA78E286-FCA3-42D2-CBED-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 2340.00m,
                    InsurancePensionEmployer = 631.80m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 208.00m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 130.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 260.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("48E5D1F8-ADAD-49A1-7427-08DB804E8F82"),
                    PaymentId = Guid.Parse("6342590F-6F84-4654-564C-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("57E51B62-48FD-408F-CBEE-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 1440.00m,
                    InsurancePensionEmployer = 388.80m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 128.00m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 80.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 160.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("C256288A-8B6D-48D0-7428-08DB804E8F82"),
                    PaymentId = Guid.Parse("178D9A33-702C-4CAC-564D-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("21D55765-5AFB-412F-CBEF-08DB80474B75"),
                    NetSalaryJobContract = 2722.25m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 493.92m,
                    InsurancePensionEmployee = 380.88m,
                    InsuranceHealthEmployer = 172.80m,
                    InsuranceHealthEmployee = 115.20m,
                    InsuranceAdditionalEmployer = 100.80m,
                    InsuranceAdditionalEmployee = 79.20m,
                    IncomeTax = 302.47m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("6B6A4D04-F583-447D-7429-08DB804E8F82"),
                    PaymentId = Guid.Parse("178D9A33-702C-4CAC-564D-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("A9A85601-7847-4872-CBF0-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 4230.00m,
                    InsurancePensionEmployer = 1142.10m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 376.00m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 235.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 470.00m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("1EEEBE3D-A0C6-40E0-742A-08DB804E8F82"),
                    PaymentId = Guid.Parse("178D9A33-702C-4CAC-564D-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("0D09C40D-943A-4AC2-CBF1-08DB80474B75"),
                    NetSalaryJobContract = 2155.11m,
                    NetSalaryStateOfficial = 0m,
                    InsurancePensionEmployer = 391.02m,
                    InsurancePensionEmployee = 301.53m,
                    InsuranceHealthEmployer = 136.80m,
                    InsuranceHealthEmployee = 91.20m,
                    InsuranceAdditionalEmployer = 79.80m,
                    InsuranceAdditionalEmployee = 62.70m,
                    IncomeTax = 239.46m,
                },
                new PaymentSalaryDetails
                {
                    Id = Guid.Parse("6D21AA45-7BEF-4360-742B-08DB804E8F82"),
                    PaymentId = Guid.Parse("178D9A33-702C-4CAC-564D-08DB804E8F7E"),
                    EmployeeId = Guid.Parse("D7D70D4C-CA53-4D05-CBF2-08DB80474B75"),
                    NetSalaryJobContract = 0m,
                    NetSalaryStateOfficial = 2106.00m,
                    InsurancePensionEmployer = 568.62m,
                    InsurancePensionEmployee = 0m,
                    InsuranceHealthEmployer = 187.20m,
                    InsuranceHealthEmployee = 0m,
                    InsuranceAdditionalEmployer = 117.00m,
                    InsuranceAdditionalEmployee = 0m,
                    IncomeTax = 234.00m,
                },
            };
    }
}
