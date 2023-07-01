namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Information;

    public interface IInformationService
    {
        Task<List<PaymentInformationItemModel>> GetPaymentsInfoAsync(string userId, DateTime from, DateTime to);
    }
}
