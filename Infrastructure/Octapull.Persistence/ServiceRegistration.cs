using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octapull.Persistence.Contexts.Application;

namespace Octapull.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionStringApplication = configuration.GetSection("MSSQLConnectionString").Value;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionStringApplication);
            });

            return services;
        }
    }
}
