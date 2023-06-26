using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace PaymentsBudgetSystem.Core.Services
{
    using Core.Contracts;
    using Core.Models.Assets;
    using Core.Models.Beneficiaries;
    using Core.Models.Support;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Core.Helpers;
    using PaymentsBudgetSystem.Core.Models.Salaries;
    using static Common.DataConstants.General;
    using static Common.ExceptionMessages.Payment;

    public class PaymentService : IPaymentService
    {
        private readonly PBSystemDbContext context;

        public PaymentService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<Guid> AddNewAssetPayment(string userId, NewAssetFormModel model)
        {
            var assetPayment = new Payment
            {
                Amount = model.Amount,
                Date = DateTime.Now,
                Paragraph = model.ParagraphType,
                PaymentType = PaymentType.Assets,
                UserId = userId,
                Description = model.Description,
                AssetsDetails = new PaymentAssetsDetails
                {
                    BeneficiaryId = model.BeneficiaryId,
                    DeliveryDate = DateTime.Now,
                    InvoiceNumber = model.InvoiceNumber,
                    InvoiceDate = model.InvoiceDate
                }
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

            var supportPayment = new Payment
            {
                UserId = userId,
                Date = DateTime.Now,
                Paragraph = model.ParagraphType,
                Description = model.Description,
                Amount = model.Amount,
                PaymentType = PaymentType.Support,
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
                .ParseExact(firstDayOfTheMonthString, "dd.MM.yyyy HH:hh", CultureInfo.InvariantCulture, DateTimeStyles.None);

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
                    Id = e.Id,
                    EmployeeName = e.FirstName + " " + e.LastName,
                    DateEmployed = e.DateEmployed,
                    DateLeft = e.DateLeft                    
                })
                .ToListAsync();

            Calculator calculator = new();

            foreach (var employee in employees)
            {
                //TODO: Implement method
                calculator.CalculateEmployeeMonthlySalary(employee, firstDayOfTheMonth, lastDayOfTheMonth);
            }

            return new SalariesPaymentViewModel
            {
                //TODO: new calculation method
            };
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
