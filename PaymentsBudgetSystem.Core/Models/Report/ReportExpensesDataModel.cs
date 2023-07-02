
namespace PaymentsBudgetSystem.Core.Models.Report
{
    using Data.Entities.Enums;

    public class ReportExpensesDataModel
    {
        public decimal Amount { get; set; }

        public PaymentType PaymentType { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public decimal NetSalaryJobContract { get; set; }

        public decimal NetSalaryStateOfficial { get; set; }

        public decimal InsurancePension { get; set; }

        public decimal InsuranceHealth { get; set; }

        public decimal InsuranceAdditional { get; set; }

        public decimal IncomeTax { get; set; }
    }
}
