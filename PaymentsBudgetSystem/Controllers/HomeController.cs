using Microsoft.AspNetCore.Mvc;
using PaymentsBudgetSystem.Core.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PaymentsBudgetSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorMessage)
        {          
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = errorMessage
            });
        }
    }
}