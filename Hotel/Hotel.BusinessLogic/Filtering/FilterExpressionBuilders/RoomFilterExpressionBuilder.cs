// <copyright file="RoomFilterExpressionBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Filtering.FilterExpressionBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Hotel.BusinessLogic.Dtos;
    using Hotel.BusinessLogic.Filtering.Interfaces;
    using Hotel.DataAccess.Entities;

    /// <summary>
    /// Builds expression for room getting query.
    /// </summary>
    public class RoomFilterExpressionBuilder : IFilterExpressionBuilder<Room, BookFilterSettingsDto>
    {
        /// <summary>
        /// All condition filters.
        /// </summary>
        private readonly ICollection<IFilter<Room, bool>> conditionFilters;

        /// <summary>
        /// All order filters.
        /// </summary>
        private readonly ICollection<IFilter<Room, object>> orderFilters;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomFilterExpressionBuilder"/> class.
        /// </summary>
        public RoomFilterExpressionBuilder()
        {
            this.conditionFilters = new List<IFilter<Room, bool>>();
            this.orderFilters = new List<IFilter<Room, object>>();
        }

        /// <summary>
        /// Gets filter expression for query.
        /// </summary>
        /// <typeparam name="TEntity">Type of searched entity.</typeparam>
        /// <returns>Expression for query.</returns>
        public Expression<Func<Room, bool>> GetFilterExpression()
        {
            return Expression.Lambda<Func<Room, bool>>(
                Expression.Call(
                    this.conditionFilters
                    .Select(x => x.GetFilter())
                    .Aggregate((first, second) => first + second)
                    .Method));
        }

        /// <summary>
        /// Gets order expression for query.
        /// </summary>
        /// <returns>Expression for query.</returns>
        public Expression<Func<Room, object>> GetOrderExpression()
        {
            return Expression.Lambda<Func<Room, object>>(
                Expression.Call(
                    this.orderFilters
                    .Select(x => x.GetFilter())
                    .Aggregate((first, second) => first + second)
                    .Method));
        }

        /// <summary>
        /// Sets filter settings for an expression builder.
        /// </summary>
        /// <param name="filterSettings">Settings for filter expression builder.</param>
        public void SetFilterSettings(BookFilterSettingsDto filterSettings)
        {
        }
    }
}