using PaymentsBudgetSystem.Core.Models.Support;

namespace PaymentsBudgetSystem.Core.Contracts
{
    public interface IPaymentService
    {
        Task<Guid> AddNewSupportPayment(string userId, SupportPaymentFormModel model);

        Task<SupportPaymentDetailsViewModel> GetSupportPaymentDetailsById(string userId, Guid paymentId);
    }
}
