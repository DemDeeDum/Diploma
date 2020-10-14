// <copyright file="UnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.UnitOfWork
{
    using System;
    using Hotel.DAL.Context;
    using Hotel.DAL.Entities;
    using Hotel.DAL.Repositories.Interfaces;
    using Hotel.DAL.UnitOfWork.Interfaces;

    /// <summary>
    /// Provides access to db with transactional system.
    /// </summary>
    public class UnitOfWork : IUnitOfWork<ApplicationDbContext>
    {
        /// <summary>
        /// Provides access to db.
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Lazy room repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<Room, ApplicationDbContext>> rooms;

        /// <summary>
        /// Lazy room class repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<RoomClass, ApplicationDbContext>> roomClasses;

        /// <summary>
        /// Lazy booking repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<Booking, ApplicationDbContext>> bookings;

        /// <summary>
        /// Lazy unlucky requests repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<UnluckyRequest, ApplicationDbContext>> unluckyRequests;

        /// <summary>
        /// Lazy log repository.
        /// </summary>
        private readonly Lazy<IGenericRepository<Log, ApplicationDbContext>> logs;

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
            ApplicationDbContext context,
            Lazy<IGenericRepository<Room, ApplicationDbContext>> rooms,
            Lazy<IGenericRepository<RoomClass, ApplicationDbContext>> roomClasses,
            Lazy<IGenericRepository<Booking, ApplicationDbContext>> bookings,
            Lazy<IGenericRepository<UnluckyRequest, ApplicationDbContext>> unluckyRequests,
            Lazy<IGenericRepository<Log, ApplicationDbContext>> logs)
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
        public IGenericRepository<Room, ApplicationDbContext> Rooms => this.rooms.Value;

        /// <summary>
        /// Gets room class repository.
        /// </summary>
        public IGenericRepository<RoomClass, ApplicationDbContext> RoomClasses => this.roomClasses.Value;

        /// <summary>
        /// Gets booking repository.
        /// </summary>
        public IGenericRepository<Booking, ApplicationDbContext> Bookings => this.bookings.Value;

        /// <summary>
        /// Gets unlucky request repository.
        /// </summary>
        public IGenericRepository<UnluckyRequest, ApplicationDbContext> UnluckyRequests => this.unluckyRequests.Value;

        /// <summary>
        /// Gets log repository.
        /// </summary>
        public IGenericRepository<Log, ApplicationDbContext> Logs => this.logs.Value;

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
