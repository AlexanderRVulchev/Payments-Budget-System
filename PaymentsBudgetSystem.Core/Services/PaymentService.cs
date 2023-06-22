using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Core.Contracts;
    using Core.Models.Support;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;
    using Core.Models.Beneficiaries;
    using static Common.DataConstants.General;
    using static Common.ExceptionMessages.Payment;

    public class PaymentService : IPaymentService
    {
        private readonly PBSystemDbContext context;

        public PaymentService(PBSystemDbContext _context)
        {
            context = _context;
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
    }
}
