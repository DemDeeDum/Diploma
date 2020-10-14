// <copyright file="IGenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Hotel.DAL.Entities;

    /// <summary>
    /// Provides db operations for a certain entity.
    /// </summary>
    /// <typeparam name="TEntity">Any db entity.</typeparam>
    /// <typeparam name="TContext">Any db context.</typeparam>
    public interface IGenericRepository<TEntity, TContext>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        /// <summary>
        /// Gets all filtered and ordered entity collection.
        /// </summary>
        /// <param name="filter">To filter a collection.</param>
        /// <param name="orderBy">To order a collection.</param>
        /// <returns>IEnumerable collection of entities.</returns>
        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null);

        /// <summary>
        /// Gets entity by a condition.
        /// </summary>
        /// <param name="condition">Condition to search by.</param>
        /// <returns>Entity object.</returns>
        public Task<TEntity> Get(Expression<Func<TEntity, bool>> condition = null);

        /// <summary>
        /// Updates entity in db.
        /// </summary>
        /// <param name="entity">Some entity to be updated.</param>
        public void Update(TEntity entity);

        /// <summary>
        /// Deletes some entity by its id.
        /// </summary>
        /// <param name="id">Some id to be deleted by.</param>
        /// <returns>Waiting for task.</returns>
        public Task Delete(Guid id);
    }
}
