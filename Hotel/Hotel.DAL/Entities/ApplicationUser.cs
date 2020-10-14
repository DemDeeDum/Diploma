// <copyright file="ApplicationUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Entities
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Application user db entity.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
        /// </summary>
        public ApplicationUser()
        {
            this.Bookings = new List<Booking>(); 
            this.UnluckyRequests = new List<UnluckyRequest>();
        }

        /// <summary>
        /// Gets or sets booking collection.
        /// One to many relationship.
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; }

        /// <summary>
        /// Gets or sets unlucky request collection.
        /// One to many relationship.
        /// </summary>
        public virtual ICollection<UnluckyRequest> UnluckyRequests { get; set; }

        /// <summary>
        /// Generates new user identity.
        /// </summary>
        /// <param name="manager">An instance of user manager.</param>
        /// <returns>New user identity.</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}