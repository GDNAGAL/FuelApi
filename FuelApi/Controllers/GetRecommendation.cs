using FuelApi.Data;
using FuelApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace FuelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRecommendation : ControllerBase
    {
        private readonly FuelApiDbContext _dbContext;

        public GetRecommendation(FuelApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<JToken> GetRecommendations(string origin,string destination)
        {
            JToken json_route = new JObject();
            try
            {
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
                    order=new(){RouteID = Guid.NewGuid().ToString(), RouteName = json["routes"][i]["summary"].ToString(), Distance = json["routes"][i]["legs"][0]["distance"]["text"].ToString(),StartPoint=origin,EndPoint=destination };
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
    }
}
