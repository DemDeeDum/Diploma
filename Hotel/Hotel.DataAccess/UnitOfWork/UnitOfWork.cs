// <copyright file="UnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.UnitOfWork
{
    using System;
    using Hotel.DataAccess.Context;
    using Hotel.DataAccess.Entities;
    using Hotel.DataAccess.Repositories.Interfaces;
    using Hotel.DataAccess.UnitOfWork.Interfaces;

    /// <summary>
    /// Provides access to db with transactional system.
    /// </summary>
    public class UnitOfWork : IUnitOfWork<ApplicationDatabaseContext>
    {
        /// <summary>
        /// Provides access to db.
        /// </summary>
        private readonly ApplicationDatabaseContext context;

        /// <summary>
        /// Lazy room repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<Room, ApplicationDatabaseContext>> rooms;

        /// <summary>
        /// Lazy room class repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<RoomClass, ApplicationDatabaseContext>> roomClasses;

        /// <summary>
        /// Lazy booking repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<Booking, ApplicationDatabaseContext>> bookings;

        /// <summary>
        /// Lazy unlucky requests repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<UnluckyRequest, ApplicationDatabaseContext>> unluckyRequests;

        /// <summary>
        /// Lazy log repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<Log, ApplicationDatabaseContext>> logs;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">Provides access to db.</param>
        /// <param name="rooms">Lazy room repository.</param>
        /// <param name="roomClasses">Lazy room classes repository.</param>
        /// <param name="bookings">Lazy booking repository.</param>
        /// <param name="unluckyRequests">Lazy unlucky request repository.</param>
        /// <param name="logs">Lazy log repository.</param>
        public UnitOfWork(
            ApplicationDatabaseContext context,
            Lazy<IGenericRepository<Room, ApplicationDatabaseContext>> rooms,
            Lazy<IGenericRepository<RoomClass, ApplicationDatabaseContext>> roomClasses,
            Lazy<IGenericRepository<Booking, ApplicationDatabaseContext>> bookings,
            Lazy<IGenericRepository<UnluckyRequest, ApplicationDatabaseContext>> unluckyRequests,
            Lazy<IGenericRepository<Log, ApplicationDatabaseContext>> logs)
        {
            this.context = context;
            this.rooms = rooms;
            this.roomClasses = roomClasses;
            this.bookings = bookings;
            this.unluckyRequests = unluckyRequests;
            this.logs = logs;
        }

        /// <summary>
        /// Gets room repository.
        /// </summary>
        public IGenericRepository<Room, ApplicationDatabaseContext> Rooms => this.rooms.Value;

        /// <summary>
        /// Gets room class repository.
        /// </summary>
        public IGenericRepository<RoomClass, ApplicationDatabaseContext> RoomClasses => this.roomClasses.Value;

        /// <summary>
        /// Gets booking repository.
        /// </summary>
        public IGenericRepository<Booking, ApplicationDatabaseContext> Bookings => this.bookings.Value;

        /// <summary>
        /// Gets unlucky request repository.
        /// </summary>
        public IGenericRepository<UnluckyRequest, ApplicationDatabaseContext> UnluckyRequests => this.unluckyRequests.Value;

        /// <summary>
        /// Gets log repository.
        /// </summary>
        public IGenericRepository<Log, ApplicationDatabaseContext> Logs => this.logs.Value;

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
