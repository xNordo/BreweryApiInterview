using WebApplication1.Domain.Entities;
using WebApplication1.Domain.ValueObjects;

namespace WebApplication1.Application.Services
{
    public interface IBreweryService
    {
        ValueTask<IEnumerable<Brewery>> GetBreweriesAsync(BreweryQueryParameters queryParameters);
    }
}