// <copyright file="DatabaseAccessFacade.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.Facade
{
    using System;
    using Hotel.DataAccess.Context;
    using Hotel.DataAccess.Facade.Interfaces;
    using Hotel.DataAccess.UnitOfWork.Interfaces;

    /// <summary>
    /// Provides access to different db accesors.
    /// </summary>
    public class DatabaseAccessFacade : IDatabaseAccessFacade
    {
        /// <summary>
        /// Lazy created sql server db accessor.
        /// </summary>
        private readonly Lazy<IUnitOfWork<ApplicationDatabaseContext>> unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseAccessFacade"/> class.
        /// </summary>
        /// <param name="unitOfWork">Lazy created sql server db accessor.</param>
        public DatabaseAccessFacade(Lazy<IUnitOfWork<ApplicationDatabaseContext>> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        public IUnitOfWork<ApplicationDatabaseContext> UnitOfWork => this.unitOfWork.Value;
    }
}
