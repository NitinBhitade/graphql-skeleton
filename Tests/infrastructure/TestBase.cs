using Bogus;
using Domain;
using Infrastructure;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.infrastructure
{
    public class TestBase
    {
        private static readonly ServiceProvider _serviceProvider;

        protected TestBase()
        {

        }
        static TestBase()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.Test.json")
                .Build();

            var services = new ServiceCollection();

            services.AddInfrastructure(configuration);
            _serviceProvider = services.BuildServiceProvider();
        }


        public static T GetService<T>()
        {
            try
            {
                return _serviceProvider.GetRequiredService<T>();
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public ApplicationDbContext GetContext()
        {
            var context = GetService<IDbContextFactory<ApplicationDbContext>>();
            return context.CreateDbContext();
        }
        protected static async Task InitializeAsync(ApplicationDbContext db)
        {
            try
            {


                await db.Database.MigrateAsync();

                // already seeded
                if (db.Role.Any())
                    return;

                // sample data will be different due
                // to the nature of generating data
                var fake = new Faker<Role>()
                    .Rules((f, v) => v.Name = f.Vehicle.Model());
                var roles = fake.Generate(10);

                db.Role.AddRange(roles);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
