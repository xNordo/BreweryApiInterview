using BreweryApiInterview.Application.Repositories;
using BreweryApiInterview.Infrastructure.Repositories;
using BreweryApiInterview.Infrastructure.Services.BreweryApi;
using BreweryApiInterview.Infrastructure.Services.Caching;

namespace BreweryApiInterview.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClient();

            services.AddSingleton<ICacheService, MemoryCacheService>();
            services.AddScoped<IBreweryApiService, BreweryApiService>();
            services.AddScoped<IBreweryRepository, BreweryRepository>();

        }
    }
}