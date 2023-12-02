using FuelApi.Data;
using FuelApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteDetailsManage : ControllerBase
    {
        private readonly FuelApiDbContext _dbContext;

        public RouteDetailsManage(FuelApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllRouteDetails")]
        public List<RouteDetails> GetAllWorkOrderDetails()
        {
            return _dbContext.RouteDetails.ToList();
        }
        [HttpPost]
        [Route("AddNewRoute")]
        public string AddRoute(RouteDetails oreder)
        {
            if (oreder != null)
            {
                oreder.RouteID = Guid.NewGuid().ToString();
                _dbContext.RouteDetails.Add(oreder);
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
        public string UpdateRoute(RouteDetails route)
        {
            if (route != null)
            {
                _dbContext.RouteDetails.Update(route);
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

        [HttpGet]
        [Route("FindRoute")]
        public Object GetRouteDetails(string? RouteId)
        {
            if (RouteId != null && RouteId != "")
            {
                Object obj = _dbContext.RouteDetails.Find(RouteId);
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
        [Route("GetRoutesByStartAndEndPoints")]
        public List<RouteDetails> GetRoutesByStartAndEndPoints(string? StartPoint,string? EndPoint)
        {
            List<RouteDetails> routeDetails = new List<RouteDetails>();
            if(StartPoint!=null && EndPoint != null)
            {
                routeDetails= _dbContext.RouteDetails.Where(x=>x.EndPoint==EndPoint && x.StartPoint==StartPoint).ToList();
                return routeDetails;
            }
            return routeDetails;
        }
    }
}
