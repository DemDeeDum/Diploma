// <copyright file="ApplicationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Context
{
    using System.Data.Entity;
    using Hotel.DAL.Entities;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Db context for the application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes static members of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbContextInitializer());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
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
        /// Creates a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <returns><see cref="ApplicationDbContext"/> instance.</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
