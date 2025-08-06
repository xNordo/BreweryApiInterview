namespace WebApplication1.API.DTOs
{
    public class BreweryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public double? Distance { get; set; }
    }
}