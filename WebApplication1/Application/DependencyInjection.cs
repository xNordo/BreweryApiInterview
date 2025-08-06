using WebApplication1.Application.Services;

namespace WebApplication1.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<IBreweryService, BreweryService>();

            return services;
        }
    }
}