using WebApplication1.Application.Enums;
using WebApplication1.Application.ValueObjects;

namespace WebApplication1.Application.Mappers
{
    public static class QueryParametersMapper
    {
        public static BreweryQueryParameters ToBreweryQueryParameters(
            string? searchTerm = null,
            string? byCity = null,
            string? byState = null,
            string? byName = null,
            string? byType = null,
            string? byPostal = null,
            string? byCountry = null,
            SortBy? sortBy = null,
            SortDirection sortDirection = SortDirection.Ascending)
        {
            return BreweryQueryParameters.Create(
                searchTerm: searchTerm,
                byCity: byCity,
                byState: byState,
                byName: byName,
                byType: byType,
                byPostal: byPostal,
                byCountry: byCountry,
                sortBy: sortBy,
                sortDirection: sortDirection
            );
        }
    }
}
