using System.ComponentModel.DataAnnotations;

namespace TransportWebApi.Model
{
    public class Package
    {
        [Key] public int PackageId { get; set; }
        public string? PackageType { get; set; }
        public string? Packages { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }

    }
}
