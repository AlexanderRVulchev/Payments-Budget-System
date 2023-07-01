using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Extensions;
    using System.Globalization;

    public class InformationController : Controller
    {
        private readonly IInformationService informationService;

        public InformationController(IInformationService _informationService)
        {
            informationService = _informationService;
        }

        public IActionResult Info()
        {
            DateTime from = DateTime.ParseExact("01.01.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact("31.12.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var model = informationService.GetPaymentsInfoAsync(User.Id(), from, to);
            return View(model);
        }
    }
}
