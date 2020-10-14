// <copyright file="DataAccessorsDependencies.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Dependencies
{
    using Hotel.DataAccess.Context;
    using Hotel.DataAccess.Entities;
    using Hotel.DataAccess.Facade;
    using Hotel.DataAccess.Facade.Interfaces;
    using Hotel.DataAccess.Repositories;
    using Hotel.DataAccess.Repositories.Interfaces;
    using Hotel.DataAccess.UnitOfWork;
    using Hotel.DataAccess.UnitOfWork.Interfaces;
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
            this.Bind<IGenericRepository<Room, ApplicationDatabaseContext>>()
                .To<GenericRepository<Room, ApplicationDatabaseContext>>()
                .InThreadScope();
            this.Bind<IGenericRepository<RoomClass, ApplicationDatabaseContext>>()
                .To<GenericRepository<RoomClass, ApplicationDatabaseContext>>()
                .InThreadScope();
            this.Bind<IGenericRepository<Booking, ApplicationDatabaseContext>>()
                .To<GenericRepository<Booking, ApplicationDatabaseContext>>()
                .InThreadScope();
            this.Bind<IGenericRepository<UnluckyRequest, ApplicationDatabaseContext>>()
                .To<GenericRepository<UnluckyRequest, ApplicationDatabaseContext>>()
                .InThreadScope();
            this.Bind<IGenericRepository<Log, ApplicationDatabaseContext>>()
                .To<GenericRepository<Log, ApplicationDatabaseContext>>()
                .InThreadScope();

            this.Bind<IUnitOfWork<ApplicationDatabaseContext>>()
                .To<UnitOfWork>()
                .InThreadScope();
            this.Bind<IDatabaseAccessFacade>()
                .To<DatabaseAccessFacade>()
                .InThreadScope();

            this.Bind<ApplicationDatabaseContext>()
                .ToSelf()
                .InThreadScope();
        }
    }
}