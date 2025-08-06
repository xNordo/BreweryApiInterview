using BreweryApiInterview.API.DTOs;
using BreweryApiInterview.Domain.Entities;
using BreweryApiInterview.Application.Enums;
using BreweryApiInterview.Application.ValueObjects;

namespace BreweryApiInterview.API.Mappers
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
                BreweryType = brewery.BreweryType,
                Street = brewery.Street,
                State = brewery.State,
                PostalCode = brewery.PostalCode,
                Country = brewery.Country,
                Longitude = brewery.Longitude,
                Latitude = brewery.Latitude,
                WebsiteUrl = brewery.WebsiteUrl
            };
        }

        public static BreweryQueryParameters ToQueryParameters(
            string? searchTerm = null,
            string? byCity = null,
            string? byName = null,
            double? latitude = null,
            double? longitude = null,
            SortBy? sortBy = null,
            SortDirection sortDirection = SortDirection.Ascending,
            int page = 1,
            int perPage = 20)
        {
            return BreweryQueryParameters.Create(
                searchTerm: searchTerm,
                byCity: byCity,
                byName: byName,
                latitude: latitude,
                longitude: longitude,
                sortBy: sortBy,
                sortDirection: sortDirection,
                page: page,
                perPage: perPage
            );
        }
    }
}