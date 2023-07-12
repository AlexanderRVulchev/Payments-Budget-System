
namespace PaymentsBudgetSystem.Core.Models.Support
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.Payment;
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;
    using Microsoft.AspNetCore.Mvc;

    public class SupportPaymentFormModel
    {
        public BeneficiaryFormModel? Beneficiary { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public Guid BeneficiaryId { get; set; }

        [Display(Name = "Сума")]
        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [MaxLength(InvoiceNumberMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [RegularExpression(InvoiceNumberRegex, ErrorMessage = InvalidInvoiceNumber)]
        [Display(Name = "Номер на фактура")]
        public string InvoiceNumber { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [Display(Name = "Дата на фактура")]
        public DateTime InvoiceDate { get; set; }

        [MaxLength(DescriptionMaxLength)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
