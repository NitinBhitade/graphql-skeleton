using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    { /// <summary>
      /// Configure Infrastructure layer services.
      /// </summary>
      /// <param name="services"></param>
      /// <param name="configuration"></param>
      /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
         
                services.AddPooledDbContextFactory<ApplicationDbContext>(_ =>
                    _.UseNpgsql(
                        configuration.GetConnectionString("PostgresDbConnection"),
                        a =>
                            a.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

           
           // services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleRepository>(_ =>
                 new RoleRepository(
                     _.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext()));
            return services;
        }
    }
}
