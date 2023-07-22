namespace PaymentsBudgetSystem.Common.DataConstants
{
    public class General
    {
        public const double DecimalMoneyMinValue = 0.01;
        public const double DecimalMoneyMaxValue = 99999999999.0;

        public const string ValidDateFormat = "dd.MM.yyyy";

        public const int YearMinValue = 1990;
        public const int YearMaxValue = 2100;

        public const int MonthMinValue = 1;
        public const int MonthMaxValue = 12;

        public const int ItemsPerPage = 9;

        public const string InvoiceNumberRegex = @"\d{10}";
        public const int InvoiceLength = 10;
    }
}
