﻿using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.User
{

    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;

        public Dictionary<string, string> PrimaryInstitution { get; set; } = new();

        public bool IsSecondary { get; set; }

        public int InputForPrimary { get; set; }
    }
}

