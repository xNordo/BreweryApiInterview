namespace BreweryApiInterview.Domain.Entities
{
    public class Brewery
    { 
        public string Id { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string BreweryType { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public double? Longitude { get; private set; }
        public double? Latitude { get; private set; }
        public string WebsiteUrl { get; private set; } = string.Empty;

        public static Brewery Create(
            string id, 
            string name, 
            string city, 
            string phoneNumber, 
            string breweryType = "",
            string street = "",
            string state = "",
            string postalCode = "",
            string country = "",
            double? longitude = null,
            double? latitude = null,
            string websiteUrl = "")
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Brewery ID cannot be empty", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Brewery name cannot be empty", nameof(name));

            return new Brewery
            {
                Id = id,
                Name = name,
                City = city,
                PhoneNumber = phoneNumber,
                BreweryType = breweryType,
                Street = street,
                State = state,
                PostalCode = postalCode,
                Country = country,
                Longitude = longitude,
                Latitude = latitude,
                WebsiteUrl = websiteUrl
            };
        }
    }
}
