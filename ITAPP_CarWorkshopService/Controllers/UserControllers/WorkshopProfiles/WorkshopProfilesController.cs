using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.ModelsManager;
using ITAPP_CarWorkshopService.AdditionalModels;

/// <summary>
/// Controller
/// </summary>
namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopProfiles
{
    /// <summary>
    /// Workshop Profile Controller
    /// </summary>
    public class WorkshopProfilesController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopprofiles/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Workshop_ID</param>
        /// <returns>Returns an Workshop selected by ID or if there is any return null</returns>
        [HttpGet]
        public Workshop_Profiles Get_Workshop_by_ID([FromUri] int ID)
        {
            return WorkshopProfileManager.GetWorkshopProfileById(ID);
        }

        [HttpGet]
        [Route("api/workshopcontroller/cities")]
        public List<CityAndZipCode> GetCities()
        {
            return WorkshopProfileManager.GetAllCitiesAndZipCodes();
        }

        /// <summary>
        /// POST method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopprofiles/ &#xD;
        /// Wokshop_ID is automaticly incremented so as a Wokshop_ID = "" &#xD;
        /// </summary>
        /// <param name="New_Workshop">
        /// {
        ///     Workshop_address_city =, &#xD;
        ///     Workshop_address_streer =, &#xD;
        ///     Workshop_address_zip_code =, &#xD;
        ///     Workshop_average_rating =, &#xD; 
        ///     Workshop_description =, &#xD;
        ///     Workshop_email_address =, &#xD;
        ///     Workshop_ID =, &#xD;
        ///     Workshop_logo_URL =, &#xD;
        ///     Workshop_NIP =, &#xD;
        ///     Workshop_phone_number =, &#xD;
        ///     Workshop_URL =, &#xD;   
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
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
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopprofiles/ &#xD;
        /// Workshop_ID should be passed by in body &#xD;
        /// </summary>
        /// <param name="Modify_Workshop_Profile">
        /// {
        ///     Workshop_address_city =, &#xD;
        ///     Workshop_address_streer =, &#xD;
        ///     Workshop_address_zip_code =, &#xD;
        ///     Workshop_average_rating =, &#xD; 
        ///     Workshop_description =, &#xD;
        ///     Workshop_email_address =, &#xD;
        ///     Workshop_ID =, &#xD;
        ///     Workshop_logo_URL =, &#xD;
        ///     Workshop_NIP =, &#xD;
        ///     Workshop_phone_number =, &#xD;
        ///     Workshop_URL =, &#xD;   
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify", "Item does not exsists" or "Item with already exists"</returns>
        [HttpPut]
        public Response_String Modify_Workshop([FromBody] Workshop_Profiles Modify_Workshop_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var TheSameNIP = db.Workshop_Profiles.FirstOrDefault(p => p.Workshop_NIP == Modify_Workshop_Profile.Workshop_NIP);
                if(TheSameNIP != null)
                {
                    return new Response_String() { Response = "Item with already exists" };
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
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopprofiles/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Workshop_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
        [HttpDelete]
        public Response_String Delete_Workshop(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Profiles.FirstOrDefault(p => p.Workshop_ID == ID);
                if(Old != null)
                {
                    db.Workshop_Profiles.Remove(Old);
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exists" };
            }
        }
    }
}
