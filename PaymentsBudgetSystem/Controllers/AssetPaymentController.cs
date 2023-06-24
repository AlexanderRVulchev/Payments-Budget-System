using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;
    using Core.Contracts;
    using Extensions;

    using static Common.ExceptionMessages.Beneficiary;
    using static Common.ExceptionMessages.Payment;
    using Microsoft.AspNetCore.Authorization;
    using PaymentsBudgetSystem.Core.Models.Assets;

    [Authorize]
    public class AssetPaymentController : Controller
    {
        private readonly IBeneficiaryService beneficiaryService;

        public AssetPaymentController(IBeneficiaryService _beneficiaryService)
        {
            this.beneficiaryService = _beneficiaryService;
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

            NewAssetFormModel model = new NewAssetFormModel
            {
                Beneficiary = beneficiary,
                BeneficiaryId = id,
                ParagraphType = (ParagraphType)type,
            };

            return View(model);
        }
    }
}
