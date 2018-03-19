using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.Car.CarProflie
{
    public class CarProfileController : ApiController
    {
        [HttpGet]
        public List<Car_Profiles> Get_Cars_Profiles()
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Profiles.ToList();
            }
        }
        [HttpGet]
        public Car_Profiles Get_Car_Profile(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Profiles.FirstOrDefault(client => client.Car_ID == ID);
            }
        }
        [HttpPost]
        public Response_String Add_Car_Proflie([FromBody] Car_Profiles New_Car_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var CarExists = db.Car_Profiles.FirstOrDefault(car =>  car.Car_VIN_number == New_Car_Profile.Car_VIN_number);
                if (CarExists != null)
                {
                    return new Response_String() { Response = "Car profile already exists" };
                }
                db.Car_Profiles.Add(CarExists);
                db.SaveChangesAsync();
                return new Response_String() { Response = "Car profile was added to database" };
            }
        }
        [HttpPut]
        public Response_String Change_Car_Profile([FromBody] Car_Profiles Modifi_car)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var OldCar = db.Car_Profiles.FirstOrDefault(car => car.Car_VIN_number == Modifi_car.Car_VIN_number);
                if (OldCar != null)
                {
                    var ID = OldCar.Car_ID;
                    OldCar = Modifi_car;
                    OldCar.Car_ID = ID;
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Client added to a database" };
                }
                return new Response_String() { Response = "Client was not found in database" };
            }
        }
        [HttpDelete]
        public Response_String Delete_Car_Proflie(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Car = db.Car_Profiles.FirstOrDefault(car => car.Car_ID == ID);
                if (Car != null)
                {
                    db.Car_Profiles.Remove(Car);
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Client has been deleted" };
                }
                return new Response_String() { Response = "Client was not found" };
            }
        }
    }
}
