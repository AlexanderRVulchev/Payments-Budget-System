﻿using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public class SupportController : Controller
    {
        private readonly IBeneficiaryService beneficiaryService;

        public SupportController(IBeneficiaryService _beneficiaryService)
        {
            beneficiaryService = _beneficiaryService;
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var allUserBeneficiaries = await beneficiaryService
                .GetAllBeneficiariesAsync(User.Id(), new AllBeneficiariesViewModel());

            var model = new NewSupportPaymentViewModel
            {
                Beneficiaries = allUserBeneficiaries.Beneficiaries
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult New(NewSupportPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Payment), new { id = model.SelectedBeneficiary, type = model.SelectedParagraph });
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
                var invoiceDateIsValid = DateTime.TryParseExact(model.InvoiceDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

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

            return View();
        }
    }
}
