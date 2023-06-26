using PaymentsBudgetSystem.Core.Models.Assets;
using PaymentsBudgetSystem.Core.Models.Salaries;
using PaymentsBudgetSystem.Core.Models.Support;

namespace PaymentsBudgetSystem.Core.Contracts
{
    public interface IPaymentService
    {
        Task<Guid> AddNewSupportPayment(string userId, SupportPaymentFormModel model);

        Task<SupportPaymentDetailsViewModel> GetSupportPaymentDetailsById(string userId, Guid paymentId);

        Task<Guid> AddNewAssetPayment(string userId, NewAssetFormModel model);

        Task<AssetPaymentDetailsViewModel> GetAssetPaymentDetailsById(string userId, Guid paymentId);

        Task<SalariesPaymentViewModel> CreatePayroll(string userId, int year, int month);
    }
}
