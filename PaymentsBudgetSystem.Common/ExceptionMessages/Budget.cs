namespace PaymentsBudgetSystem.Common.ExceptionMessages
{
    public class Budget
    {
        public const string CannotRetrieveConsolidatedBudget = "Неуспешно извличане на информация за консолидиран бюджет";

        public const string CannotRetrieveIndividualBudget = "Неуспешно извличане на информация за индивидуален бюджет";

        public const string ExpensesCannotExceedLimit = "Недопустимо е лимитът на всяка група разходи да бъде по-малък от извършените годишни разходи";

        public const string InvalidBudgetYearOrFunds = "Въведете правилна година или стойност на новият бюджет";

        public const string TheBudgetAlreadyExists = "Бюджетът за посочената година вече е създаден";
    }
}
