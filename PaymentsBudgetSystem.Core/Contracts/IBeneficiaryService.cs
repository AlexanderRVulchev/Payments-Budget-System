
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Core.Models.Beneficiaries;

    public interface IBeneficiaryService
    {
        Task<AllBeneficiariesViewModel> GetAllBeneficiariesAsync(string userId, AllBeneficiariesViewModel model);

        Task AddBeneficiaryAsync(string userId, BeneficiaryFormModel model);

        Task<BeneficiaryFormModel> GetBeneficiaryAsync(string userId, Guid beneficiaryId);

        Task EditBeneficiary(string userId, BeneficiaryFormModel model);
    }
}
