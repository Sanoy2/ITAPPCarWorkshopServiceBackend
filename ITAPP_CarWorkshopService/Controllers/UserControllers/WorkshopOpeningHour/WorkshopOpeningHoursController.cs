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
namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopOpeningHour
{
    /// <summary>
    /// Workshop Opening Hours Controller
    /// </summary>
    public class WorkshopOpeningHoursController : ApiController
    {
        /// <summary>
        /// GET method  &#xD;
        /// URL: http://itappcarworkshopservice.azurewebsites.net/api/workshopopeninghours/ + ID of Workshop &#xD;
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Opening hours of specyfic workshop</returns>
        [HttpGet]
        public Workshop_Opening_Hours Get_oppenting_houers(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Opening_Hours.FirstOrDefault(p => p.Workshop_ID == ID);
            }
        }
        /// <summary>
        /// POST method &#xD; URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopopeninghours &#xD;
        /// WOH_ID is automaticly incremented so leave it empty like WOH_ID: ""&#xD;
        /// </summary>
        /// <param name="New_Opening_Hours"> 
        /// {
        ///            Fri_end =, &#xD;
        ///            Fri_start = , &#xD;
        ///            Mon_end = , &#xD;
        ///            Mon_start = , &#xD;
        ///            Sat_end = , &#xD; 
        ///            Sat_start = , &#xD;
        ///            Sun_end = , &#xD; 
        ///            Sun_start = , &#xD;
        ///            Thu_end = , &#xD;
        ///            Thu_start = , &#xD;
        ///            Tue_end = , &#xD;
        ///            Tue_start = , &#xD;
        ///            Wed_end = , &#xD;
        ///            Wed_start =, &#xD;
        ///            Workshop_ID = , &#xD;      
        ///            WOH_ID= , &#xD;       
        /// }
        /// </param>
        /// 
        /// <returns> Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
        [HttpPost]
        public Response_String Add_opening_hours([FromBody] Workshop_Opening_Hours New_Opening_Hours)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Opening_Hours.FirstOrDefault(p => p.Workshop_ID == New_Opening_Hours.Workshop_ID);
                if (Old != null)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                db.Workshop_Opening_Hours.Add(New_Opening_Hours);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added" };
            }
        }
        /// <summary>
        /// PUT mehtod URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopopeninghours 
        /// in this method we need to pass in body WOH_ID becouse it will surch for the same ID and modifi it with changes in body
        /// </summary>
        /// <param name="Modifi_hours"> 
        /// {
        ///            Fri_end =, &#xD;
        ///            Fri_start = , &#xD;
        ///            Mon_end = , &#xD;
        ///            Mon_start = , &#xD;
        ///            Sat_end = , &#xD; 
        ///            Sat_start = , &#xD;
        ///            Sun_end = , &#xD; 
        ///            Sun_start = , &#xD;
        ///            Thu_end = , &#xD;
        ///            Thu_start = , &#xD;
        ///            Tue_end = , &#xD;
        ///            Tue_start = , &#xD;
        ///            Wed_end = , &#xD;
        ///            Wed_start =, &#xD;
        ///            Workshop_ID = , &#xD;      
        ///            WOH_ID= , &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" or "Item does not exsists"</returns>
        [HttpPut]
        public Response_String Modifi_oppening_hours([FromBody] Workshop_Opening_Hours Modifi_hours)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Opening_Hours.FirstOrDefault(p => p.WOH_ID == Modifi_hours.WOH_ID);
                if (Old != null)
                {
                    var id = Old.WOH_ID;
                    Old = Modifi_hours;
                    Old.WOH_ID = id;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
        /// <summary>
        /// DELETE mehod &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/workshopopeninghours/ + ID &#xD;
        /// </summary>
        /// <param name="ID">WOH_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
        [HttpDelete]
        public Response_String Delete_oppening_hours(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Opening_Hours.FirstOrDefault(p => p.WOH_ID == ID);
                if(Old != null)
                {
                    db.Workshop_Opening_Hours.Remove(Old);
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exists" };
            }
        }
    }
}
