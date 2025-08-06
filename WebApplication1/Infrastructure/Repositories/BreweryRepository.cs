using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Enums;
using WebApplication1.Domain.Repositories;
using WebApplication1.Domain.ValueObjects;
using WebApplication1.Infrastructure.ExternalModels;

namespace WebApplication1.Infrastructure.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private const string BreweriesApiUrl = "https://api.openbrewerydb.org/v1/breweries";
        private const string CacheKey = "BreweriesList";
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BreweryRepository> _logger;

        public BreweryRepository(IMemoryCache cache, HttpClient httpClient, ILogger<BreweryRepository> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Brewery>> GetAllAsync()
        {
            try
            {
                // Try to get breweries from cache first
                if (!_cache.TryGetValue(CacheKey, out List<Brewery>? breweries))
                {
                    _logger.LogInformation("Fetching breweries from API");
                    // If not in cache, fetch from API
                    var apiBreweries = await FetchBreweriesFromApiAsync();

                    // Map API model to domain entities
                    breweries = apiBreweries.Select(MapToDomainEntity).ToList();

                    // Cache the results for 10 minutes
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(CacheKey, breweries, cacheOptions);
                }
                else
                {
                    _logger.LogInformation("Fetching breweries from cache");
                }

                return breweries ?? new List<Brewery>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting breweries");
                throw;
            }
        }

        public async ValueTask<IEnumerable<Brewery>> GetFilteredAsync(BreweryQueryParameters queryParameters)
        {
            var breweries = await GetAllAsync();
            var result = breweries.AsEnumerable();

            // Apply search filtering if provided
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                result = result.Where(b =>
                    b.Name.Contains(queryParameters.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    b.City.Contains(queryParameters.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Apply sorting if provided
            if (queryParameters.SortBy.HasValue)
            {
                result = SortBreweries(result, queryParameters.SortBy.Value, queryParameters.SortDirection);
            }

            return result;
        }

        private static IEnumerable<Brewery> SortBreweries(IEnumerable<Brewery> breweries, SortBy sortBy, SortDirection direction)
        {
            return sortBy switch
            {
                SortBy.Name => direction == SortDirection.Ascending
                    ? breweries.OrderBy(b => b.Name)
                    : breweries.OrderByDescending(b => b.Name),

                SortBy.City => direction == SortDirection.Ascending
                    ? breweries.OrderBy(b => b.City)
                    : breweries.OrderByDescending(b => b.City),

                SortBy.Distance => direction == SortDirection.Ascending
                    ? breweries.OrderBy(b => b.Distance)
                    : breweries.OrderByDescending(b => b.Distance),

                _ => breweries
            };
        }

        private static Brewery MapToDomainEntity(BreweryApiModel apiBrewery)
        {
            return Brewery.Create(
                id: apiBrewery.Id,
                name: apiBrewery.Name,
                city: apiBrewery.City,
                phoneNumber: apiBrewery.Phone
            );
        }

        private async Task<List<BreweryApiModel>> FetchBreweriesFromApiAsync()
        {
            try
            {
                // Implement pagination to fetch all breweries
                var allBreweries = new List<BreweryApiModel>();
                int pageSize = 50;
                int pageNumber = 1;
                List<BreweryApiModel> pageResults;

                do
                {
                    // Construct URL with pagination parameters
                    string url = $"{BreweriesApiUrl}?page={pageNumber}&per_page={pageSize}";
                    _logger.LogInformation("Fetching breweries page {PageNumber}", pageNumber);

                    pageResults = await _httpClient.GetFromJsonAsync<List<BreweryApiModel>>(url) ?? new List<BreweryApiModel>();

                    if (pageResults.Count > 0)
                    {
                        allBreweries.AddRange(pageResults);
                        pageNumber++;
                    }

                } while (pageResults != null && pageResults.Count == pageSize);

                _logger.LogInformation("Fetched a total of {Count} breweries", allBreweries.Count);
                return allBreweries;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching breweries from API: {Message}", ex.Message);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing API response: {Message}", ex.Message);
                throw;
            }
        }
    }
}
