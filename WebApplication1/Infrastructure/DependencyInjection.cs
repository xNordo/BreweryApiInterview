using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Application.Services;
using WebApplication1.Domain.Repositories;
using WebApplication1.Infrastructure.Repositories;

namespace WebApplication1.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClient();

            services.AddScoped<IBreweryRepository, BreweryRepository>();
            services.AddScoped<IBreweryService, BreweryService>();

            return services;
        }
    }
}