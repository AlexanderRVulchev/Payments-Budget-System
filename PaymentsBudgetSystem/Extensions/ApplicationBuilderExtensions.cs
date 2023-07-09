using Microsoft.AspNetCore.Identity;

namespace PaymentsBudgetSystem.Extensions
{
    using Data.Entities;
    using static Common.RoleNames;

    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedRoles(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(AdminRoleName))
            {
                var role = new IdentityRole(AdminRoleName);
                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByNameAsync("admin");

                await userManager.AddToRoleAsync(admin, AdminRoleName);
            }

            if (!await roleManager.RoleExistsAsync(PrimaryRoleName))
            {
                var role = new IdentityRole(PrimaryRoleName);
                await roleManager.CreateAsync(role);

                var mc = await userManager.FindByNameAsync("mc");
                var mtsp = await userManager.FindByNameAsync("mtsp");

                await userManager.AddToRoleAsync(mc, PrimaryRoleName);
                await userManager.AddToRoleAsync(mtsp, PrimaryRoleName);
            }

            if (!await roleManager.RoleExistsAsync(SecondaryRoleName))
            {
                var role = new IdentityRole(SecondaryRoleName);
                await roleManager.CreateAsync(role);

                var sf = await userManager.FindByNameAsync("sf");
                var daa = await userManager.FindByNameAsync("daa");

                await userManager.AddToRoleAsync(sf, SecondaryRoleName);
                await userManager.AddToRoleAsync(daa, SecondaryRoleName);
            }
                        
            return app;
        }
    }
}
