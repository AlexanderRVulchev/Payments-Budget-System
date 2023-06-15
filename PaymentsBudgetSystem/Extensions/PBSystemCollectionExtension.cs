
namespace PaymentsBudgetSystem.Extensions
{
    using Core.Contracts;
    using Core.Services;
    using PaymentsBudgetSystem.Controllers;

    public static class PBSystemCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<UserController, UserController>();

            return services;
        }
    }
}