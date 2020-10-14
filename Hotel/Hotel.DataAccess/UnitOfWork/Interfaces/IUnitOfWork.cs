// <copyright file="IUnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.UnitOfWork.Interfaces
{
    using System.Data.Entity;
    using Hotel.DataAccess.Entities;
    using Hotel.DataAccess.Repositories.Interfaces;

    /// <summary>
    /// Provides access to db with transactional system.
    /// </summary>
    /// <typeparam name="TContext">Any db context.</typeparam>
    public interface IUnitOfWork<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Gets room repository.
        /// </summary>
        public IGenericRepository<Room, TContext> Rooms { get; }

        /// <summary>
        /// Gets room class repository.
        /// </summary>
        public IGenericRepository<RoomClass, TContext> RoomClasses { get; }

        /// <summary>
        /// Gets booking repository.
        /// </summary>
        public IGenericRepository<Booking, TContext> Bookings { get; }

        /// <summary>
        /// Gets unlucky request repository.
        /// </summary>
        public IGenericRepository<UnluckyRequest, TContext> UnluckyRequests { get; }

        /// <summary>
        /// Gets log repository.
        /// </summary>
        public IGenericRepository<Log, TContext> Logs { get; }

        /// <summary>
        /// Save changes in the database.
        /// </summary>
        public void SaveChanges();
    }
}