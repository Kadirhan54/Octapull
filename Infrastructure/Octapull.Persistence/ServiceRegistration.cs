using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octapull.Application.Abstractions;
using Octapull.Infrastructure.Services;
using Octapull.Persistence.Contexts.Application;
using Octapull.Persistence.Services;

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

            services.AddSingleton<IMeetingService,MeetingService>();
            services.AddSingleton<ITokenService,TokenService>();

            return services;
        }
    }
}
