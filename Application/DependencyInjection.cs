﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace Application
{
    /// <summary>
    /// Dependency injection extension to configure Application layer services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configure Application layer services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
          

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}
