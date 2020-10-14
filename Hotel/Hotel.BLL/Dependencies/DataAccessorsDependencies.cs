// <copyright file="DataAccessorsDependencies.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Dependencies
{
    using System;
    using Hotel.DAL.Context;
    using Hotel.DAL.Entities;
    using Hotel.DAL.Repositories.Interfaces;
    using Hotel.DAL.UnitOfWork;
    using Hotel.DAL.UnitOfWork.Interfaces;
    using Ninject.Modules;

    /// <summary>
    /// Registers data accessors dependencies.
    /// </summary>
    public class DataAccessorsDependencies : NinjectModule
    {
        /// <summary>
        /// Method to contain registrations.
        /// </summary>
        public override void Load()
        {
            this.Bind<Lazy<IGenericRepository<Room, ApplicationDbContext>>>()
                .To<Lazy<IGenericRepository<Room, ApplicationDbContext>>>();
            this.Bind<Lazy<IGenericRepository<RoomClass, ApplicationDbContext>>>()
                .To<Lazy<IGenericRepository<RoomClass, ApplicationDbContext>>>();
            this.Bind<Lazy<IGenericRepository<Booking, ApplicationDbContext>>>()
                .To<Lazy<IGenericRepository<Booking, ApplicationDbContext>>>();
            this.Bind<Lazy<IGenericRepository<UnluckyRequest, ApplicationDbContext>>>()
                .To<Lazy<IGenericRepository<UnluckyRequest, ApplicationDbContext>>>();
            this.Bind<Lazy<IGenericRepository<Log, ApplicationDbContext>>>()
                .To<Lazy<IGenericRepository<Log, ApplicationDbContext>>>();

            this.Bind<IUnitOfWork<ApplicationDbContext>>().To<UnitOfWork>();
        }
    }
}