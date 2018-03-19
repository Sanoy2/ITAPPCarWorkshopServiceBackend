using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.Car.CarService
{
    public class CarSerivceController : ApiController
    {
        [HttpGet]
        public List<Car_Services> Get_Cars_Service()
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Services.ToList();
            }
        }
        [HttpGet]
        public Car_Services Get_Car_Service(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Services.FirstOrDefault(client => client.Car_ID == ID);
            }
        }
        [HttpPost]
        public Response_String Add_Car_Service([FromBody] Car_Services New_Car_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var CarExists = db.Car_Services.FirstOrDefault(car => car.Service_ID == New_Car_Profile.Service_ID);
                if (CarExists != null)
                {
                    return new Response_String() { Response = "Car service already exists" };
                }
                db.Car_Services.Add(CarExists);
                db.SaveChangesAsync();
                return new Response_String() { Response = "Car service was added to database" };
            }
        }
        [HttpPut]
        public Response_String Change_Car_Service([FromBody] Car_Services Modifi_car)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var OldCar = db.Car_Services.FirstOrDefault(car => car.Workshop_ID == Modifi_car.Workshop_ID && car.Service_ID == car.Service_ID && car.Workshop_ID == Modifi_car.Workshop_ID);
                if (OldCar != null)
                {
                    var ID = OldCar.Service_ID;
                    OldCar = Modifi_car;
                    OldCar.Service_ID = ID;
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Client added to a database" };
                }
                return new Response_String() { Response = "Client was not found in database" };
            }
        }
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
                    return new Response_String() { Response = "Client has been deleted" };
                }
                return new Response_String() { Response = "Client was not found" };
            }
        }
    }
}
