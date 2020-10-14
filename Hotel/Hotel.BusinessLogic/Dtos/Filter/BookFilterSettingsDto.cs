// <copyright file="BookFilterSettingsDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Dtos
{
    using System;

    /// <summary>
    /// Data transferring object.
    /// </summary>
    public class BookFilterSettingsDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether it should order by price.
        /// </summary>
        public bool PriceFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it should order by people count.
        /// </summary>
        public bool PeopleCountFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it should order by room class prestige.
        /// </summary>
        public bool RoomClassFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it should order by room status.
        /// </summary>
        public bool StatusFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it should order by ascending.
        /// </summary>
        public bool Ascending { get; set; }

        /// <summary>
        /// Gets or sets start date to filter by.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date to filter by.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
