using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopBrandConnections
{
    public class WorkshopBrandConnectionController : ApiController
    {
        /// <summary>
        /// Function find's Workshop Connection Brand according to delivered workshop ID. 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> Returns all brands connected to specyfic workshop </returns>
        [HttpGet]
        public IEnumerable<Workshop_Brand_Connections> Get_Workshop_Brand_Connection(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Brand_Connections.Where(p => p.Workshop_ID == ID);
            }
        }
        /// <summary>
        /// Adding a connection to WorkshopBrandConnection data set.
        /// </summary>
        /// <param name="NewWorkshopBrand"></param>
        /// <returns>JSON { "Repsone":"Text" }</returns>
        [HttpPost]
        public Response_String Add_Workshop_Brand([FromBody] Workshop_Brand_Connections NewWorkshopBrand) 
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Exists = db.Workshop_Brand_Connections.FirstOrDefault(p => p.Car_brand_ID == NewWorkshopBrand.Car_brand_ID && NewWorkshopBrand.Workshop_ID == p.Workshop_ID);
                if( Exists != null)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                db.Workshop_Brand_Connections.Add(NewWorkshopBrand);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added " };
            }
        }
        /// <summary>
        /// Deleting WorkshopBrandConnection based on ID of WorkshopBrandConnection.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> JSON { "Repsone":"Text" }</returns>
        [HttpDelete]
        public Response_String Delete_Workshop_Brand(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Exists = db.Workshop_Brand_Connections.Remove(db.Workshop_Brand_Connections.FirstOrDefault(p => ID == p.WBC_ID));
                if(Exists != null)
                {
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item dose not exists" };
            }
        }
    }
}
