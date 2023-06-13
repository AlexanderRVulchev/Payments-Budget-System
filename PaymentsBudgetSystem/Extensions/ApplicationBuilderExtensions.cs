using Microsoft.AspNetCore.Identity;

namespace PaymentsBudgetSystem.Extensions
{
    using Data.Entities;
    using static Common.RoleNames;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    var role = new IdentityRole(AdminRoleName);
                    await roleManager.CreateAsync(role);
                }

                if (!await roleManager.RoleExistsAsync(PrimaryRoleName))
                {
                    var role = new IdentityRole(PrimaryRoleName);
                    await roleManager.CreateAsync(role);
                }

                if (!await roleManager.RoleExistsAsync(SecondaryRoleName))
                {
                    var role = new IdentityRole(SecondaryRoleName);
                    await roleManager.CreateAsync(role);
                }

            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
