// <copyright file="Room.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Room db entity.
    /// </summary>
    public class Room : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room()
        {
            this.Bookings = new List<Booking>();
        }

        /// <summary>
        /// Gets or sets a number of a room.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets count of people which can stay in a room.
        /// </summary>
        public byte PeopleCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this room is accessible.
        /// </summary>
        public bool IsAccessible { get; set; } = true;

        /// <summary>
        /// Gets or sets a room class id.
        /// One to many relationship.
        /// </summary>
        public Guid RoomClassId { get; set; }

        /// <summary>
        /// Gets or sets a booking list.
        /// One to many relationship.
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; }

        /// <summary>
        /// Gets or sets a room class.
        /// One to many relationship.
        /// </summary>
        public virtual RoomClass RoomClass { get; set; }
    }
}
