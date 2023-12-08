using System.ComponentModel.DataAnnotations;

namespace FuelApi.Model
{
    public class RouteDetails
    {
        [Key] public string? RouteID { get; set; }
        public string? StartPoint { get; set; }
        public string? EndPoint { get; set; }
        public string? Distance { get; set; }
        public string? RouteName { get; set; }
        public int VehicleID { get; set; }
        public int DriverID { get; set; }

    }
}
