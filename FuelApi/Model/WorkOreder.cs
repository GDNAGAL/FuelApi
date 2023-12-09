using System.ComponentModel.DataAnnotations;

namespace FuelApi.Model
{
    public class WorkOrder
    {
        [Key]
        public string? PackageId { get; set; }
        public DateTime Date { get; set; }
        public string? TypeOfPackage { get; set; }
        public string? Package { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public string? RouteId { get; set; }
        public int EstLoadingTime { get; set; }
        public DateTime DepatureTime { get; set; }
        public DateTime EstArrivalTime { get; set; }
        public int EstUnloadingTime { get; set; }
        public int DelayTime { get; set; }
    }
}
