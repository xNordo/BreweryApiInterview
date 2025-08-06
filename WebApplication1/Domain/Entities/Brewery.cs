namespace WebApplication1.Domain.Entities
{
    public class Brewery
    {
        // Private setters for encapsulation
        public string Id { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public double? Distance { get; private set; }

        // Factory method for creating a Brewery from primitive types
        public static Brewery Create(string id, string name, string city, string phoneNumber, double? distance = null)
        {
            // Here you could add domain validation rules
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
                Distance = distance
            };
        }

        // Methods to update properties with domain validation
        public void UpdateDistance(double? distance)
        {
            Distance = distance;
        }
    }
}
