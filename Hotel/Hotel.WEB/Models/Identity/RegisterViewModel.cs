// <copyright file="RegisterViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for registration.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        [StringLength(50, ErrorMessage = "Enter into {0} between {2} and {1} letters", MinimumLength = 2)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets email address.
        /// </summary>
        [Display(Name = "Email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [StringLength(100, ErrorMessage = "Enter into {0} between {2} and {1} letters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and its comfirming are not equal.")]
        public string ConfirmPassword { get; set; }
    }
}
