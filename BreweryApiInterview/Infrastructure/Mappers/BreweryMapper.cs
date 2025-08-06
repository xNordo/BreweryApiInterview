using BreweryApiInterview.Domain.Entities;
using BreweryApiInterview.Infrastructure.ExternalModels;

namespace BreweryApiInterview.Infrastructure.Mappers
{
    public static class BreweryMapper
    {
        public static Brewery ToDomainEntity(BreweryApiModel apiBrewery)
        {
            return Brewery.Create(
                id: apiBrewery.Id,
                name: apiBrewery.Name,
                city: apiBrewery.City,
                phoneNumber: apiBrewery.Phone,
                breweryType: apiBrewery.BreweryType,
                street: apiBrewery.Street,
                state: apiBrewery.State,
                postalCode: apiBrewery.PostalCode,
                country: apiBrewery.Country,
                longitude: apiBrewery.Longitude,
                latitude: apiBrewery.Latitude,
                websiteUrl: apiBrewery.WebsiteUrl
            );
        }
        
        public static IEnumerable<Brewery> ToDomainEntities(IEnumerable<BreweryApiModel> apiBreweries)
        {
            return apiBreweries.Select(ToDomainEntity);
        }
    }
}
