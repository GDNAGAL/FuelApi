namespace FuelApi.Model
{
    public class NewWorkOrder
    {
        public string? Route { get; set; }
        public string? Distance { get; set; }
        public int VID { get; set; }
        public int DriverID { get; set; }
        public string? DriverName { get; set; }
        public string? VehicleName { get; set; }
    }
}
