// <copyright file="ServiceDependencies.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Dependencies
{
    using Hotel.BusinessLogic.Interfaces;
    using Hotel.BusinessLogic.Services;
    using Ninject.Modules;

    /// <summary>
    /// Registers service dependencies.
    /// </summary>
    public class ServiceDependencies : NinjectModule
    {
        /// <summary>
        /// Method to contain registrations.
        /// </summary>
        public override void Load()
        {
            this.Bind<IBookingPageService>().To<BookingService>();
            this.Bind<IProfileService>().To<ProfileService>();
            this.Bind<IAdminService>().To<AdminService>();
            this.Bind<IManageService>().To<ManageService>();
            this.Bind<IAccountService>().To<AccountService>();
            this.Bind<ILogService>().To<LogService>();
            this.Bind<IStaffService>().To<StaffService>();
        }
    }
}