namespace PaymentsBudgetSystem.Common.ValidationErrors
{
    public class Beneficiary
    {
        public const string BeneficiaryIdentifierMustBeNineDigits = "Булстатът на контрагента трябва да бъде 9-цифрено число";

        public const string BeneficiaryInvalidBankAccount = "Въведената банкова сметка е невалидна";
    }
}
