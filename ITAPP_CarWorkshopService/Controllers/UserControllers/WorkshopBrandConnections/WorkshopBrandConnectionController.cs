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
namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopBrandConnections
{
    /// <summary>
    /// Workshop Brand Connection Controller
    /// </summary>
    public class WorkshopBrandConnectionController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopbrandconnection/listofworkshopbrands/ + ID &#xD;
        /// </summary>
        /// <param name="ID"> Workshop_ID</param>
        /// <returns> Returns all brands connected to specyfic workshop or null if there is any</returns>
        [HttpGet]
        [Route("workshopbrandconnection/listofworkshopbrands/{ID}")]
        public IEnumerable<Workshop_Brand_Connections> Get_Workshop_Brand_Connection(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Brand_Connections.Where(p => p.Workshop_ID == ID);
            }
        }
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopbrandconnection/ + ID &#xD;
        /// </summary>
        /// <param name="ID">WBC_ID</param>
        /// <returns> Returns brand to specyfic by id or null if there is any</returns>
        [HttpGet]
        public Workshop_Brand_Connections Get_workshop_brand_by_ID(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Brand_Connections.FirstOrDefault(p => p.WBC_ID == ID);
            }
        }
        /// <summary>
        /// POST method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopbrandconnection &#xD;
        /// Car_brand_ID is automaticly incremented so leave it empty like Car_brand_ID: "" &#xD;
        /// </summary>
        /// <param name="NewWorkshopBrand">         
        /// {
        ///     Car_brand_ID =, &#xD;
        ///     WBC_ID =, &#xD;
        ///     Workshop_ID  =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
        [HttpPost]
        public Response_String Add_Workshop_Brand([FromBody] Workshop_Brand_Connections NewWorkshopBrand) 
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Exists = db.Workshop_Brand_Connections.FirstOrDefault(p => p.Car_brand_ID == NewWorkshopBrand.Car_brand_ID);
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
        /// PUT method  &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopbrandconnection  &#xD;
        /// in this method we need to pass in body WBC_ID becouse it will surch for the same ID and modifi it with changes in body  &#xD;
        /// </summary>
        /// <param name="ModifyWorkshopBrand">        
        /// {
        ///     Car_brand_ID =, &#xD;
        ///     WBC_ID =, &#xD;
        ///     Workshop_ID  =, &#xD;
        /// }</param>
        /// <returns></returns>
        [HttpPut]
        public Response_String Modify_Workshop_Brand([FromBody] Workshop_Brand_Connections ModifyWorkshopBrand)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Exists = db.Workshop_Brand_Connections.FirstOrDefault(p => p.WBC_ID == ModifyWorkshopBrand.WBC_ID);
                if (Exists != null)
                {
                    Exists = ModifyWorkshopBrand;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item dose not exists" };
            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopbrandconnection &#xD;
        /// </summary>
        /// <param name="ID">Car_brand_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists</returns>
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
