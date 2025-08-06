using System.Text;
using BreweryApiInterview.Application.Enums;
using BreweryApiInterview.Application.ValueObjects;

namespace BreweryApiInterview.Infrastructure.Services.BreweryApi
{
    public class BreweryApiUrlBuilder
    {
        private const string BaseApiUrl = "https://api.openbrewerydb.org/v1/breweries";
        private readonly StringBuilder _queryBuilder;
        private bool _hasParams;

        public BreweryApiUrlBuilder()
        {
            _queryBuilder = new StringBuilder(BaseApiUrl);
            _hasParams = false;
        }

        public BreweryApiUrlBuilder AddSearchTerm(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                AddParameter("by_name", searchTerm);
            }
            return this;
        }

        public BreweryApiUrlBuilder AddCityFilter(string? city)
        {
            if (!string.IsNullOrWhiteSpace(city))
            {
                AddParameter("by_city", city);
            }
            return this;
        }

        public BreweryApiUrlBuilder AddNameFilter(string? name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                AddParameter("by_name", name);
            }
            return this;
        }

        public BreweryApiUrlBuilder AddDistanceSort(double? latitude, double? longitude)
        {
            if (latitude.HasValue && longitude.HasValue)
            {
                string distParam = $"{latitude.Value},{longitude.Value}";
                AddParameter("by_dist", distParam);
                return this;
            }
            return this;
        }

        public BreweryApiUrlBuilder AddSorting(SortBy? sortBy, SortDirection sortDirection)
        {
            if (sortBy.HasValue)
            {
                string sortField = sortBy.Value switch
                {
                    SortBy.Name => "name",
                    SortBy.City => "city",
                    _ => "name"
                };

                var direction = sortDirection == SortDirection.Ascending ? "asc" : "desc";
                AddParameter("sort", $"{sortField}:{direction}");
            }
            return this;
        }

        public BreweryApiUrlBuilder AddPagination(int page, int perPage)
        {
            AddParameter("page", page.ToString());
            AddParameter("per_page", perPage.ToString());
            return this;
        }

        private void AddParameter(string key, string value)
        {
            if (_hasParams)
            {
                _queryBuilder.Append('&');
            }
            else
            {
                _queryBuilder.Append('?');
                _hasParams = true;
            }

            _queryBuilder.Append(key).Append('=').Append(Uri.EscapeDataString(value));
        }

        public static BreweryApiUrlBuilder FromQueryParameters(BreweryQueryParameters parameters)
        {
            var builder = new BreweryApiUrlBuilder()
                .AddSearchTerm(parameters.SearchTerm)
                .AddCityFilter(parameters.ByCity)
                .AddNameFilter(parameters.ByName);
            
            if (parameters.Latitude.HasValue && parameters.Longitude.HasValue) {
                builder.AddDistanceSort(parameters.Latitude, parameters.Longitude);
            } else if (parameters.SortBy.HasValue) {
                builder.AddSorting(parameters.SortBy, parameters.SortDirection);
            }

            return builder.AddPagination(parameters.Page, parameters.PerPage);
        }

        public override string ToString() => _queryBuilder.ToString();
    }
}
