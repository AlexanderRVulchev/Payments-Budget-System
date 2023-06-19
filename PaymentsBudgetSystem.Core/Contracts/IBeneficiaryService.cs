
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Core.Models.Beneficiaries;

    public interface IBeneficiaryService
    {
        Task<AllBeneficiariesViewModel> GetAllBeneficiariesAsync(string userId, AllBeneficiariesViewModel model);

        Task AddBeneficiaryAsync(string userId, BeneficiaryFormModel model);
    }
}
