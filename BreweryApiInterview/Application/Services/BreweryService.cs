using Microsoft.Extensions.Logging;
using BreweryApiInterview.Domain.Entities;
using BreweryApiInterview.Application.Repositories;
using BreweryApiInterview.Application.ValueObjects;

namespace BreweryApiInterview.Application.Services
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

        public async Task<IEnumerable<Brewery>> GetBreweriesAsync(BreweryQueryParameters queryParameters)
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