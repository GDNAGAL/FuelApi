using FuelApi.Model;
using Microsoft.EntityFrameworkCore;
using TransportWebApi.Model;

namespace FuelApi.Data
{
    public class FuelApiDbContext:DbContext
    {
        public FuelApiDbContext(DbContextOptions options):base(options) { }
        
        public DbSet<DriverDetails> DriverDetails { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Destinations> Destinations { get; set; }
        public DbSet<RouteDetails> RouteDetails { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Package> Package { get; set; }
    }
}
