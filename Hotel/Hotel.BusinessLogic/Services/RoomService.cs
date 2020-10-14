// <copyright file="RoomService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Hotel.BusinessLogic.DTOs;
    using Hotel.BusinessLogic.Filtering.Interfaces;
    using Hotel.BusinessLogic.Services.Interfaces;
    using Hotel.DataAccess.Entities;
    using Hotel.DataAccess.Facade.Interfaces;

    /// <summary>
    /// Provides features for manipulating room data.
    /// </summary>
    public class RoomService : IRoomService
    {
        /// <summary>
        /// Provides access to different dbs.
        /// </summary>
        private readonly IDatabaseAccessFacade databaseAccess;

        /// <summary>
        /// Used for mapping objects.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Used for creating expressions for db queries.
        /// </summary>
        private readonly IFilterExpressionBuilder<Room> filterExpressionBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomService"/> class.
        /// </summary>
        /// <param name="databaseAccess">Provides access to different dbs.</param>
        /// <param name="mapper">Used for mapping objects.</param>
        /// <param name="filterExpressionBuilder">Used for creating expressions for db queries.</param>
        public RoomService(
            IDatabaseAccessFacade databaseAccess,
            IMapper mapper,
            IFilterExpressionBuilder<Room> filterExpressionBuilder)
        {
            this.databaseAccess = databaseAccess;
            this.mapper = mapper;
            this.filterExpressionBuilder = filterExpressionBuilder;
        }

        /// <summary>
        /// Gets all rooms by a filter.
        /// </summary>
        /// <returns>Enumerable collection of room object.</returns>
        public IEnumerable<RoomDTO> GetAllRooms()
        {
            this.filterExpressionBuilder.SetFilterSettings<>();

            return this.unitOfWork.Rooms
                .GetAll(
                    this.filterExpressionBuilder.GetFilterExpression(),
                    this.filterExpressionBuilder.GetOrderExpression())
                .Select(x => this.mapper.Map<RoomDTO>(x));
        }
    }
}
