using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaymentsBudgetSystem.Data;

namespace PaymentsBudgetSystem
{
    using Data;
    using Data.Entities;
    using Core.Contracts;
    using Core.Services;

    using static Common.DataConstants.User;
    using PaymentsBudgetSystem.Extensions;
    using Microsoft.AspNetCore.Authentication.Cookies;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<PBSystemDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = PasswordMinLength;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PBSystemDbContext>();

            
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddApplicationServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyAreas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.SeedRoles();

            app.Run();
        }
    }
}