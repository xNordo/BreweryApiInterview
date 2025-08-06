using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BreweryApiInterview.Infrastructure.Services.Caching
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<MemoryCacheService> _logger;

        public MemoryCacheService(IMemoryCache cache, ILogger<MemoryCacheService> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool TryGetValue<T>(object key, out T value)
        {
            bool found = _cache.TryGetValue(key, out object? cachedItem);
            if (found && cachedItem is T typedValue)
            {
                value = typedValue;
                _logger.LogInformation("CACHE HIT - Key: {Key}", key);
            }
            else
            {
                value = default!;
                _logger.LogInformation("CACHE MISS - Key: {Key}", key);
            }

            return found;
        }

        public void Set<T>(object key, T value, int absoluteExpirationMinutes = 10)
        {
            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpirationMinutes));

            _cache.Set(key, value, options);
            _logger.LogInformation("CACHE SET - Key: {Key}, Expiration: {Expiration} minutes", 
                key, absoluteExpirationMinutes);
        }

        public string GenerateKey(string prefix, object data)
        {
            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            string dataJson = JsonSerializer.Serialize(data, jsonOptions);
            int hashCode = dataJson.GetHashCode();

            return $"{prefix}_{Math.Abs(hashCode)}";
        }
    }
}
