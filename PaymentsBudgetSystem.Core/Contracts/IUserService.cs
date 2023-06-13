using PaymentsBudgetSystem.Common.DataConstants;

namespace PaymentsBudgetSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<ICollection<string>> GetPrimaryNamesAsync();

        Task<Dictionary<string, string>> GetPrimaryIdsAndNamesAsync();

        Task RelateSecondaryToPrimaryUserAsync(string primaryId, string secondaryId);
    }
}
