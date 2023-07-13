
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Assets;
    using Models.Salaries;
    using Models.Support;
    using Models.Cash;

    public interface IPaymentService
    {
        Task<Guid> AddNewSupportPaymentAsync(string userId, SupportPaymentFormModel model);

        Task<Guid> AddNewCashPaymentAsync(string userId, CashPaymentViewModel model);

        Task<Guid> AddNewAssetPaymentAsync(string userId, NewAssetFormModel model);

        Task<Guid> AddNewSalariesPaymentAsync(string userId, SalariesPaymentViewModel model);

        Task<SupportPaymentDetailsViewModel> GetSupportPaymentDetailsByIdAsync(string userId, Guid paymentId);

        Task<AssetPaymentDetailsViewModel> GetAssetPaymentDetailsByIdAsync(string userId, Guid paymentId);

        Task<SalariesPaymentViewModel> GetSalariesDetailsByIdAsync(string userId, Guid paymentId);

        Task<CashPaymentDetailsModel> GetCashPaymentByIdAsync(string userId, Guid paymentId);

        Task<SalariesPaymentViewModel> CreatePayrollAsync(string userId, int year, int month);
    }
}
