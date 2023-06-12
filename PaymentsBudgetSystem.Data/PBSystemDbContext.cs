using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Data
{
    public class PBSystemDbContext : IdentityDbContext
    {
        public PBSystemDbContext(DbContextOptions<PBSystemDbContext> options)
            : base(options)
        {
        }
    }
}