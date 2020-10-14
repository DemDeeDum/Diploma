// <copyright file="IRoomService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Services.Interfaces
{
    using System.Collections.Generic;
    using Hotel.BLL.DTOs;

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
