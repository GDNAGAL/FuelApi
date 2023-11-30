using FuelApi.Data;
using FuelApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly FuelApiDbContext _dbContext;

        public WorkOrderController(FuelApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllWorkOrderDetails")]
        public List<WorkOreder> GetAllWorkOrderDetails()
        {
            return _dbContext.WorkOrders.ToList();
        }

        [HttpPost]
        [Route("AddNewWorkOrder")]
        public string AddVehile(WorkOreder oreder)
        {
            if (oreder != null)
            {
                oreder.WorkOrderID = Guid.NewGuid().ToString();
                _dbContext.WorkOrders.Add(oreder);
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
        public string UpdateWorkOrder(WorkOreder oreder)
        {
            if (oreder != null)
            {
                _dbContext.WorkOrders.Update(oreder);
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

        [HttpGet(Name = "FindOrderDetails")]
        public Object GetVehicleDetails(string? WorkOrderId)
        {
            if (WorkOrderId != null && WorkOrderId != "")
            {
                Object obj = _dbContext.WorkOrders.Find(WorkOrderId);
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
        [Route("DeleteOrder")]
        public string DeleteOrder(string? WorkOrderId)
        {
            if (WorkOrderId != null)
            {
                VehicleDetails vehicle = _dbContext.VehicleDetails.Find(WorkOrderId);
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
