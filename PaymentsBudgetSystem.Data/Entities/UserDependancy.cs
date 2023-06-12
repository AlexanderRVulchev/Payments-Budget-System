using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Data.Entities
{
    using static Common.DataConstants.User;

    public class UserDependancy
    {
        [Required]
        [MaxLength(IdMaxLength)]
        public string PrimaryUserId { get; set; } = null!;

        [Required]
        [MaxLength(IdMaxLength)]
        public string SecondaryUserId { get; set; } = null!;
    }
}
