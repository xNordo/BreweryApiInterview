using System.Text.Json.Serialization;

namespace WebApplication1.Infrastructure.ExternalModels
{
    /// <summary>
    /// Model representing the OpenBreweryDB API response
    /// </summary>
    public class BreweryApiModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("brewery_type")]
        public string BreweryType { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        [JsonPropertyName("website_url")]
        public string WebsiteUrl { get; set; } = string.Empty;
    }
}
