// <copyright file="RoomClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Room class db entity.
    /// </summary>
    public class RoomClass : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomClass"/> class.
        /// </summary>
        public RoomClass()
        {
            this.Rooms = new List<Room>();
        }

        /// <summary>
        /// Gets or sets a name of a class.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets ot sets an info of a class.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Gets or sets the price per night for 1 person.
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Gets or sets the color to display the room class name.
        /// </summary>
        public string DisplayColor { get; set; }

        /// <summary>
        /// Gets or sets address of connected image to this class.
        /// </summary>
        public string AddressImage { get; set; }

        /// <summary>
        /// Gets or sets a list of rooms.
        /// One to many relationship.
        /// </summary>
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
