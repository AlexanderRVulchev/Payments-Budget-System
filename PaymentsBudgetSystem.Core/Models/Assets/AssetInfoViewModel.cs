﻿using PaymentsBudgetSystem.Data.Entities.Enums;

namespace PaymentsBudgetSystem.Core.Models.Assets
{
    public class AssetInfoViewModel
    {
        public Guid AssetId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime DateAquired { get; set; }

        public DateTime? DateDisposed { get; set; }

        public ParagraphType Type { get; set; }

        public string TypeText { get; set; } = null!;

        public decimal ReportValue { get; set; }

        public decimal BalanceValue { get; set; }

        public decimal Amortization { get; set; }

        public decimal ResidualValue { get; set; }

        public Guid PaymentId { get; set; }
    }
}