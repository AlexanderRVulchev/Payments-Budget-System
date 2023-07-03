using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Helpers;
    using Models;
    using Models.Assets;
    using Models.Beneficiaries;
    using Models.Employees;
    using Models.Salaries;
    using Models.Support;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;
    using Models.Cash;
    using GlobalSetting = Models.Enums.GlobalSetting;

    using static Common.DataConstants.General;
    using static Common.ExceptionMessages.Payment;

    public class PaymentService : IPaymentService
    {
        private readonly PBSystemDbContext context;

        private readonly IBeneficiaryService beneficiaryService;

        private readonly IEmployeeService employeeService;

        public PaymentService(
            PBSystemDbContext _context, 
            IBeneficiaryService _beneficiaryService,
            IEmployeeService _employeeService)
        {
            context = _context;
            beneficiaryService = _beneficiaryService;
            employeeService = _employeeService;
        }

        public async Task<Guid> AddNewAssetPayment(string userId, NewAssetFormModel model)
        {
            var beneficiary = await beneficiaryService.GetBeneficiaryAsync(userId, model.BeneficiaryId);

            var assetPayment = new Payment
            {
                Amount = model.Amount,
                Date = DateTime.Now,
                Paragraph = model.ParagraphType,
                PaymentType = PaymentType.Assets,
                UserId = userId,
                Description = model.Description,
                ReceiverName = beneficiary.Name,
                AssetsDetails = new PaymentAssetsDetails
                {
                    BeneficiaryId = model.BeneficiaryId,
                    DeliveryDate = DateTime.Now,
                    InvoiceNumber = model.InvoiceNumber,
                    InvoiceDate = model.InvoiceDate
                },                
            };

            await context.Payments.AddAsync(assetPayment);
            await context.SaveChangesAsync();

            List<Asset> newAssets = new();

            if (model.Position1Name != null && model.Position1Quantity > 0 && model.Position1SingleAssetValue > 0)
            {
                newAssets.AddRange(
                    CreateAssets(model.Position1Name, model.Position1Quantity, model.Position1SingleAssetValue, userId, assetPayment.Id, model.ParagraphType));
            }
            if (model.Position2Name != null && model.Position2Quantity > 0 && model.Position2SingleAssetValue > 0)
            {
                newAssets.AddRange(
                    CreateAssets(model.Position2Name, model.Position2Quantity, model.Position2SingleAssetValue, userId, assetPayment.Id, model.ParagraphType));
            }
            if (model.Position3Name != null && model.Position3Quantity > 0 && model.Position3SingleAssetValue > 0)
            {
                newAssets.AddRange(
                    CreateAssets(model.Position3Name, model.Position3Quantity, model.Position3SingleAssetValue, userId, assetPayment.Id, model.ParagraphType));
            }
            if (model.Position4Name != null && model.Position4Quantity > 0 && model.Position4SingleAssetValue > 0)
            {
                newAssets.AddRange(
                    CreateAssets(model.Position4Name, model.Position4Quantity, model.Position4SingleAssetValue, userId, assetPayment.Id, model.ParagraphType));
            }
            if (model.Position5Name != null && model.Position5Quantity > 0 && model.Position5SingleAssetValue > 0)
            {
                newAssets.AddRange(
                    CreateAssets(model.Position5Name, model.Position5Quantity, model.Position5SingleAssetValue, userId, assetPayment.Id, model.ParagraphType));
            }

            await context.Assets.AddRangeAsync(newAssets);
            await context.SaveChangesAsync();

            return assetPayment.Id;
        }

        public async Task<Guid> AddNewCashPaymentAsync(string userId, CashPaymentViewModel model)
        {
            var employee = await employeeService.GetEmployeeById(model.SelectedEmployee);

            var payment = new Payment
            {
                Date = DateTime.Now,
                Amount = model.Amount,
                Description = model.Description,
                PaymentType = PaymentType.Cash,
                Paragraph = model.Type,
                UserId = userId,
                ReceiverName = employee.FirstName + " " + employee.LastName,
                CashDetails = new CashPaymentDetails
                {
                    CashOrderDate = DateTime.Now,
                    CashOrderNumber = model.CashOrderNumber,
                    EmployeeId = model.SelectedEmployee
                }
            };

            await context.Payments.AddAsync(payment);
            await context.SaveChangesAsync();

            return payment.Id;
        }

        public async Task<Guid> AddNewSalariesPayment(string userId, SalariesPaymentViewModel model)
        {
            List<PaymentSalaryDetails> salaryDetails = new();

            foreach (var individualSalary in model.IndividualSalaries)
            {
                salaryDetails.Add(new PaymentSalaryDetails
                {
                    EmployeeId = individualSalary.EmployeeId,
                    IncomeTax = individualSalary.IncomeTax,
                    InsuranceAdditionalEmployee = individualSalary.InsuranceAdditionalEmployee,
                    InsuranceHealthEmployee = individualSalary.InsuranceHealthEmployee,
                    InsuranceAdditionalEmployer = individualSalary.InsuranceAdditionalEmployer,
                    InsuranceHealthEmployer = individualSalary.InsuranceHealthEmployer,
                    InsurancePensionEmployee = individualSalary.InsurancePensionEmployee,
                    InsurancePensionEmployer = individualSalary.InsurancePensionEmployer,
                    NetSalaryJobContract = individualSalary.NetSalaryJobContract,
                    NetSalaryStateOfficial = individualSalary.NetSalaryStateOfficial
                });
            }

            Payment payment = new Payment
            {
                Date = DateTime.Now,
                Description = $"Изплатени заплати за м.{model.Month} {model.Year} г.",
                SalariesDetails = salaryDetails,
                Amount = model.Amount,
                Paragraph = ParagraphType.LocalTransaction0000,
                PaymentType = PaymentType.Salaries,
                UserId = userId,
                ReceiverName = "Служители"
            };

            await context.Payments.AddAsync(payment);
            await context.SaveChangesAsync();

            return payment.Id;
        }

        public async Task<Guid> AddNewSupportPayment(string userId, SupportPaymentFormModel model)
        {
            DateTime? invoiceDate;

            var invoiceDateIsValid = DateTime.TryParseExact(model.InvoiceDate, ValidDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validInvoiceDate);

            if (!invoiceDateIsValid)
            {
                invoiceDate = null;
            }
            else
            {
                invoiceDate = validInvoiceDate;
            }

            var beneficiary = await beneficiaryService.GetBeneficiaryAsync(userId, model.BeneficiaryId);

            var supportPayment = new Payment
            {
                UserId = userId,
                Date = DateTime.Now,
                Paragraph = model.ParagraphType,
                Description = model.Description,
                Amount = model.Amount,
                PaymentType = PaymentType.Support,
                ReceiverName = beneficiary.Name,
                SupportDetails = new PaymentSupportDetails
                {
                    BeneficiaryId = model.BeneficiaryId,
                    InvoiceDate = invoiceDate,
                    InvoiceNumber = model.InvoiceNumber,
                }
            };

            await context.Payments.AddAsync(supportPayment);
            await context.SaveChangesAsync();

            return supportPayment.Id;
        }

        public async Task<SalariesPaymentViewModel> CreatePayroll(string userId, int year, int month)
        {
            string firstDayOfTheMonthString = "01." + month.ToString() + "." + year.ToString() + " 00:00";

            DateTime firstDayOfTheMonth = DateTime
                .ParseExact(firstDayOfTheMonthString, "dd.M.yyyy HH:hh", CultureInfo.InvariantCulture, DateTimeStyles.None);

            DateTime lastDayOfTheMonth = firstDayOfTheMonth
                .AddMonths(1)
                - TimeSpan.FromSeconds(1);

            var employees = await context
                .Employees
                .Where(e => e.UserId == userId)
                .Where(e => e.DateEmployed < lastDayOfTheMonth)
                .Where(e => e.DateLeft == null || e.DateLeft > firstDayOfTheMonth)
                .Select(e => new EmployeeSalaryPaymentViewModel
                {                 
                    EmployeeId = e.Id,
                    EmployeeName = e.FirstName + " " + e.LastName,
                    DateEmployed = e.DateEmployed,
                    DateLeft = e.DateLeft,
                    ContractType = e.ContractType,
                    MonthlySalary = e.MonthlySalary
                })
                .ToListAsync();

            var settings = await context
                 .GlobalSettings
                 .Select(gs => new GlobalSettingDataModel
                 {
                     Id = (GlobalSetting)gs.Id,
                     SettingName = gs.SettingName,
                     SettingValue = gs.SettingValue
                 })
                 .ToListAsync();

            Calculator calculator = new();

            foreach (var employee in employees)
            {
                calculator.CalculateEmployeeMonthlySalary(employee, firstDayOfTheMonth, lastDayOfTheMonth, settings);
            }

            var model = new SalariesPaymentViewModel
            {
                IndividualSalaries = employees,
                Month = month,
                Year = year                
            };

            calculator.CalculateTotalPayroll(model);

            return model;            
        }

        public async Task<AssetPaymentDetailsViewModel> GetAssetPaymentDetailsById(string userId, Guid paymentId)
        {
            var entity = await context
                .Payments
                .Where(p => p.Id == paymentId)
                .Include(p => p.AssetsDetails)
                .ThenInclude(ad => ad.Beneficiary)
                .Include(p => p.AssetsDetails)
                .ThenInclude(p => p.Assets)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new InvalidOperationException(InvalidPayment);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(PaymentAccessDenied);
            }

            return new AssetPaymentDetailsViewModel
            {
                Id = entity.Id,
                Date = entity.Date,
                Description = entity.Description,
                Amount = entity.Amount,
                InvoiceDate = entity.AssetsDetails.InvoiceDate,
                InvoiceNumber = entity.AssetsDetails.InvoiceNumber,
                ParagraphType = entity.Paragraph,
                Beneficiary = new BeneficiaryViewModel
                {
                    BeneficiaryId = entity.AssetsDetails.BeneficiaryId,
                    Name = entity.AssetsDetails.Beneficiary.Name,
                    Address = entity.AssetsDetails.Beneficiary.Address,
                    BankAccount = entity.AssetsDetails.Beneficiary.BankAccount,
                    Identifier = entity.AssetsDetails.Beneficiary.Identifier
                },
                Assets = entity.AssetsDetails.Assets.Select(a => new AssetShortViewModel
                {
                    AssetId = a.Id,
                    AssetAquired = a.DateAquired,
                    AssetDisposed = a.DateDisposed,
                    Description = a.Description,
                    ReportValue = a.ReportValue
                }).ToList()
            };
        }

        public async Task<CashPaymentDetailsModel> GetCashPaymentById(string userId, Guid paymentId)
        {
            var entity = await context
                .Payments
                .Where(p => p.Id == paymentId)
                .Include(p => p.CashDetails)
                .ThenInclude(cd => cd.Employee)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new InvalidOperationException(InvalidPayment);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(PaymentAccessDenied);
            }

            return new CashPaymentDetailsModel
            {
                Amount = entity.Amount,
                Date = entity.Date,
                CashOrderNumber = entity.CashDetails.CashOrderNumber,
                Description = entity.Description,
                Type = entity.Paragraph,
                Id = entity.Id,
                Employee = new EmployeeListModel
                {
                    EmployeeId = entity.CashDetails.EmployeeId,
                    EmployeeName = entity.CashDetails.Employee.FirstName + " " + entity.CashDetails.Employee.LastName
                }
            };
        }

        public async Task<SalariesPaymentViewModel> GetSalariesDetailsById(string userId, Guid paymentId)
        {
            var entity = await context
                .Payments
                .Where(p => p.Id == paymentId)
                .Include(p => p.SalariesDetails)
                .ThenInclude(sd => sd.Employee)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new InvalidOperationException(InvalidPayment);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(PaymentAccessDenied);
            }

            return new SalariesPaymentViewModel
            {
                Amount = entity.Amount,
                Month = entity.Date.Month,
                Year = entity.Date.Year,
                TotalIncomeTax = entity.SalariesDetails.Sum(sd => sd.IncomeTax),
                TotalInsuranceAdditionalEmployee = entity.SalariesDetails.Sum(sd => sd.InsuranceAdditionalEmployee),
                TotalInsuranceAdditionalEmployer = entity.SalariesDetails.Sum(sd => sd.InsuranceAdditionalEmployer),
                TotalInsuranceHealthEmployee = entity.SalariesDetails.Sum(sd => sd.InsuranceHealthEmployee),
                TotalInsuranceHealthEmployer = entity.SalariesDetails.Sum(sd => sd.InsuranceHealthEmployer),
                TotalInsurancePensionEmployee = entity.SalariesDetails.Sum(sd => sd.InsurancePensionEmployee),
                TotalInsurancePensionEmployer = entity.SalariesDetails.Sum(sd => sd.InsurancePensionEmployer),
                TotalNetSalaryJobContract = entity.SalariesDetails.Sum(sd => sd.NetSalaryJobContract),
                TotalNetSalaryStateOfficial = entity.SalariesDetails.Sum(sd => sd.NetSalaryStateOfficial),
                IndividualSalaries = entity.SalariesDetails.Select(sd => new EmployeeSalaryPaymentViewModel
                {
                    ContractType = sd.Employee.ContractType,
                    EmployeeName = sd.Employee.FirstName + " " + sd.Employee.LastName,
                    DateEmployed = sd.Employee.DateEmployed,
                    DateLeft = sd.Employee.DateLeft,
                    Id = sd.Employee.Id,
                    PaymentId = sd.PaymentId,
                    EmployeeId = sd.Employee.Id,
                    IncomeTax = sd.IncomeTax,
                    InsuranceAdditionalEmployee = sd.InsuranceAdditionalEmployee,
                    InsuranceAdditionalEmployer = sd.InsuranceAdditionalEmployer,
                    NetSalaryJobContract = sd.NetSalaryJobContract,
                    NetSalaryStateOfficial = sd.NetSalaryStateOfficial,
                    InsuranceHealthEmployee = sd.InsuranceHealthEmployee,
                    InsuranceHealthEmployer = sd.InsuranceHealthEmployer,
                    InsurancePensionEmployee = sd.InsurancePensionEmployee,
                    InsurancePensionEmployer = sd.InsurancePensionEmployer
                })
                .ToList()
            };

        }

        public async Task<SupportPaymentDetailsViewModel> GetSupportPaymentDetailsById(string userId, Guid paymentId)
        {
            var entity = await context
                .Payments
                .Where(p => p.Id == paymentId)
                .Include(p => p.SupportDetails)
                .ThenInclude(sd => sd.Beneficiary)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new InvalidOperationException(InvalidPayment);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(PaymentAccessDenied);
            }

            return new SupportPaymentDetailsViewModel
            {
                Amount = entity.Amount,
                Date = entity.Date,
                Description = entity.Description,
                ParagraphType = entity.Paragraph,
                PaymentType = entity.PaymentType,
                InvoiceDate = entity.SupportDetails.InvoiceDate.ToString(),
                InvoiceNumber = entity.SupportDetails.InvoiceNumber,
                Beneficiary = new BeneficiaryViewModel
                {
                    BeneficiaryId = entity.SupportDetails.BeneficiaryId,
                    Address = entity.SupportDetails.Beneficiary.Address,
                    BankAccount = entity.SupportDetails.Beneficiary.BankAccount,
                    Identifier = entity.SupportDetails.Beneficiary.Identifier,
                    Name = entity.SupportDetails.Beneficiary.Name
                }
            };
        }

        private List<Asset> CreateAssets(string description, int quantity, decimal reportValue, string userId, Guid paymentId, ParagraphType paragraph)
        {
            List<Asset> assets = new();

            for (int i = 0; i < quantity; i++)
            {
                assets.Add(new Asset
                {
                    DateAquired = DateTime.Now,
                    DateDisposed = null,
                    Description = description,
                    ReportValue = reportValue,
                    UserId = userId,
                    PaymentAssetDetailsId = paymentId,
                    Type = paragraph
                });
            }

            return assets;
        }
    }
}
