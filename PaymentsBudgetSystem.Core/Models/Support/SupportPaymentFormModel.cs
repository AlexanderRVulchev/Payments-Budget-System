
namespace PaymentsBudgetSystem.Core.Models.Support
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.Payment;
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class SupportPaymentFormModel
    {
        public BeneficiaryFormModel? Beneficiary { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public Guid BeneficiaryId { get; set; }

        [Display(Name = "Сума")]
        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        public decimal Amount { get; set; }

        [MaxLength(InvoiceNumberMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Номер на фактура")]
        public string? InvoiceNumber { get; set; }

        [Display(Name = "Дата на фактура")]
        public string? InvoiceDate { get; set; }

        [MaxLength(DescriptionMaxLength)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
