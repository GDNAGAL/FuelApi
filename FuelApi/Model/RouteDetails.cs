using System.ComponentModel.DataAnnotations;

namespace FuelApi.Model
{
    public class RouteDetails
    {
        [Key] public string? RouteID { get; set; }
        public string? StartPoint { get; set; }
        public string? EndPoint { get; set; }
        public string? MidCity1 { get; set; }
        public string? MidCity2 { get; set; }
        public string? MidCity3 { get; set; }

    }
}
