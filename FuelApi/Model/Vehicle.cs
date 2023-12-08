using System.ComponentModel.DataAnnotations;

namespace TransportWebApi.Model
{
    public class Vehicle
    {
        [Key] public int VId { get; set; }
        public string? VehicleType { get; set; }
        public int VTypeID { get; set; }
        public int Capacity { get; set; }
        public string? Unit { get; set; }
        public DateTime LastMaintainance { get; set; }
    }
}
