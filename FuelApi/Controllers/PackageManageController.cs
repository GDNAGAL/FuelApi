using FuelApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportWebApi.Model;

namespace TransportWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageManageController : ControllerBase
    {
        private readonly FuelApiDbContext _dbContext;
        public PackageManageController(FuelApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        [Route("AddPackage")]
        public List<Vehicle> CreatePackage(Package details)
        {
            Vehicle vehicle = new Vehicle();
            List<Vehicle> list = new List<Vehicle>();
            if(details != null)
            {
                _dbContext.Package.Add(details);
                _dbContext.SaveChanges();
                if (details.PackageType != null)
                {
                    vehicle.Capacity = details.Quantity;
                    vehicle.Unit=details.Unit;
                    if (details.PackageType.ToUpper() == "SOLID")
                    {
                        vehicle.VehicleType = "LOADBODY";
                    }
                    else if (details.PackageType.ToUpper() == "LIQUID")
                    {
                        vehicle.VehicleType = "TANKER";
                    }
                    else if (details.PackageType.ToUpper() == "GAS")
                    {
                        vehicle.VehicleType = "GAS TANKER";
                    }
                    list = GetVehicles(vehicle,0);
                    var li = list.Select(x=>x.LastMaintainance).ToList();
                    var max1 = li.Max();
                    li.Remove(max1);
                    var max2 = li.Max();
                    li.Remove(max2);
                    var max3 = li.Max();
                    list = list.Where(o => o.LastMaintainance == max1 || o.LastMaintainance == max2 || o.LastMaintainance == max3).ToList();/*.Select(x => x.VId).ToList();*/

                }
            }
            return list;
        }

        List<Vehicle> GetVehicles(Vehicle vehicle,int valplush)
        {            
            var list= _dbContext.Vehicles.Where(x => x.VehicleType == vehicle.VehicleType && x.Unit == vehicle.Unit && x.Capacity == getCapacity(vehicle.Capacity) + valplush).ToList();
            if (list.Count==0)
            {
                valplush += 500;
                if(valplush <= 2000)
                {
                    list=GetVehicles(vehicle,valplush);
                }
            }
            return list;
        }

        int getCapacity(int Capacity)
        {
            if(Capacity<=500)
            {
                return 500;
            }
            else if(Capacity>500 && Capacity<=1000)
            {
                return 1000;
            }
            else if (Capacity > 1000 && Capacity <= 1500)
            {
                return 1500;
            }
            else if (Capacity > 1500 && Capacity <= 2000)
            {
                return 2000;
            }
            return 0;
        }
    }
}
