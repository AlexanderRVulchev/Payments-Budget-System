
namespace PaymentsBudgetSystem.Extensions
{
    using Core.Contracts;
    using Core.Services;

    public static class PBSystemCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBudgetService, BudgetService>();


            return services;
        }
    }
}