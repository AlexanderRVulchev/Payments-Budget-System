﻿using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Assets
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;

    using static Common.DataConstants.Asset;
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class NewAssetFormModel
    {
        public Guid? BeneficiaryId { get; set; }

        public BeneficiaryFormModel? Beneficiary { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public string? InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public decimal Amount { get; set; }

        [StringLength(AssetDescriptionMaxLength, MinimumLength = AssetDescriptionMinLength)]
        public string? Description { get; set; } = null!;

        [Required]
        public DateTime DeliveryDate { get; set; }

        // Position 1 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position1Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Количество")]
        public int Position1Quantity { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal? Position1SingleAssetValue { get; set; }

        // Position 2 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position2Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Количество")]
        public int Position2Quantity { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal? Position2SingleAssetValue { get; set; }

        // Position 3 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position3Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Количество")]
        public int Position3Quantity { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal? Position3SingleAssetValue { get; set; }

        // Position 4 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position4Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Количество")]
        public int Position4Quantity { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal? Position4SingleAssetValue { get; set; }

        // Position 5 data

        [MaxLength(AssetNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на актива")]
        public string? Position5Name { get; set; }

        [Range(AssetQuantityMinValue, AssetQuantityMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Количество")]
        public int Position5Quantity { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = MoneyValidationError)]
        [Display(Name = "Единична стойност на актива")]
        public decimal? Position5SingleAssetValue { get; set; }
    }
}
