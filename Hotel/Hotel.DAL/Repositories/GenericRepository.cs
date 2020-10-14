// <copyright file="GenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Hotel.DAL.Entities;
    using Hotel.DAL.Repositories.Interfaces;

    /// <summary>
    /// Provides db operations for a certain entity.
    /// </summary>
    /// <typeparam name="TEntity">Any db entity.</typeparam>
    /// <typeparam name="TContext">Any db context.</typeparam>
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        /// <summary>
        /// Provides access to a db.
        /// </summary>
        private readonly TContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity, TContext}"/> class.
        /// </summary>
        /// <param name="context">Provides access to a db.</param>
        public GenericRepository(TContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all filtered and ordered entity collection.
        /// </summary>
        /// <param name="filter">To filter a collection.</param>
        /// <param name="orderBy">To order a collection.</param>
        /// <returns>IEnumerable collection of entities.</returns>
        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null)
        {
            return this.context.Set<TEntity>()
                .Where(filter ?? (x => true))
                .OrderBy(orderBy ?? (x => x))
                .AsEnumerable();
        }

        /// <summary>
        /// Gets entity by a condition.
        /// </summary>
        /// <param name="condition">Condition to search by.</param>
        /// <returns>Entity object.</returns>
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> condition = null)
        {
            return await this.context.Set<TEntity>()
                .FirstOrDefaultAsync(condition ?? (x => true));
        }

        /// <summary>
        /// Updates entity in db.
        /// </summary>
        /// <param name="entity">Some entity to be updated.</param>
        public void Update(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes some entity by its id.
        /// </summary>
        /// <param name="id">Some id to be deleted by.</param>
        /// <returns>Waiting for task.</returns>
        public async Task Delete(Guid id)
        {
            (await this.context.Set<TEntity>().FindAsync(id)).IsDeleted = true;
        }
    }
}
