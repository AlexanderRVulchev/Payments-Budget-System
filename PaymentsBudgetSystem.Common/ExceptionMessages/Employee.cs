namespace PaymentsBudgetSystem.Common.ExceptionMessages
{
    public class Employee
    {
        public const string EmployeeDoesNotExist = "Несъществуващ служител!";

        public const string EmployeeAccessDenied = "Нямате достъп до данните на този служител!";

        public const string EmployeeInvalidDateLeft = "Датата на освобождаване на служителя не може да бъде по-ранна от датата на назначаването му";
    }
}
