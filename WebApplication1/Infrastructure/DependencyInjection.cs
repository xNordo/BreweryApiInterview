using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Application.Repositories;
using WebApplication1.Infrastructure.Repositories;
using WebApplication1.Infrastructure.Services.BreweryApi;
using WebApplication1.Infrastructure.Services.Caching;

namespace WebApplication1.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClient();

            services.AddSingleton<ICacheService, MemoryCacheService>();
            services.AddScoped<IBreweryApiService, BreweryApiService>();

            services.AddScoped<IBreweryRepository, BreweryRepository>();

            return services;
        }
    }
}
