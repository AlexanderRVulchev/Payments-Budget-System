using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using static Common.DataConstants.User;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserNameMaxLength)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsPrimary;
        
        public HashSet<Payment> Payments { get; set; } = new();
        
        public HashSet<Beneficiary> Beneficiaries { get; set; } = new();
        
        public HashSet<Employee> Employees { get; set; } = new();
        
        public HashSet<Asset> Assets { get; set; } = new();

        [InverseProperty(nameof(Message.Sender))]
        public HashSet<Message> SentMessages { get; set; } = new();

        [InverseProperty(nameof(Message.Receiver))]
        public HashSet<Message> ReceivedMessages { get; set; } = new();

        public HashSet<IndividualBudget> IndividualBudgets { get; set; } = new();
        
        public HashSet<ConsolidatedBudget> ConsolidatedBudgets { get; set; } = new();
        
        public HashSet<Report> Reports { get; set; } = new();
    }
}
