using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;

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

            user = new User()
            {
                Id = "33fb1d42-a747-4860-b3ce-7e33a0421a0d",
                Email = "Областна администрация София",
                UserName = "sf",
                NormalizedUserName = "SF",
                Name = "Областна администрация София"
            };

            user.PasswordHash = hasher.HashPassword(user, "1111");
            users.Add(user);

            user = new User()
            {
                Id = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                Email = "Министерски съвет",
                UserName = "mc",
                NormalizedUserName = "MC",
                Name = "Министерски съвет"
            };

            user.PasswordHash = hasher.HashPassword(user, "1111");
            users.Add(user);

            user = new User()
            {
                Id = "a01f638b-535d-48bc-9cee-ec31217088b9",
                Email = "Министерство на труда и социалната политика",
                UserName = "mtsp",
                NormalizedUserName = "MTSP",
                Name = "Министерство на труда и социалната политика"
            };

            user.PasswordHash = hasher.HashPassword(user, "1111");
            users.Add(user);

            user = new User()
            {
                Id = "f9e9db47-f25b-411f-ad79-2b2715dd132f",
                Email = "Държавна агенция Архиви",
                UserName = "daa",
                NormalizedUserName = "DAA",
                Name = "Държавна агенция Архиви"
            };

            user.PasswordHash = hasher.HashPassword(user, "1111");
            users.Add(user);

            return users;
        }
    }
}
