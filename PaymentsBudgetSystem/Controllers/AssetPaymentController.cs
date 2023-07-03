using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;
    using Extensions;
    using Core.Models.Assets;

    using static Common.ExceptionMessages.Beneficiary;
    using static Common.ExceptionMessages.Payment;
    using static Common.ValidationErrors.General;


    [Authorize]
    public class AssetPaymentController : Controller
    {
        private readonly IBeneficiaryService beneficiaryService;

        private readonly IPaymentService paymentService;

        public AssetPaymentController(
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
                    ParagraphType.UpkeepLongTermAssets5100,
                    ParagraphType.AquisitionLongTermAssets5200,
                    ParagraphType.AquisitionIntangibleAssets5300
                }.Contains((ParagraphType)type) == false)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = InvalidParagraph });
            }

            BeneficiaryFormModel beneficiary;
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

            NewAssetFormModel model = new NewAssetFormModel
            {
                Beneficiary = beneficiary,
                BeneficiaryId = (Guid)id,
                ParagraphType = (ParagraphType)type,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(NewAssetFormModel model)
        {
            if (model.InvoiceDate > DateTime.Now)
            {
                ModelState.AddModelError("", InvoiceDateCannotBeInTheFuture);
            }

            if (model.Position1Name == null && model.Position1Quantity != 0 ||
                model.Position2Name == null && model.Position2Quantity != 0 ||
                model.Position3Name == null && model.Position3Quantity != 0 ||
                model.Position4Name == null && model.Position4Quantity != 0 ||
                model.Position5Name == null && model.Position5Quantity != 0)
            {
                ModelState.AddModelError("", AssetMustHaveAName);
            }

            if (model.Amount <= 0)
            {
                ModelState.AddModelError("", PaymentMoneyCannotBeZeroOrLess);
            }

            model.Beneficiary = await beneficiaryService.GetBeneficiaryAsync(User.Id(), (Guid)model.BeneficiaryId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                Guid assetPaymentId = await paymentService.AddNewAssetPayment(User.Id(), model);
                return RedirectToAction(nameof(AssetPaymentDetails), new { id = assetPaymentId });
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }

        public async Task<IActionResult> AssetPaymentDetails(Guid id)
        {
            try
            {
                AssetPaymentDetailsViewModel model = await paymentService.GetAssetPaymentDetailsById(User.Id(), id);
                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }
    }
}
