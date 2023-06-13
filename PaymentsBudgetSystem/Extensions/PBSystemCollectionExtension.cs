
namespace PaymentsBudgetSystem.Extensions
{
    using Core.Contracts;
    using Core.Services;

    public static class HouseRentingServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}