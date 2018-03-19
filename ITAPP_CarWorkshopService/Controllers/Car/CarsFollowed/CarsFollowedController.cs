using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.Car.CarsFollowed
{
    public class CarsFollowedController : ApiController
    {
        [HttpGet]
        public IEnumerable<Cars_followed> List_of_car_followed_by_User(int Client_ID)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Cars_followed.Where(car_followed => car_followed.Client_ID == Client_ID);
            }
        }
        [HttpPost]
        public Response_String Add_Car_Followed([FromBody]Cars_followed New_Connection)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            { 
                var OldConnection = db.Cars_followed.FirstOrDefault(p => p.Client_ID == New_Connection.Client_ID && p.Car_profile_ID == New_Connection.Car_profile_ID);
                if (OldConnection != null)
                {
                    return new Response_String() { Response = "item already exisit" };
                }
                db.Cars_followed.Add(New_Connection);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added" };
            }
        }
        [HttpDelete]
        public Response_String Delete_Car_Followed(int Old_Connection)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var OldConnection = db.Cars_followed.Remove(db.Cars_followed.FirstOrDefault(p => p.Client_ID == Old_Connection));
                if (OldConnection != null)
                {   
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item dose not exist" };
            }
        }
    }
}
