using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

/// <summary>
/// Controller
/// </summary>
namespace ITAPP_CarWorkshopService.Controllers.Car.CarService
{
    /// <summary>
    /// Car Service Controller
    /// </summary>
    public class CarSerivceController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carservice &#xD;
        /// </summary>
        /// <returns>Returns list of car service or null if there is any</returns>
        [HttpGet]
        public List<Car_Services> Get_Cars_Service()
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Services.ToList();
            }
        }
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carservice/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Service_ID</param>
        /// <returns>Return selected service or null if there is no selected service</returns>
        [HttpGet]
        public Car_Services Get_Car_Service(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Services.FirstOrDefault(client => client.Service_ID == ID);
            }
        }
        /// <summary>
        /// POST method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carservice &#xD;
        /// Service_ID is automaticly incremented so as a Service_ID = "" &#xD;
        /// </summary>
        /// <param name="New_Car_Profile">
        /// {
        ///     Car_ID =, &#xD;
        ///     Mileage =, &#xD;
        ///     Service_date =, &#xD;
        ///     Service_description =, &#xD;
        ///     Service_ID =, &#xD;
        ///     Service_name =, &#xD;
        ///     Workshop_ID =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
        [HttpPost]
        public Response_String Add_Car_Service([FromBody] Car_Services New_Car_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var CarExists = db.Car_Services.FirstOrDefault(car => car.Service_ID == New_Car_Profile.Service_ID);
                if (CarExists != null)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                db.Car_Services.Add(CarExists);
                db.SaveChangesAsync();
                return new Response_String() { Response = "Item was added" };
            }
        }
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carservice &#xD;
        /// Service_ID should be passed by in body &#xD;
        /// </summary>
        /// <param name="Modifi_car">
        /// {
        ///     Car_ID =, &#xD;
        ///     Mileage =, &#xD;
        ///     Service_date =, &#xD;
        ///     Service_description =, &#xD;
        ///     Service_ID =, &#xD;
        ///     Service_name =, &#xD;
        ///     Workshop_ID =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" or "Item does not exsists"</returns>
        [HttpPut]
        public Response_String Change_Car_Service([FromBody] Car_Services Modifi_car)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var OldCar = db.Car_Services.FirstOrDefault(car => car.Service_ID == Modifi_car.Service_ID);
                if (OldCar != null)
                {
                    var ID = OldCar.Service_ID;
                    OldCar = Modifi_car;
                    OldCar.Service_ID = ID;
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carservice/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Service_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists" </returns>
        [HttpDelete]
        public Response_String Delete_Car_Service(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Car = db.Car_Services.FirstOrDefault(car => car.Service_ID == ID);
                if (Car != null)
                {
                    db.Car_Services.Remove(Car);
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
    }
}
