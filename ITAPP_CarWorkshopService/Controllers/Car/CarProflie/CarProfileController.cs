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
namespace ITAPP_CarWorkshopService.Controllers.Car.CarProflie
{
    /// <summary>
    /// Car Profile Controller
    /// </summary>
    public class CarProfileController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carprofile &#xD;
        /// </summary>
        /// <returns>Return List of all car profiles or if there is any returns null</returns>
        [HttpGet]
        public List<Car_Profiles> Get_Cars_Profiles()
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Profiles.ToList();
            }
        }
        /// <summary>
        /// GET method  &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carprofile/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Car_ID</param>
        /// <returns>Return specyfic car with selected id or if there is any null</returns>
        [HttpGet]
        public Car_Profiles Get_Car_Profile(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Car_Profiles.FirstOrDefault(client => client.Car_ID == ID);
            }
        }
        /// <summary>
        /// POST method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carprofile &#xD;
        /// Car_ID is automaticly incremented so as a Car_ID = "" &#xD;
        /// </summary>
        /// <param name="New_Car_Profile">
        /// {
        ///   Brand_ID =, &#xD;
        ///   Car_first_registration_year =, &#xD;
        ///   Car_ID =, &#xD;
        ///   Car_model =, &#xD;
        ///   Car_production_year =, &#xD;
        ///   Car_VIN_number =,  &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
        [HttpPost]
        public Response_String Add_Car_Proflie([FromBody] Car_Profiles New_Car_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var CarExists = db.Car_Profiles.FirstOrDefault(car =>  car.Car_VIN_number == New_Car_Profile.Car_VIN_number);
                if (CarExists != null)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                db.Car_Profiles.Add(CarExists);
                db.SaveChangesAsync();
                return new Response_String() { Response = "Item was added" };
            }
        }
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carprofile &#xD;
        /// </summary>
        /// in this method we need to pass in body Car_ID becouse it will surch for the same ID and modifi it with changes in body
        /// <param name="Modifi_car">
        /// {
        ///   Brand_ID =, &#xD;
        ///   Car_first_registration_year =, &#xD;
        ///   Car_ID =, &#xD;
        ///   Car_model =, &#xD;
        ///   Car_production_year =, &#xD;
        ///   Car_VIN_number =,  &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" or "Item does not exsists"</returns>
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
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/carprofile/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Car_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
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
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
    }
}
