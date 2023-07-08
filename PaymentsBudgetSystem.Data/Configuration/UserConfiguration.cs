using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsBudgetSystem.Data.Entities;

namespace PaymentsBudgetSystem.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(CreateUsers());
        }

        private List<User> CreateUsers()
        {
            var users = new List<User>();

            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "586513cb-2bad-4ea3-ae33-7b8954efb167",
                Email = "Администратор",
                UserName = "admin",
                NormalizedUserName = "admin",
                Name = "Админитратор"
            };

            user.PasswordHash = hasher.HashPassword(user, "1111");

            users.Add(user);

            return users;
        }
    }
}
