
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Assets;

    public interface IAssetService
    {
        Task<AllAssetsViewModel> GetAllAssetsAsync(string userId, AllAssetsViewModel model);        
    }
}
