// <copyright file="SetPasswordViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for setting password.
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets new password.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
