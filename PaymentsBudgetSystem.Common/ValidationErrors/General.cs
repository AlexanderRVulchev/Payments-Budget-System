﻿namespace PaymentsBudgetSystem.Common.ValidationErrors
{
    public class General
    {
        public const string StringLengthValidationError = "Полето {0} трябва да бъде с дължина между {2} и {1} символа.";

        public const string StringMaxLengthValidationError = "Полето {0} може да съдържа най-много {1} символа.";

        public const string StringFixedLength = "Полето {0} трябва да съдържа точно {1} символа.";

        public const string RangeValidationError = "Полето {0} трябва да бъде със стойност между {1} и {2}.";

        public const string PasswordDoesntMatchError = "Паролата не съвпада.";

        public const string InvalidPrimaryNumberForRegister = "Не е избран валиден Първостепенен РБ";

        public const string InvalidYearError = "Годината трябва да бъде между {1} и {2}";

        public const string EgnRegexValidationMessage = "Полето ЕГН трябва да съдържа само цифри";

        public const string InvalidMonthError = "Невалиден месец.";

        public const string DateIsInvalid = "Моля, въведете дата във формат дд.мм.гггг";

        public const string PaymentMoneyCannotBeZeroOrLess = "Стойността на плащането трябва да бъде най-малко 0.01 лв.";

        public const string AssetMustHaveAName = "Всеки актив с поне 1 брой трябва да има име.";

        public const string OrderNumberMustBeAPositiveNumber = "Номера на ордера трябва да бъде положително число.";

        public const string EarlierDateCannotBeAfterLaterDate = "Невъзможно е началната дата да бъде след крайната дата.";

        public const string FieldIsRequired = "Полето е задължително";

        public const string SalaryIsBelowMinimumWage = "Брутната заплата на служителя не може да бъде по-малка от минималната работна заплата - {0} лв.";

        public const string InvalidInvoiceNumber = "Номерът на фактура трябва да съдържа 10 цифри";

        public const string ConsolidatedBudgetLimitExceeded = "Въведеното разпределение превишава общият лимит на консолидирания отчет. Превишението е в размер {0} лева. Моля въведете нови данни.";

        public const string BeneficiaryIdentifierMustBeNineDigits = "Булстатът на контрагента трябва да бъде 9-цифрено число";

        public const string BeneficiaryInvalidBankAccount = "Въведената банкова сметка е невалидна";
    }
}
