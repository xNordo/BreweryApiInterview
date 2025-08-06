using BreweryApiInterview.Domain.Entities;
using BreweryApiInterview.Application.ValueObjects;

namespace BreweryApiInterview.Application.Services
{
    public interface IBreweryService
    {
        Task<IEnumerable<Brewery>> GetBreweriesAsync(BreweryQueryParameters queryParameters);
    }
}