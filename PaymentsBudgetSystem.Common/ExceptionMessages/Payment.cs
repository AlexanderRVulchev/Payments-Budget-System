namespace PaymentsBudgetSystem.Common.ExceptionMessages
{
    public class Payment
    {
        public const string InvalidParagraph = "Посоченият Вид плащане е невалиден!";

        public const string CannotAddPayment = "Вашето плащане не може да бъде извършено!";

        public const string InvalidPayment = "Плащането не съществува!";

        public const string PaymentAccessDenied = "Нямате достъп до това плащане!";

        public const string InvoiceDateCannotBeInTheFuture = "Датата на фактурата не може да бъде след плащането по нея";

        public const string NoBudgetCreated = "Няма утвърден бюджет за текущата година. Не могат да се извършват плащания без бюджет. Създайте бюджет за текущата година.";

        public const string PaymentExceedsBudgetLimit = "Разходите не могат да превишават утвърденият план за {0} по бюджета. Свободни средства: {1} лв.";
    }
}
