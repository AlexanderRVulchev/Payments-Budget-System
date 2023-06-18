namespace PaymentsBudgetSystem.Core.Models.Beneficiaries
{
    public class BeneficiaryViewModel
    {
        public Guid BeneficiaryId { get; set; }

        public string Name { get; set; } = null!;

        public string Identifier { get; set; } = null!;

        public string? Address { get; set; }
    }
}
