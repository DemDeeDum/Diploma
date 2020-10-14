// <copyright file="IDatabaseAccessFacade.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.Facade.Interfaces
{
    using Hotel.DataAccess.Context;
    using Hotel.DataAccess.UnitOfWork.Interfaces;

    /// <summary>
    /// Provides access to different db accesors.
    /// </summary>
    public interface IDatabaseAccessFacade
    {
        /// <summary>
        /// Gets unit of work.
        /// </summary>
        public IUnitOfWork<ApplicationDatabaseContext> UnitOfWork { get; }
    }
}
