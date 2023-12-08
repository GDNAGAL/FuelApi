using FuelApi.Data;
using FuelApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.Json.Nodes;
using TransportWebApi.Model;

namespace FuelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRecommendation : ControllerBase
    {
        private readonly MySqlConnection conn;
        private readonly FuelApiDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public GetRecommendation(FuelApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        [HttpPost]
        public async Task<JToken> GetRecommendations(string origin,string destination,Package package)
        {
            JToken json_route = new JObject();
            try
            {
                List<int> VID = CreatePackage(package);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&key=AIzaSyD7KtQoq29-5TqELLdPBSQoqCD376-qGjA&alternatives=true");
                request.Headers.Add("accept", "text/plain");
                var content = new StringContent("post", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var jsondata = await response.Content.ReadAsStringAsync();
                //var result = JsonConvert.DeserializeObject<T>(jsondata);
                
                JObject json = JObject.Parse(jsondata);
                json_route = json["routes"][0];
                RouteDetails order = new();
               
                //JObject json_routes = JObject.Parse(json["routes"].ToString());
                for (var i = 0; i < json["routes"].Count(); i++)
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("getBestDrivers", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader adapter = command.ExecuteReader();
                    int driverID = 0;
                    if (adapter.Read())
                    {
                        driverID = Convert.ToInt32(adapter["DriverID"]);
                    }
                    conn.Close();

                    order =new(){RouteID = Guid.NewGuid().ToString(), DriverID = driverID, RouteName = json["routes"][i]["summary"].ToString(), Distance = json["routes"][i]["legs"][0]["distance"]["text"].ToString(), StartPoint = origin, EndPoint = destination, VehicleID = VID[i] };
                    _dbContext.RouteDetails.Add(order);
                    _dbContext.SaveChanges();                    
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return json_route;
        }
        public List<int> CreatePackage(Package details)
        {
            Vehicle vehicle = new Vehicle();
            List<Vehicle> list = new List<Vehicle>();
            List<int> Vid = new List<int>();

            if (details != null)
            {
                _dbContext.Package.Add(details);
                _dbContext.SaveChanges();
                if (details.PackageType != null)
                {
                    vehicle.Capacity = details.Quantity;
                    vehicle.Unit = details.Unit;
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
                    list = GetVehicles(vehicle, 0);
                    var li = list.Select(x => x.LastMaintainance).ToList();
                    //List<DateTime> max=new List<DateTime>();
                    //for(int i=0;i<4;i++)
                    //{
                    //    if(li.Count<i)
                    //    {
                    //        max.Add(li.Max());
                    //        li.Remove(max[i]);
                    //    }
                    //}

                    //Vid = list.Where(o => o.LastMaintainance == max[0]).Select(x => x.VId).ToList();
                    Vid = list.Select(o=>o.VId).ToList();

                }
            }
            return Vid;
        }

        List<Vehicle> GetVehicles(Vehicle vehicle, int valplush)
        {
            var list = _dbContext.Vehicles.Where(x => x.VehicleType == vehicle.VehicleType && x.Unit == vehicle.Unit && x.Capacity == getCapacity(vehicle.Capacity) + valplush).ToList();
            if (list.Count == 0)
            {
                valplush += 500;
                if (valplush <= 2000)
                {
                    list = GetVehicles(vehicle, valplush);
                }
            }
            return list;
        }

        int getCapacity(int Capacity)
        {
            if (Capacity <= 500)
            {
                return 500;
            }
            else if (Capacity > 500 && Capacity <= 1000)
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
