namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Information;

    public interface IInformationService
    {
        Task<PaymentInformationViewModel> GetPaymentsInfoAsync(string userId, PaymentInformationViewModel model);
    }
}
