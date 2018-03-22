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
namespace ITAPP_CarWorkshopService.Controllers.UserControllers
{
    /// <summary>
    /// Cilent Profile Controller
    /// </summary>
    public class ClientProfileController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/clientproflie &#xD;
        /// </summary>
        /// <returns>Returning list of all clients or null if there are any clients</returns>
        [HttpGet]
        public List<Client_Profiles> Get_Clients_Profiles()
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Client_Profiles.ToList();
            }
        }
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/clientproflie/ + ID &#xD;
        /// </summary>
        /// <param name="ID">client_ID</param>
        /// <returns>Returning selected by ID client or null if there is not client with passed id</returns>
        [HttpGet]
        public Client_Profiles Get_Client_Profile(int ID)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Client_Profiles.FirstOrDefault(client => client.Client_ID == ID);
            }
        }
        /// <summary>
        /// POST method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/clientproflie &#xD;
        /// Client_ID is automaticly incremented so as a Clietn_ID = "" &#xD;
        /// </summary>
        /// <param name="New_Client_Profile">
        /// {
        ///        Cars_followed =, &#xD;
        ///        Client_ID =, &#xD;
        ///        Client_name =, &#xD;
        ///        Client_phone_number =, &#xD;
        ///        Client_surname =, &#xD;
        ///        User_ID =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
        [HttpPost]
        public Response_String Add_Client_Proflie([FromBody] Client_Profiles New_Client_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var ClientExists = db.Client_Profiles.FirstOrDefault(client => client.Client_name == New_Client_Profile.Client_name && client.Client_surname == New_Client_Profile.Client_surname && client.Client_phone_number == New_Client_Profile.Client_phone_number);
                if(ClientExists != null)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                db.Client_Profiles.Add(New_Client_Profile);
                db.SaveChangesAsync();
                return new Response_String() { Response = "Item was added" };
            }
        }
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/clientproflie &#xD;
        /// Client_ID is automaticly incremented so as a Clietn_ID = "" &#xD;
        /// </summary>
        /// <param name="Modifi_client">
        /// {
        ///        Cars_followed =, &#xD;
        ///        Client_ID =, &#xD;
        ///        Client_name =, &#xD;
        ///        Client_phone_number =, &#xD;
        ///        Client_surname =, &#xD;
        ///        User_ID =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify", "Item already exists" or "Item does not exsists" </returns>
        [HttpPut]
        public Response_String Change_Client_Profile([FromBody] Client_Profiles Modifi_client)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var ClientExists = db.Client_Profiles.FirstOrDefault(client => client.Client_name == Modifi_client.Client_name && client.Client_surname == Modifi_client.Client_surname && client.Client_phone_number == Modifi_client.Client_phone_number);
                if (ClientExists != null)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                var OldClient = db.Client_Profiles.FirstOrDefault(client => client.Client_ID == Modifi_client.Client_ID);
                if (OldClient != null)
                {
                    var ID = OldClient.Client_ID;
                    OldClient = Modifi_client;
                    OldClient.Client_ID = ID;
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/clientproflie/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Client_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists" </returns>
        [HttpDelete]
        public Response_String Delete_Client_Proflie(int ID)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Client = db.Client_Profiles.FirstOrDefault(client => client.Client_ID == ID);
                if(Client != null)
                {
                    db.Client_Profiles.Remove(Client);
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exsits" };
            }
        }
    }
}
