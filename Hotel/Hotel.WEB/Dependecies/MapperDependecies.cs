// <copyright file="MapperDependecies.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Dependecies
{
    using AutoMapper;
    using Hotel.WEB.Infrastructure;
    using Ninject.Modules;

    /// <summary>
    /// Registers mapper dependencies.
    /// </summary>
    public class MapperDependecies : NinjectModule
    {
        /// <summary>
        /// Method to contain registrations.
        /// </summary>
        public override void Load()
        {
            var configuration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WebMapper());
                mc.AddProfile(new BllMapper());
            });

            this.Bind<IMapper>().ToConstant(configuration.CreateMapper());
        }
    }
}