// <copyright file="IFilterExpressionBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Filtering.Interfaces
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for building expressions for db queries.
    /// </summary>
    /// <typeparam name="TEntity">Type of searched db entity.</typeparam>
    public interface IFilterExpressionBuilder<TEntity>
    {
        /// <summary>
        /// Sets filter settings for an expression builder.
        /// </summary>
        /// <typeparam name="T">Some filter settings type.</typeparam>
        /// <param name="filterSettings">Settings for filter expression builder.</param>
        public void SetFilterSettings<T>(T filterSettings);

        /// <summary>
        /// Gets filter expression for query.
        /// </summary>
        /// <typeparam name="TEntity">Type of searched entity.</typeparam>
        /// <returns>Expression for query.</returns>
        public Expression<Func<TEntity, bool>> GetFilterExpression();

        /// <summary>
        /// Gets order expression for query.
        /// </summary>
        /// <returns>Expression for query.</returns>
        public Expression<Func<TEntity, object>> GetOrderExpression();
    }
}