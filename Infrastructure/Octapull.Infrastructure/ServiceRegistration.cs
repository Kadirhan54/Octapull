using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octapull.Application.Abstractions.Storage;
using Octapull.Infrastructure.Services.Storage;

namespace Octapull.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBlobService, BlobService>();

            services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));

            return services;
        }
    }
}
