using WebApplication1.Domain.Entities;
using WebApplication1.Domain.ValueObjects;

namespace WebApplication1.Domain.Repositories
{
    public interface IBreweryRepository
    {
        Task<IEnumerable<Brewery>> GetAllAsync();
        ValueTask<IEnumerable<Brewery>> GetFilteredAsync(BreweryQueryParameters queryParameters);
    }
}
