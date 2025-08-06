using WebApplication1.API.DTOs;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Enums;
using WebApplication1.Domain.ValueObjects;

namespace WebApplication1.API.Mappers
{
    public static class BreweryMapper
    {
        public static BreweryDto ToDto(Brewery brewery)
        {
            return new BreweryDto
            {
                Id = brewery.Id,
                Name = brewery.Name,
                City = brewery.City,
                PhoneNumber = brewery.PhoneNumber,
                Distance = brewery.Distance
            };
        }

        public static BreweryQueryParameters ToQueryParameters(
            string? search = null,
            SortBy? sortBy = null,
            SortDirection sortDirection = SortDirection.Ascending)
        {
            return BreweryQueryParameters.Create(search, sortBy, sortDirection);
        }
    }
}