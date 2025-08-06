using Microsoft.Extensions.Logging;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Repositories;
using WebApplication1.Domain.ValueObjects;

namespace WebApplication1.Application.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;
        private readonly ILogger<BreweryService> _logger;

        public BreweryService(IBreweryRepository breweryRepository, ILogger<BreweryService> logger)
        {
            _breweryRepository = breweryRepository ?? throw new ArgumentNullException(nameof(breweryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<IEnumerable<Brewery>> GetBreweriesAsync(BreweryQueryParameters queryParameters)
        {
            try
            {
                _logger.LogInformation("Getting breweries with query parameters");
                return await _breweryRepository.GetFilteredAsync(queryParameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting breweries");
                throw;
            }
        }
    }
}