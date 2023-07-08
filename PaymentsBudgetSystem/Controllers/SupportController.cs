using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Beneficiaries;
    using Core.Models.Support;
    using Data.Entities.Enums;
    using Extensions;

    using static Common.ExceptionMessages.Beneficiary;
    using static Common.ExceptionMessages.Payment;
    using static Common.RoleNames;

    [Authorize(Roles = PrimaryAndSecondaryRoleNames)]
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
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = InvalidParagraph });
            }

            BeneficiaryFormModel? beneficiary = new();
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                Guid paymentId = await paymentService.AddNewSupportPayment(User.Id(), model);
                TempData["SuccessMessage"] = "Плащането е извършено успешно!";

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
