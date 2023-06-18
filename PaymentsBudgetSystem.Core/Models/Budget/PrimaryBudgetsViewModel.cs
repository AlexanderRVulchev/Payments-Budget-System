﻿using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Budget
{
    using static Common.DataConstants.General;

    public class PrimaryBudgetsViewModel
    {
        public List<BudgetViewModel> IndividualBudgets { get; set; } = new();

        public List<ConsolidatedBudgetViewModel> ConsolidatedBudgets { get; set; } = new();

        [Range(1990, 2100)]
        public int NewBudgetYear { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue)]
        public decimal NewBudgetFunds { get; set; }
    }
}
