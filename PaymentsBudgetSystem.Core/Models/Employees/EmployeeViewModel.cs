using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Employees
{
    using Data.Entities.Enums;

    public class EmployeeViewModel
    {
        public Guid EmployeeId { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Display(Name = "ЕГН")]
        public string Egn { get; set; } = null!;

        [Display(Name = "Брутна заплата")]
        public decimal MonthlySalary { get; set; }

        [Display(Name = "Дата на назначаване")]
        public DateTime DateEmployed { get; set; }

        [Display(Name = "Дата на освобождаване")]
        public DateTime? DateLeft { get; set; }

        [Display(Name = "Общо възнаграждания")]
        public decimal TotalIncome { get; set; }

        public ContractType ContractType { get; set; }
    }
}
