// <copyright file="IFilterExpressionBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Filtering.Interfaces
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for building expressions for db queries.
    /// </summary
    /// <typeparam name="TEntity">Type of searched db entity.</typeparam>
    /// <typeparam name="TFilter">Some filter settings type.</typeparam>
    public interface IFilterExpressionBuilder<TEntity, TFilter>
    {
        /// <summary>
        /// Sets filter settings for an expression builder.
        /// </summary>
        /// <param name="filterSettings">Settings for filter expression builder.</param>
        public void SetFilterSettings(TFilter filterSettings);

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