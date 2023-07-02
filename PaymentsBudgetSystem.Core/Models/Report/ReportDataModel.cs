namespace PaymentsBudgetSystem.Core.Models.Report
{
    public class ReportDataModel
    {
        public string UserId { get; set; } = null!;

        public int Year { get; set; }

        public int Month { get; set; }

        public bool IsConsolidated { get; set; }

        public decimal Bank0101 { get; set; }

        public decimal Bank0102 { get; set; }

        public decimal Transfer0551 { get; set; }

        public decimal Transfer0560 { get; set; }

        public decimal Transfer0580 { get; set; }

        public decimal Transfer0590 { get; set; }

        public decimal Bank1015 { get; set; }

        public decimal Cash1015 { get; set; }

        public decimal Bank1020 { get; set; }

        public decimal Cash1020 { get; set; }

        public decimal Cash1051 { get; set; }

        public decimal Bank1051 { get; set; }

        public decimal Bank5100 { get; set; }

        public decimal Bank5200 { get; set; }

        public decimal Bank5300 { get; set; }

        public decimal SalariesLimit { get; set; }

        public decimal SupportLimit { get; set; }

        public decimal AssetsLimit { get; set; }
    }
}
