// <copyright file="UnluckyRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Entities
{
    using System;

    /// <summary>
    /// Unlucky request db entity.
    /// </summary>
    public class UnluckyRequest : BaseEntity
    {
        /// <summary>
        /// Gets or sets room class name.
        /// </summary>
        public string RoomClassName { get; set; }

        /// <summary>
        /// Gets or sets people count.
        /// </summary>
        public byte PeopleCount { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets finish date.
        /// </summary>
        public DateTime FinishDate { get; set; }

        /// <summary>
        /// Gets or sets application user id.
        /// One to many relationship.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets application user.
        /// One to many relationship.
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}
