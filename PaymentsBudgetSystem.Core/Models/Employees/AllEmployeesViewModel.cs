using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Employees
{
    using Core.Models.Enums;

    public class AllEmployeesViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; } = new();

        [Display(Name = "Име")]
        public string? FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string? LastName { get; set; } 

        [Display(Name = "ЕГН")]
        public string? Egn { get; set; } 

        [Display(Name = "Брутна заплата")]
        public decimal? MonthlySalary { get; set; }

        [Display(Name = "Дата на назначаване")]
        public DateTime? DateEmployed { get; set; }

        [Display(Name = "Общо възнаграждания")]
        public decimal TotalIncome { get; set; }

        public EmployeeSort SortAttribute { get; set; }

        public SortBy SortBy { get; set; }
    }
}
