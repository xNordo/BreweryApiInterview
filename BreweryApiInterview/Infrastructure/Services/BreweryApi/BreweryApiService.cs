using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Reflection;
using BreweryApiInterview.Application.Enums;
using BreweryApiInterview.Application.ValueObjects;
using BreweryApiInterview.Infrastructure.ExternalModels;

namespace BreweryApiInterview.Infrastructure.Services.BreweryApi
{
    public class BreweryApiService : IBreweryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BreweryApiService> _logger;

        public BreweryApiService(HttpClient httpClient, ILogger<BreweryApiService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<BreweryApiModel>> FetchBreweriesAsync(BreweryQueryParameters queryParameters)
        {
            string url = BreweryApiUrlBuilder.FromQueryParameters(queryParameters).ToString();

            _logger.LogInformation("API REQUEST - Fetching breweries: {Url}", url);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

            _logger.LogInformation("API RESPONSE - Status: {StatusCode}, Duration: {Duration}ms",
                (int)response.StatusCode,
                duration);

            response.EnsureSuccessStatusCode();

            var breweries = await response.Content.ReadFromJsonAsync<List<BreweryApiModel>>() ?? new List<BreweryApiModel>();

            _logger.LogInformation("API RESPONSE - Received {Count} breweries", breweries.Count);

            return breweries;
        }

        
    }
}
