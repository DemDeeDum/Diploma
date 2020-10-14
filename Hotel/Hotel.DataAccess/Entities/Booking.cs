// <copyright file="Booking.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.Entities
{
    using System;

    /// <summary>
    /// Booking db entity.
    /// </summary>
    public class Booking : BaseEntity
    {
        /// <summary>
        /// Gets or sets beginning date of booking.
        /// </summary>
        public DateTime BeginningDate { get; set; }

        /// <summary>
        /// Gets or sets ending date of booking.
        /// </summary>
        public DateTime EndingDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether booking is confirmed.
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a deadline for paying a booking.
        /// </summary>
        public DateTime DeadLine { get; set; }

        /// <summary>
        /// Gets or sets room id.
        /// One to many relationship.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Gets or sets application user id.
        /// One to many relationship.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets room.
        /// One to many relationship.
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        /// Gets or sets user.
        /// One to many relationship.
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}
