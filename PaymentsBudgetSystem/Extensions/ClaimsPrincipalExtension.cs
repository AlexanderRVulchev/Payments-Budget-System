using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace PaymentsBudgetSystem.Extensions
{
    using static Common.RoleNames;

    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.NameIdentifier);

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdminRoleName);

        public static bool IsPrimary(this ClaimsPrincipal user)
            => user.IsInRole(PrimaryRoleName);

        public static bool IsSecondary(this ClaimsPrincipal user)
            => user.IsInRole(SecondaryRoleName);
    }
}
