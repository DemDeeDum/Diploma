// <copyright file="ExternalLoginConfirmationViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for external login confirmation operation.
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>
        /// Gets or sets email address.
        /// </summary>
        [Required]
        [Display(Name = "Email address")]
        public string Email { get; set; }
    }
}
