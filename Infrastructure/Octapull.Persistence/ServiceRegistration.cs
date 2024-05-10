using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octapull.Application.Abstractions;
using Octapull.Application.Interfaces;
using Octapull.Infrastructure.Services;
using Octapull.Persistence.Contexts.Application;
using Octapull.Persistence.Services;

namespace Octapull.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //IConfigurationRoot configuration2 = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var connectionStringApplication = configuration.GetSection("ConnectionStrings:MSSQLConnectionString").Value;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetSection("MSSQLConnectionString").Value);
            });

            services.AddScoped<IMeetingService,MeetingService>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IIdentityService,IdentityService>();

            return services;
        }
    }
}
