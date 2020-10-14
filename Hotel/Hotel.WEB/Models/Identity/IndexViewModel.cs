// <copyright file="IndexViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Models.Identity
{
    using System.Collections.Generic;
    using Hotel.WEB.Models.Profile;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// View model for index account page.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether user has password or not.
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Gets or sets user login info list.
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// Gets or sets phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user uses two factor authentication.
        /// </summary>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether browser remember authentication or not.
        /// </summary>
        public bool BrowserRemembered { get; set; }

        /// <summary>
        /// Gets or sets the list of bookings to be confirmed.
        /// </summary>
        public List<BookingViewModel> BookingsToConfirm { get; set; }

        /// <summary>
        /// Gets or sets the list of active bookings.
        /// </summary>
        public List<BookingViewModel> ActiveBookings { get; set; }

        /// <summary>
        /// Gets or sets the list of past bookings.
        /// </summary>
        public List<BookingViewModel> PastBookings { get; set; }
    }
}
