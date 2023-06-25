
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Assets;

    public interface IAssetService
    {
        Task<List<AssetInfoViewModel>> GetAllAssetsAsync(string userId, int year, int month);
    }
}
