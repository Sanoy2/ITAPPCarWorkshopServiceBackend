using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers
{
    public class ClientProfileController : ApiController
    {
        [HttpGet]
        public List<Client_Profiles> Get_Clients_Profiles()
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Client_Profiles.ToList();
            }
        }
        [HttpGet]
        public Client_Profiles Get_Client_Profile(int ID)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Client_Profiles.FirstOrDefault(client => client.Client_ID == ID);
            }
        }
        [HttpPost]
        public Response_String Add_Client_Proflie([FromBody] Client_Profiles New_Client_Profile)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var ClientExists = db.Client_Profiles.FirstOrDefault(client => client.Client_name == New_Client_Profile.Client_name && client.Client_surname == New_Client_Profile.Client_surname && client.Client_phone_number == New_Client_Profile.Client_phone_number);
                if(ClientExists != null)
                {
                    return new Response_String() { Response = "Client already exists" };
                }
                db.Client_Profiles.Add(New_Client_Profile);
                db.SaveChangesAsync();
                return new Response_String() { Response = "Client was added to database" };
            }
        }
        [HttpPut]
        public Response_String Change_Client_Profile([FromBody] Client_Profiles Modifi_client)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var ClientExists = db.Client_Profiles.FirstOrDefault(client => client.Client_name == Modifi_client.Client_name && client.Client_surname == Modifi_client.Client_surname && client.Client_phone_number == Modifi_client.Client_phone_number);
                if (ClientExists != null)
                {
                    return new Response_String() { Response = "Client with that data already exists" };
                }
                var OldClient = db.Client_Profiles.FirstOrDefault(client => client.Client_ID == Modifi_client.Client_ID);
                if (OldClient != null)
                {
                    var ID = OldClient.Client_ID;
                    OldClient = Modifi_client;
                    OldClient.Client_ID = ID;
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Client added to a database" };
                }
                return new Response_String() { Response = "Client was not found in database" };
            }
        }
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
                    return new Response_String() { Response = "Client has been deleted" };
                }
                return new Response_String() { Response = "Client was not found" };
            }
        }
    }
}
