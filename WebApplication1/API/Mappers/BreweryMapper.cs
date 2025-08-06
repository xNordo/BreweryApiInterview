using WebApplication1.API.DTOs;
using WebApplication1.Domain.Entities;

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
    }
}
