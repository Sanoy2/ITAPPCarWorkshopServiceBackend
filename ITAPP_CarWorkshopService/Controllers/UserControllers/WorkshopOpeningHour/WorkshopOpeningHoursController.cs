using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopOpeningHour
{
    public class WorkshopOpeningHoursController : ApiController
    {
        [HttpGet]
        public Workshop_Opening_Hours Get_oppenting_houers(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Opening_Hours.FirstOrDefault(p => p.Workshop_ID == ID);
            }
        }
        [HttpPost]
        public Response_String Add_opening_hours([FromBody] Workshop_Opening_Hours New_Opening_Hours)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Opening_Hours.FirstOrDefault(p => p.Workshop_ID == New_Opening_Hours.Workshop_ID);
                if (Old != null)
                {
                    return new Response_String() { Response = "This Workshop have opening hours" };
                }
                db.Workshop_Opening_Hours.Add(New_Opening_Hours);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added" };
            }
        }
        [HttpPut]
        public Response_String Modifi_oppening_hours([FromBody] Workshop_Opening_Hours Modifi_hours)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Opening_Hours.FirstOrDefault(p => p.Workshop_ID == Modifi_hours.Workshop_ID);
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
        [HttpDelete]
        public Response_String Delete_oppening_hours(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Opening_Hours.FirstOrDefault(p => p.Workshop_ID == ID);
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
