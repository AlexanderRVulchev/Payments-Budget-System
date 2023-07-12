using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Assets
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;

    using static Common.DataConstants.Asset;
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;
    using static Common.DataConstants.Payment;

    public class NewAssetFormModel
    {
        public Guid BeneficiaryId { get; set; }

        public BeneficiaryFormModel? Beneficiary { get; set; }

        public ParagraphType ParagraphType { get; set; }

        [MaxLength(InvoiceNumberMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Required(ErrorMessage = FieldIsRequired)]
        [Display(Name = "Номер на фактура")]
        [RegularExpression(InvoiceNumberRegex, ErrorMessage = InvalidInvoiceNumber)]
        public string InvoiceNumber { get; set; } = null!;

        [Display(Name ="Дата на фактура")]
        [Required(ErrorMessage = FieldIsRequired)]
        public DateTime InvoiceDate { get; set; }

        public decimal Amount
            => (Position1Quantity * Position1SingleAssetValue) +
            (Position2Quantity * Position2SingleAssetValue) +
            (Position3Quantity * Position3SingleAssetValue) +
            (Position4Quantity * Position4SingleAssetValue) +
            (Position5Quantity * Position5SingleAssetValue);

        [StringLength(AssetDescriptionMaxLength, MinimumLength = AssetDescriptionMinLength)]
        public string? Description { get; set; } = null!;

        // Position 1 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position1Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Количество")]
        public int Position1Quantity { get; set; }

        [Range(typeof(decimal), "0", DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal Position1SingleAssetValue { get; set; }

        // Position 2 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position2Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Количество")]
        public int Position2Quantity { get; set; }

        [Range(typeof(decimal), "0", DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal Position2SingleAssetValue { get; set; }

        // Position 3 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position3Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Количество")]
        public int Position3Quantity { get; set; }

        [Range(typeof(decimal), "0", DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal Position3SingleAssetValue { get; set; }

        // Position 4 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position4Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Количество")]
        public int Position4Quantity { get; set; }

        [Range(typeof(decimal), "0", DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal Position4SingleAssetValue { get; set; }

        // Position 5 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position5Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Количество")]
        public int Position5Quantity { get; set; }

        [Range(typeof(decimal), "0", DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal Position5SingleAssetValue { get; set; }
    }
}
