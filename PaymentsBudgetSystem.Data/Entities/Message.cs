using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using static Common.DataConstants.Message;

    public class Message
    {
        [Key]
        public Guid Id { get; set; } = new();

        [Required]
        [ForeignKey(nameof(Sender))]
        public string SenderId { get; set; } = null!;
        public User Sender { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Receiver))]
        public string ReceiverId { get; set; } = null!;
        public User Receiver { get; set; } = null!;

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; } = null!;

        public HashSet<UserFile> UserFiles { get; set; } = new();
    }
}
