using Microsoft.AspNetCore.Mvc;
    using System.Globalization;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Extensions;
    using Microsoft.AspNetCore.Authorization;
    using PaymentsBudgetSystem.Core.Helpers;
    using PaymentsBudgetSystem.Core.Models.Enums;
    using PaymentsBudgetSystem.Core.Models.Information;
    using PaymentsBudgetSystem.Data.Entities.Enums;

    using static Common.ValidationErrors.General;
    using static Common.RoleNames;

    [Authorize(Roles = PrimaryAndSecondaryRoleNames)]
    public class InformationController : Controller
    {
        private readonly IInformationService informationService;

        public InformationController(IInformationService _informationService)
        {
            informationService = _informationService;
        }

        [HttpGet]
        public async Task<IActionResult> Info(
            DateTime? from,
            DateTime? to,
            SortBy sortBy,
            InformationSort informationSort,
            PaymentType paymentType,
            decimal? amountMin,
            decimal? amountMax,
            int page = 1,
            string receiver = ""
            )
        {
            DateTime startDate = from == null
                ? DateTime.ParseExact("01.01." + DateTime.Now.Year.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture)
                : (DateTime)from;

            DateTime endDate = to == null
                ? DateTime.ParseExact("31.12." + DateTime.Now.Year.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture)
                : (DateTime)to;

            startDate = DateTime.ParseExact(startDate.Day + "." + startDate.Month + "." + startDate.Year + " 00:00", "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);
            endDate = DateTime.ParseExact(endDate.Day + "." + endDate.Month + "." + endDate.Year + " 23:59", "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);

            if (startDate > endDate)
            {
                ModelState.AddModelError("", EarlierDateCannotBeAfterLaterDate);
                startDate = DateTime.ParseExact("01.01." + DateTime.Now.Year.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact("31.12." + DateTime.Now.Year.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

                var returnModel = new PaymentInformationViewModel
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    SortBy = sortBy,
                    InformationSort = informationSort,
                    AmountMin = amountMin,
                    AmountMax = amountMax,
                    PaymentType = paymentType,
                    Page = page,
                    ReceiverNameFilter = receiver,
                };

                return View(returnModel);
            }

            var model = new PaymentInformationViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                SortBy = sortBy,
                InformationSort = informationSort,
                AmountMin = amountMin,
                AmountMax = amountMax,
                PaymentType = paymentType,
                Page = page,
                ReceiverNameFilter = receiver
            };

            model = await informationService.GetPaymentsInfoAsync(User.Id(), model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Info(PaymentInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Info), new
            {
                from = model.StartDate,
                to = model.EndDate,
                sortBy = model.SortBy,
                informationSort = model.InformationSort,
                paymentType = model.PaymentType,
                amountMin = model.AmountMin,
                amountMax = model.AmountMax,
                page = model.Page,
                receiver = model.ReceiverNameFilter
            });
        }
    }
}
