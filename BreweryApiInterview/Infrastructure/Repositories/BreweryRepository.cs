using BreweryApiInterview.Domain.Entities;
using BreweryApiInterview.Application.Repositories;
using BreweryApiInterview.Application.ValueObjects;
using BreweryApiInterview.Infrastructure.Mappers;
using BreweryApiInterview.Infrastructure.Services.BreweryApi;
using BreweryApiInterview.Infrastructure.Services.Caching;

namespace BreweryApiInterview.Infrastructure.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private const string CacheKeyPrefix = "BreweriesList";
        private readonly ICacheService _cacheService;
        private readonly IBreweryApiService _apiService;
        private readonly ILogger<BreweryRepository> _logger;

        public BreweryRepository(
            ICacheService cacheService, 
            IBreweryApiService apiService, 
            ILogger<BreweryRepository> logger)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Brewery>> GetFilteredAsync(BreweryQueryParameters queryParameters)
        {
            try
            { 
                string cacheKey = _cacheService.GenerateKey(CacheKeyPrefix, queryParameters);

                if (_cacheService.TryGetValue(cacheKey, out List<Brewery> cachedBreweries))
                {
                    _logger.LogInformation("Repository: Returning {Count} breweries from cache", cachedBreweries.Count);
                    return cachedBreweries;
                }

                _logger.LogInformation("Repository: Cache miss, fetching from API");
                var apiBreweries = await _apiService.FetchBreweriesAsync(queryParameters);

                var breweries = BreweryMapper.ToDomainEntities(apiBreweries).ToList();

                _cacheService.Set(cacheKey, breweries, 10);

                _logger.LogInformation("Repository: Stored {Count} breweries in cache", breweries.Count);
                return breweries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Error getting breweries: {Message}", ex.Message);
                throw;
            }
        }

    }
}
