// <copyright file="IRoomService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Services.Interfaces
{
    using System.Collections.Generic;
    using Hotel.BusinessLogic.DTOs;

    /// <summary>
    /// Provides features for manipulating room data.
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Gets all rooms by a filter.
        /// </summary>
        /// <returns>Enumerable collection of room object.</returns>
        public IEnumerable<RoomDTO> GetAllRooms();
    }
}
