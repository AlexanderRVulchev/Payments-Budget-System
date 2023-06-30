using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

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