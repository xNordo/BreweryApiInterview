using BreweryApiInterview.Application.Services;
using BreweryApiInterview.Application.Repositories;
using BreweryApiInterview.Infrastructure.Repositories;

namespace BreweryApiInterview.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBreweryService, BreweryService>();

            return services;
        }
    }
}