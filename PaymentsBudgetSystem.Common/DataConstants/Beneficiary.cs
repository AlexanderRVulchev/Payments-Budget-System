namespace PaymentsBudgetSystem.Common.DataConstants
{
    public class Beneficiary
    {
        public const int BeneficiaryNameMinLength = 3;
        public const int BeneficiaryNameMaxLength = 30;

        public const int BeneficiaryIdentifierFixedLength = 9;

        public const int BeneficiaryAddressMaxLength = 100;

        public const int BeneficiaryBankAccountMaxLength = 22;

        public const string BankAccountRegex = @"^[A-Z]{2}\d{2}[A-Z]{4}\d{14}$";
    }
}
