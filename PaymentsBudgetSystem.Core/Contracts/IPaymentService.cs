
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models;
    using Models.Assets;
    using Models.Salaries;
    using Models.Support;
    using PaymentsBudgetSystem.Core.Models.Cash;

    public interface IPaymentService
    {
        Task<Guid> AddNewSupportPayment(string userId, SupportPaymentFormModel model);

        Task<Guid> AddNewCashPaymentAsync(string userId, CashPaymentViewModel model);

        Task<Guid> AddNewAssetPayment(string userId, NewAssetFormModel model);

        Task<Guid> AddNewSalariesPayment(string userId, SalariesPaymentViewModel model);

        Task<SupportPaymentDetailsViewModel> GetSupportPaymentDetailsById(string userId, Guid paymentId);

        Task<AssetPaymentDetailsViewModel> GetAssetPaymentDetailsById(string userId, Guid paymentId);

        Task<SalariesPaymentViewModel> GetSalariesDetailsById(string userId, Guid paymentId);

        Task<CashPaymentDetailsModel> GetCashPaymentById(string userId, Guid paymentId);

        Task<SalariesPaymentViewModel> CreatePayroll(string userId, int year, int month);
    }
}
