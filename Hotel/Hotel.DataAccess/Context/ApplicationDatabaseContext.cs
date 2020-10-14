// <copyright file="ApplicationDatabaseContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.Context
{
    using System.Data.Entity;
    using Hotel.DataAccess.Entities;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Db context for the application.
    /// </summary>
    public class ApplicationDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes static members of the <see cref="ApplicationDatabaseContext"/> class.
        /// </summary>
        static ApplicationDatabaseContext()
        {
            Database.SetInitializer(new ApplicationDatabaseContextInitializer());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDatabaseContext"/> class.
        /// </summary>
        public ApplicationDatabaseContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        /// <summary>
        /// Gets or sets room dt set.
        /// </summary>
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// Gets or sets room class db set.
        /// </summary>
        public DbSet<RoomClass> RoomClasses { get; set; }

        /// <summary>
        /// Gets or sets booking db set.
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }

        /// <summary>
        /// Gets or sets unlucky request db set.
        /// </summary>
        public DbSet<UnluckyRequest> UnluckyRequests { get; set; }

        /// <summary>
        /// Gets or sets log db set.
        /// </summary>
        public DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationDatabaseContext"/> class.
        /// </summary>
        /// <returns><see cref="ApplicationDatabaseContext"/> instance.</returns>
        public static ApplicationDatabaseContext Create()
        {
            return new ApplicationDatabaseContext();
        }
    }
}
