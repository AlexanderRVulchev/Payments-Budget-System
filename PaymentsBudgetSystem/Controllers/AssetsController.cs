using Microsoft.AspNetCore.Mvc;
using PaymentsBudgetSystem.Core.Contracts;

namespace PaymentsBudgetSystem.Controllers
{
    public class AssetsController : Controller
    {
        private readonly IAssetService assetService;

        public AssetsController(IAssetService _assetService)
        {
            assetService = _assetService;
        }

        public IActionResult Info()
        {
            
        }
    }
}
