using FuelApi.Data;
using FuelApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationManageController : ControllerBase
    {
        private readonly FuelApiDbContext _dbContext;

        public DestinationManageController(FuelApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllDestinationDetails")]
        public List<Destinations> GetAllWorkOrderDetails()
        {
            return _dbContext.Destinations.ToList();
        }

        [HttpPost]
        [Route("AddNewDestination")]
        public string AddVehile(Destinations oreder)
        {
            if (oreder != null)
            {                
                _dbContext.Destinations.Add(oreder);
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
        public string UpdateDestination(Destinations oreder)
        {
            if (oreder != null)
            {
                _dbContext.Destinations.Update(oreder);
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
        [Route("FindDestinationsDetails")]
        public Object GetDestinationDetails(int DestinationsId)
        {
            if (DestinationsId != 0)
            {
                Object obj = _dbContext.Destinations.Find(DestinationsId);
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
    }
}
