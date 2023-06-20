
namespace PaymentsBudgetSystem.Extensions
{
    using Core.Contracts;
    using Core.Services;
    using PaymentsBudgetSystem.Controllers;

    public static class PBSystemCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<UserController, UserController>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<IBeneficiaryService, BeneficiaryService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}