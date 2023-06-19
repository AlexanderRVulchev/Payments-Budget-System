namespace PaymentsBudgetSystem.Common.ExceptionMessages
{
    public class Beneficiary
    {
        public const string BeneficiaryAlreadyExists = "Вече съществува контрагент с такова име или булстат!";

        public const string BeneficiaryDoesNotExist = "Несъществуващ контрагент!";

        public const string BeneficiaryAccessDenied = "Нямате достъп до този контрагент!";
    }
}
