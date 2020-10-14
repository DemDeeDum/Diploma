// <copyright file="SendCodeViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Models.Identity
{
    using System.Collections.Generic;

    /// <summary>
    /// View model for sending code operation.
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// Gets or sets selected provider.
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Gets or sets provider list.
        /// </summary>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

        /// <summary>
        /// Gets or sets return url.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether browser should remember the account.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
