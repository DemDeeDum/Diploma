// <copyright file="Global.asax.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Hotel.BusinessLogic.BookingKiller;
    using Hotel.BusinessLogic.Dependencies;
    using Hotel.Web.Dependecies;
    using Ninject;
    using Ninject.Web.Mvc;

    /// <summary>
    /// Main application class.
    /// </summary>
#pragma warning disable SA1649
    public class MvcApplication : HttpApplication
#pragma warning restore SA1649
    {
        /// <summary>
        /// Function which is executed as soon as application starts.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var serviceDependencies = new ServiceDependencies();
            var dataAccessorsDependencies = new DataAccessorsDependencies();
            var lazyDependecies = new LazyDependencyInjection();
            var mapperDependencies = new MapperDependecies();
            var kernel = new StandardKernel(
                lazyDependecies,
                serviceDependencies,
                dataAccessorsDependencies,
                mapperDependencies);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            Destroyer destroyer = new Destroyer();
        }
    }
}
