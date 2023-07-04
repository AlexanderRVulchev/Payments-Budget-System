namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.User;

    public interface IUserService
    {
        Task<ICollection<string>> GetPrimaryNamesAsync();

        Task<Dictionary<string, string>> GetPrimaryIdsAndNamesAsync();

        Task RelateSecondaryToPrimaryUserAsync(string primaryId, string secondaryId);

        Task<List<InstitutionSelectModel>> GetAllUsersWithSavedReportsAsync();
    }
}
