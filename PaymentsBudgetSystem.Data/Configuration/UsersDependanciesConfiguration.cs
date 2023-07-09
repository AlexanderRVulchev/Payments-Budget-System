using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;
    using System;

    internal class UsersDependanciesConfiguration : IEntityTypeConfiguration<UserDependancy>
    {
        public void Configure(EntityTypeBuilder<UserDependancy> builder)
        {
            builder.HasData(SeedUserDependancies());
        }

        private static List<UserDependancy> SeedUserDependancies()
            => new()
            {
                new UserDependancy
                {
                    PrimaryUserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    SecondaryUserId = "33fb1d42-a747-4860-b3ce-7e33a0421a0d"
                },
                new UserDependancy
                {
                    PrimaryUserId = "9c3aa9ca-3546-4a5a-a327-d1a0555dc2d3",
                    SecondaryUserId = "f9e9db47-f25b-411f-ad79-2b2715dd132f"
                }
            };
    }
}
