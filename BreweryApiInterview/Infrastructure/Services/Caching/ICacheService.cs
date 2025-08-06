using Microsoft.Extensions.Caching.Memory;

namespace BreweryApiInterview.Infrastructure.Services.Caching
{
    public interface ICacheService
    {
        bool TryGetValue<T>(object key, out T value);

        void Set<T>(object key, T value, int absoluteExpirationMinutes = 10);
        
        string GenerateKey(string prefix, object data);
    }
}
