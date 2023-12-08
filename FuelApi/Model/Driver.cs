using System.ComponentModel.DataAnnotations;

namespace TransportWebApi.Model
{
    public class Driver
    {
        [Key]
        public int DriverID { get; set; }
        public int YearOfExp { get; set; }
        public int Age { get; set; }
        public string? LicenseClass { get; set; }
    }
}
