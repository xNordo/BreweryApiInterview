using BreweryApiInterview.Application.ValueObjects;
using BreweryApiInterview.Infrastructure.ExternalModels;

namespace BreweryApiInterview.Infrastructure.Services.BreweryApi
{
    public interface IBreweryApiService
    {
        Task<List<BreweryApiModel>> FetchBreweriesAsync(BreweryQueryParameters queryParameters);
    }
}
