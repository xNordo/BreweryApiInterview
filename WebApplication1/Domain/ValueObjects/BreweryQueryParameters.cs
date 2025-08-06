using WebApplication1.Domain.Enums;

namespace WebApplication1.Domain.ValueObjects
{
    public class BreweryQueryParameters
    {
        public string? SearchTerm { get; private set; }
        public SortBy? SortBy { get; private set; }
        public SortDirection SortDirection { get; private set; }

        private BreweryQueryParameters() { }

        public static BreweryQueryParameters Create(
            string? searchTerm = null,
            SortBy? sortBy = null,
            SortDirection sortDirection = SortDirection.Ascending)
        {
            return new BreweryQueryParameters
            {
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortDirection = sortDirection
            };
        }
    }
}
