// <copyright file="BookingViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Models.Profile
{
    using System;

    /// <summary>
    /// View model for booking.
    /// </summary>
    public class BookingViewModel
    {
        /// <summary>
        /// Gets or sets id of a booking.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets beggining time.
        /// </summary>
        public DateTime BegginingTime { get; set; }

        /// <summary>
        /// Gets or sets ending time.
        /// </summary>
        public DateTime EndingTime { get; set; }

        /// <summary>
        /// Gets or sets room class name.
        /// </summary>
        public string RoomClassName { get; set; }

        /// <summary>
        /// Gets or sets room class color.
        /// </summary>
        public string RoomColorToDisplay { get; set; }

        /// <summary>
        /// Gets or sets number of room.
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether paid or not.
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets dealine for payment.
        /// </summary>
        public DateTime DeadLine { get; set; }

        /// <summary>
        /// Gets or sets count of people in the room.
        /// </summary>
        public byte PeopleCount { get; set; }
    }
}