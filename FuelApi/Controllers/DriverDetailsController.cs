using FuelApi.Data;
using FuelApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Xml;

namespace FuelApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DriverDetailsController : ControllerBase
    {
        private readonly MySqlConnection conn;
        private readonly IConfiguration _configuration;
        private readonly FuelApiDbContext _dbContext;
        public DriverDetailsController(FuelApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        [Route("AddDriver")]
        public string AddDriver(DriverDetails driver)
        {
            if (driver != null)
            {
                driver.DriverID=Guid.NewGuid().ToString();
                _dbContext.DriverDetails.Add(driver);
                var result=_dbContext.SaveChanges();
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
        public string UpdateDriver(DriverDetails driver)
        {
            if (driver != null)
            {
                _dbContext.DriverDetails.Update(driver);
                var result=_dbContext.SaveChanges();
                if(0 == result)
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

        [HttpGet(Name ="FindDriverDetails")]
        public Object GetDriverDetails(string? DriverId)
        {
            if(DriverId!=null && DriverId != "")
            {
                Object obj= _dbContext.DriverDetails.Find(DriverId);
                if(obj!=null)
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
        [Route("FindDrivers")]
        public Object GetDriver(int DriverId)
        {
            if (DriverId > 0 )
            {
                Object obj = _dbContext.Drivers.Find(DriverId);
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
        [Route("DeleteDriver")]
        public string DeleteDriver(string? DriverId)
        {
            if (DriverId != null)
            {
                DriverDetails driver = _dbContext.DriverDetails.Find(DriverId);
                if (driver!=null)
                {
                    _dbContext.DriverDetails.Remove(driver);
                    var result=_dbContext.SaveChanges();
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
        [HttpGet]
        [Route("GetAllDrivers")]
        public string GetAllDrivers()
        {
            DataTable DriverDataTable = new DataTable();

            MySqlCommand command = new MySqlCommand("get_driver_details", conn);
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add(new MySqlParameter("PStudentId", TextBox1.Text));
            conn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(DriverDataTable);
            conn.Close();
            string jsonResult = JsonConvert.SerializeObject(DriverDataTable, Newtonsoft.Json.Formatting.Indented);
            return jsonResult;
        }

        [HttpPost]
        [Route("SearchDriver")]
        public string SearchDriver(string? search)
        {
            DataTable DriverDataTable = new DataTable();

            MySqlCommand command = new MySqlCommand("search_in_DriverList", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new MySqlParameter("search", search));
            conn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(DriverDataTable);
            conn.Close();
            string jsonResult = JsonConvert.SerializeObject(DriverDataTable, Newtonsoft.Json.Formatting.Indented);
            return jsonResult;
        }
    }
}
