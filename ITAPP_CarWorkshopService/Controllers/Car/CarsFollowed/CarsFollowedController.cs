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
namespace ITAPP_CarWorkshopService.Controllers.Car.CarsFollowed
{
    /// <summary>
    /// Car Followed Controller 
    /// </summary>
    public class CarsFollowedController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carsfollowed/carsfollowedbyuser + ID &#xD;
        /// </summary>
        /// <param name="Client_ID"> Client_ID </param>
        /// <returns>Returns list of cars followed by user or null if there is any car followed by user</returns>
        [HttpGet]
        [Route ("carsfollowed/carsfollowedbyuser/{ID}")]
        public IEnumerable<Cars_followed> List_of_car_followed_by_User(int Client_ID)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Cars_followed.Where(car_followed => car_followed.Client_ID == Client_ID);
            }
        }
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carsfollowed + ID &#xD;
        /// </summary>
        /// <param name="Car_follow_ID">Car_follow_ID</param>
        /// <returns>Return car followed with specyfi id or null if there is any car countaining that id</returns>
        [HttpGet]
        public Cars_followed Car_followed_by_ID(int Car_follow_ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Cars_followed.FirstOrDefault(car_followed => car_followed.Car_follow_ID == Car_follow_ID);
            }
        }
        /// <summary>
        /// POST method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carsfollowed &#xD;
        /// Car_follow_ID is automaticly incremented so as a Car_follow_ID= ""
        /// </summary>
        /// <param name="New_Connection">        
        /// {
        ///     Car_follow_ID =, &#xD;
        ///     Car_profile_ID =, &#xD;
        ///     Client_ID =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
        [HttpPost]
        public Response_String Add_Car_Followed([FromBody]Cars_followed New_Connection)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            { 
                var OldConnection = db.Cars_followed.FirstOrDefault(p => p.Client_ID == New_Connection.Client_ID && p.Car_profile_ID == New_Connection.Car_profile_ID);
                if (OldConnection != null)
                {
                    return new Response_String() { Response = "Item already exisit" };
                }
                db.Cars_followed.Add(New_Connection);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added" };
            }
        }
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carsfollowed &#xD;
        /// Car_follow_ID should be passed by in body
        /// </summary>
        /// <param name="Modify_Connectio">
        /// {
        ///     Car_follow_ID =, &#xD;
        ///     Car_profile_ID =, &#xD;
        ///     Client_ID =, &#xD;
        /// }
        /// </param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" or "Item does not exsists"</returns>
        [HttpPut]
        public Response_String Modify_Car_Followed([FromBody] Cars_followed Modify_Connectio)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Cars_followed.FirstOrDefault(p => p.Car_follow_ID == Modify_Connectio.Car_follow_ID);
                if( Old != null)
                {
                    Old = Modify_Connectio;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item does not exists" };
            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carsfollowed/ + ID &#xD;
        /// </summary>
        /// <param name="Car_follow_ID"> Car_follow_ID </param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
        [HttpDelete]
        public Response_String Delete_Car_Followed(int Car_follow_ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var OldConnection = db.Cars_followed.FirstOrDefault(p => p.Client_ID == Car_follow_ID);
                if (OldConnection != null)
                {
                    db.Cars_followed.Remove(OldConnection);
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item dose not exist" };
            }
        }
    }
}
