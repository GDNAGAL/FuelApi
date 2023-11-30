using FuelApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FuelApi.Data
{
    public class FuelApiDbContext:DbContext
    {
        public FuelApiDbContext(DbContextOptions options):base(options) { }
        
        public DbSet<DriverDetails> DriverDetails { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }
        public DbSet<WorkOreder> WorkOrders { get; set; }
    }
}
