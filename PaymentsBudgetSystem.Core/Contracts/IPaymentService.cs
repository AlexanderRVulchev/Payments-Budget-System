
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models;
    using Models.Assets;
    using Models.Salaries;
    using Models.Support;

    public interface IPaymentService
    {
        Task<Guid> AddNewSupportPayment(string userId, SupportPaymentFormModel model);

        Task<SupportPaymentDetailsViewModel> GetSupportPaymentDetailsById(string userId, Guid paymentId);

        Task<Guid> AddNewAssetPayment(string userId, NewAssetFormModel model);

        Task<AssetPaymentDetailsViewModel> GetAssetPaymentDetailsById(string userId, Guid paymentId);

        Task<SalariesPaymentViewModel> CreatePayroll(string userId, int year, int month);

        Task<Guid> AddNewSalariesPayment(string userId, SalariesPaymentViewModel model);

        Task<SalariesPaymentViewModel> GetSalariesDetailsById(string userId, Guid paymentId);
    }
}
