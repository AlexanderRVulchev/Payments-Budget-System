namespace PaymentsBudgetSystem.Common.ValidationErrors
{
    public class General
    {
        public const string StringLengthValidationError = "{0} трябва да бъде с дължина между {2} и {1} символа.";

        public const string MoneyValidationError = "Полето {0} трябва да бъде със стойност между {1} и {2}.";

        public const string PasswordDoesntMatchError = "Паролата не съвпада.";

        public const string InvalidPrimaryNumberForRegister = "Моля въведете валиден номер на Първостепенен РБ";

        public const string InvalidYearError = "Годината трябва да бъде между {1} и {2}";
    }
}
