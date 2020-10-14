// <copyright file="IFilter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Filtering.Interfaces
{
    using System;

    /// <summary>
    /// Filters some collection.
    /// </summary>
    /// <typeparam name="TEntity">Some entity whose collection to be filtered.</typeparam>
    /// <typeparam name="TResult">Result for function.</typeparam>
    public interface IFilter<TEntity, TResult>
    {
        /// <summary>
        /// Gets expression for some filtering.
        /// </summary>
        /// <returns>Expression to be used for filtering.</returns>
        public Func<TEntity, TResult> GetFilter();
    }
}
