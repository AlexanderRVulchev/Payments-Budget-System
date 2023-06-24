using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models.Support;
    using Core.Contracts;
    using Extensions;
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;

    using static Common.ExceptionMessages.Beneficiary;
    using static Common.ExceptionMessages.Payment;
    using static Common.ValidationErrors.General;
    using static Common.DataConstants.General;
    using PaymentsBudgetSystem.Data.Entities;

    [Authorize]
    public class SupportController : Controller
    {
        private readonly IBeneficiaryService beneficiaryService;
        private readonly IPaymentService paymentService;

        public SupportController(
            IBeneficiaryService _beneficiaryService,
            IPaymentService _paymentService)
        {
            beneficiaryService = _beneficiaryService;
            paymentService = _paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Payment(Guid? id, ParagraphType? type)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = BeneficiaryDoesNotExist });
            }
            if (type == null ||
                new List<ParagraphType>
                {
                    ParagraphType.Materials1015,
                    ParagraphType.Services1020,
                    ParagraphType.BusinessTrips1051
                }.Contains((ParagraphType)type) == false)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = InvalidParagarph });
            }

            BeneficiaryFormModel? beneficiary = null;
            try
            {
                beneficiary = await beneficiaryService.GetBeneficiaryAsync(User.Id(), (Guid)id);
                if (beneficiary == null)
                {
                    throw new InvalidOperationException(BeneficiaryDoesNotExist);
                }
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }

            var model = new SupportPaymentFormModel
            {
                ParagraphType = (ParagraphType)type,
                Beneficiary = beneficiary
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(SupportPaymentFormModel model)
        {
            if (model.InvoiceDate != null)
            {
                var invoiceDateIsValid = DateTime.TryParseExact(model.InvoiceDate, ValidDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

                if (!invoiceDateIsValid)
                {
                    ModelState.AddModelError("", DateIsInvalid);
                }
            }

            if (!ModelState.IsValid)
            {
                var beneficiary = await beneficiaryService.GetBeneficiaryAsync(User.Id(), model.BeneficiaryId);
                model.Beneficiary = beneficiary;
                return View(model);
            }

            try
            {
                Guid paymentId = await paymentService.AddNewSupportPayment(User.Id(), model);
                return RedirectToAction(nameof(SupportPaymentDetails), new { id = paymentId });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = CannotAddPayment });
            }
        }

        public async Task<IActionResult> SupportPaymentDetails(Guid id)
        {
            try
            {
                SupportPaymentDetailsViewModel model = await paymentService.GetSupportPaymentDetailsById(User.Id(), id);
                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }
    }
}
