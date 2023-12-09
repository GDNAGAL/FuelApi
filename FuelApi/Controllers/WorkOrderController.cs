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
        public List<WorkOrder> GetAllWorkOrderDetails()
        {
            return _dbContext.WorkOrders.ToList();
        }

        [HttpPost]
        [Route("AddNewWorkOrder")]
        public string AddOrder(WorkOrder oreder)
        {
            if (oreder != null)
            {                
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
        public string UpdateWorkOrder(WorkOrder oreder)
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
                WorkOrder order = _dbContext.WorkOrders.Find(WorkOrderId);
                if (order != null)
                {
                    _dbContext.WorkOrders.Remove(order);
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
