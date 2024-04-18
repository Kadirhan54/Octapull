using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octapull.Persistence.Contexts.Application;

namespace Octapull.Persistence
{
    public static class DependencyInjection
    {
        //private static readonly IConfiguration _configuration;

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionStringApplication = configuration.GetSection("MSSQLConntectionString").Value;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionStringApplication);
            });

            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseNpgsql(connectionStringApplication);
            //});

            //services.AddDbContextFactory<EpicuriousIdentityContext>();
            //services.AddDbContextFactory<ApplicationDbContextFactory>();

            return services;
        }
    }
}
