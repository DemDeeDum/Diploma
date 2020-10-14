// <copyright file="IFilter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Filtering.Interfaces
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Filters some collection.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Gets expression for some filtering.
        /// </summary>
        /// <typeparam name="TEntity">Some entity whose collection to be filtered.</typeparam>
        /// <typeparam name="TResult">Result for function.</typeparam>
        /// <returns>Expression to be used for filtering.</returns>
        public Expression<Func<TEntity, TResult>> GetFilter<TEntity, TResult>();
    }
}
