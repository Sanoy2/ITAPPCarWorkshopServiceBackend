using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopProfiles
{
    public class WorkshopProfilesController : ApiController
    {
        [HttpGet]
        public Workshop_Profiles Get_Workshop_by_ID_or_NIP([FromUri] int? ID = null)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                if (ID != null)
                    return db.Workshop_Profiles.FirstOrDefault(p => p.Workshop_ID == ID);
                return null;
            }
        }
        [HttpPost]
        public Response_String Add_New_Workshop([FromBody] Workshop_Profiles New_Workshop)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Profiles.FirstOrDefault(p => p.Workshop_NIP == New_Workshop.Workshop_NIP);
                if (Old != null)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                db.Workshop_Profiles.Add(New_Workshop);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added " };
            }
        }
        [HttpPut]
        public Response_String Modify_Workshop([FromBody] Workshop_Profiles Modify_Workshop_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var TheSameNIP = db.Workshop_Profiles.FirstOrDefault(p => p.Workshop_NIP == Modify_Workshop_Profile.Workshop_NIP);
                if(TheSameNIP != null)
                {
                    return new Response_String() { Response = "Workshop with this nip already exists" };
                }
                var Old = db.Workshop_Profiles.FirstOrDefault(p => p.Workshop_ID == Modify_Workshop_Profile.Workshop_ID);
                if (Old != null)
                {
                    var ID = Old.Workshop_ID;
                    Old = Modify_Workshop_Profile;
                    Old.Workshop_ID = ID;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item dose not exsists" };
            }
        }
        [HttpDelete]
        public Response_String Delete_Workshop(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Profiles.Remove(db.Workshop_Profiles.FirstOrDefault(p => p.Workshop_ID == ID));
                if(Old != null)
                {
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exists" };
            }
        }
    }
}
