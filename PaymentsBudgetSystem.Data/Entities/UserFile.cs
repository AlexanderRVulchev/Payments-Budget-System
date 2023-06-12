using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using static Common.DataConstants.UserFile;

    public class UserFile
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Message))]
        public Guid MessageId { get; set; }
        public Message Message { get; set; } = null!;

        [Required]
        [MaxLength(FileNameMaxLength)]
        public string FileName { get; set; } = null!;

        [Required]
        [MaxLength(FilePathMaxLength)]
        public string FilePath { get; set; } = null!;
    }
}
