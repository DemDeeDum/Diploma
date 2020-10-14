// <copyright file="BaseEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Entities
{
    using System;

    /// <summary>
    /// Abstract entity for coherency in generic repository.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets id of an entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether entity is deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
