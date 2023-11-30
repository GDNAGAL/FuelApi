using System.ComponentModel.DataAnnotations;

namespace FuelApi.Model
{
    public class VehicleDetails
    {
        [Key] public string? VehicleID { get; set; }
        [Required]
        public int VehicleType { get; set; }
        [Required]
        public int VehicleClass { get; set; }
        [Required]
        public string? ChassisNo { get; set; }
        public string? Color { get; set; }
        [Required]
        public string? RegistrationNo { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public int Age { get; set; }
        public int Capacity { get; set; }
        public string? CapacityUnit { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string? Status { get; set; }
        public byte isAllDocumentValid { get; set; }
        public string? FuelType { get; set; }
        public int Mileage { get; set; }
        public string? DriverId { get; set; }
        public string? Remark { get; set; }
    }
}
