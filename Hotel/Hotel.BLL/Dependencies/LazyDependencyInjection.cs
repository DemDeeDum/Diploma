// <copyright file="LazyDependencyInjection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Dependencies
{
    using System;
    using System.Reflection;
    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// Register an opportunity of lazy dependecies.
    /// </summary>
    public class LazyDependencyInjection : NinjectModule
    {
        /// <summary>
        /// Method to contain registrations.
        /// </summary>
        public override void Load()
        {
            this.Bind(typeof(Lazy<>)).ToMethod(ctx =>
                    this.GetType()
                        .GetMethod(nameof(this.GetLazyProvider), BindingFlags.Instance | BindingFlags.NonPublic)
                        .MakeGenericMethod(ctx.GenericArguments[0])
                        .Invoke(this, new object[] { ctx.Kernel }));
        }

        /// <summary>
        /// Gets lazy instance of an object.
        /// </summary>
        /// <typeparam name="T">Some type.</typeparam>
        /// <param name="kernel">DI container.</param>
        /// <returns>Lazy instance of T.</returns>
        protected Lazy<T> GetLazyProvider<T>(IKernel kernel)
        {
            return new Lazy<T>(() => kernel.Get<T>());
        }
    }
}
