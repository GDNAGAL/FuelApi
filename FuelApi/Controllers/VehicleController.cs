using FuelApi.Data;
using FuelApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace FuelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly MySqlConnection conn;
        private readonly IConfiguration configuration;
        private readonly FuelApiDbContext _dbContext;

        public VehicleController(FuelApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            this.configuration = configuration;
            conn = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));

        }

        [HttpGet]
        [Route("GetAllVehicleDetails")]
        public List<VehicleDetails> GetAllVehicleDetails()
        {
            //conn.Open();
            //MySqlCommand cmd = new MySqlCommand("select * from VehicleDetails", conn);
            //MySqlDataAdapter reader =cmd.ExecuteReader();
            //conn.Close();
            return _dbContext.VehicleDetails.ToList();
        }

        [HttpPost]
        [Route("AddVehile")]
        public string AddVehicle(VehicleDetails vehicle)
        {
            if (vehicle != null)
            {
                vehicle.VehicleID = Guid.NewGuid().ToString();
                _dbContext.VehicleDetails.Add(vehicle);
                var result = _dbContext.SaveChanges();
                if (result == 0)
                {
                    return "Data not Add";
                }
                else
                {
                    return "Data Added";
                }
            }
            return "Please Enter Details";
        }

        [HttpPost]
        [Route("UpdateDetails")]
        public string UpdateVehicle(VehicleDetails vehicle)
        {
            if (vehicle != null)
            {
                _dbContext.VehicleDetails.Update(vehicle);
                var result = _dbContext.SaveChanges();
                if (0 == result)
                {
                    return "Data not Updated";
                }
                else
                {
                    return "Data Updated";
                }
            }
            return "Please Enter Valid Details";
        }

        [HttpGet(Name = "FindVehicleDetails")]
        public Object GetVehicleDetails(string? VehicleId)
        {
            if (VehicleId != null && VehicleId != "")
            {
                Object obj = _dbContext.VehicleDetails.Find(VehicleId);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return Ok("Not Found");
                }
            }
            return Ok("invalid Id");
        }


        [HttpGet]
        [Route("FindVehicles")]
        public Object GetVehicles(int VehicleId)
        {
            if (VehicleId >0)
            {
                Object obj = _dbContext.Vehicles.Find(VehicleId);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return Ok("Not Found");
                }
            }
            return Ok("invalid Id");
        }

        [HttpPost]
        [Route("DeleteVehicle")]
        public string DeleteVehicle(string? VehicleId)
        {
            if (VehicleId != null)
            {
                VehicleDetails vehicle = _dbContext.VehicleDetails.Find(VehicleId);
                if (vehicle != null)
                {
                    _dbContext.VehicleDetails.Remove(vehicle);
                    var result = _dbContext.SaveChanges();
                    if (result == 0)
                    {
                        return "Data Not Delete";
                    }
                    else
                    {
                        return "Data Deleted";
                    }
                }
                else
                {
                    return "Data Not Found";
                }
            }
            return "Please Enter DriverId";
        }
    }
}
