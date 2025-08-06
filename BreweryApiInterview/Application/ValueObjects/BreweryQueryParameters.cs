using BreweryApiInterview.Application.Enums;

namespace BreweryApiInterview.Application.ValueObjects
{
    public class BreweryQueryParameters
    {
        public string? SearchTerm { get; private set; }

        public string? ByCity { get; private set; }
        public string? ByName { get; private set; }

        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }

        public SortBy? SortBy { get; private set; }
        public SortDirection SortDirection { get; private set; }

        public int Page { get; private set; } = 1;
        public int PerPage { get; private set; } = 20;
        
        private BreweryQueryParameters() { }

        public static BreweryQueryParameters Create(
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
            return new BreweryQueryParameters
            {
                SearchTerm = searchTerm,
                ByCity = byCity,
                ByName = byName,
                Latitude = latitude,
                Longitude = longitude,
                SortBy = sortBy,
                SortDirection = sortDirection,
                Page = Math.Max(1, page),
                PerPage = Math.Clamp(perPage, 1, 50)
            };
        }
    }
}
