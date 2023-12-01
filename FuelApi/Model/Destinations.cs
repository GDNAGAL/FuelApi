using System.ComponentModel.DataAnnotations;

namespace FuelApi.Model
{
    public class Destinations
    {
        [Key]
        public int DestinationID { get; set; }
        public string? DestinationName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

    }
}
