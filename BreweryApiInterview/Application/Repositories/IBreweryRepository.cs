using BreweryApiInterview.Domain.Entities;
using BreweryApiInterview.Application.ValueObjects;

namespace BreweryApiInterview.Application.Repositories
{
    public interface IBreweryRepository
    { 
        Task<IEnumerable<Brewery>> GetFilteredAsync(BreweryQueryParameters queryParameters);
    }
}
