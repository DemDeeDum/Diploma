﻿// <copyright file="RoomService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Hotel.BLL.DTOs;
    using Hotel.BLL.Filtering.Interfaces;
    using Hotel.BLL.Services.Interfaces;
    using Hotel.DAL.Context;
    using Hotel.DAL.Entities;
    using Hotel.DAL.UnitOfWork.Interfaces;

    /// <summary>
    /// Provides features for manipulating room data.
    /// </summary>
    public class RoomService : IRoomService
    {
        /// <summary>
        /// Provides access to a db.
        /// </summary>
        private readonly IUnitOfWork<ApplicationDbContext> unitOfWork;

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
        /// <param name="unitOfWork">Provides access to a db.</param>
        /// <param name="mapper">Used for mapping objects.</param>
        /// <param name="filterExpressionBuilder">Used for creating expressions for db queries.</param>
        public RoomService(
            IUnitOfWork<ApplicationDbContext> unitOfWork,
            IMapper mapper,
            IFilterExpressionBuilder<Room> filterExpressionBuilder)
        {
            this.unitOfWork = unitOfWork;
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
