// <copyright file="Log.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DataAccess.Entities
{
    using System;

    /// <summary>
    /// Log db entity.
    /// </summary>
    public class Log : BaseEntity
    {
        /// <summary>
        /// Gets or sets a date time of a log.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets url.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is access was given through the mobile phone.
        /// </summary>
        public bool IsMobile { get; set; }

        /// <summary>
        /// Gets or sets a platform.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets a browser name.
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// Gets or sets a browser version.
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// Gets or sets javascript version.
        /// </summary>
        public string JavasriptVersion { get; set; }

        /// <summary>
        /// Gets or sets an exception.
        /// </summary>
        public string Exeption { get; set; }
    }
}
