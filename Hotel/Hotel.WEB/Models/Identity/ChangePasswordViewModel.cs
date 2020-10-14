// <copyright file="ChangePasswordViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for changing password operation.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Gets or sets old password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets new password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Enter into {0} between {2} and {1} letters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "New password and its confirming must be equal")]
        public string ConfirmPassword { get; set; }
    }
}
