using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.DataModels;
using ITAPP_CarWorkshopService.ModelsManager;
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

        [HttpGet]
        public List<DataModels.ClientProfile> Get_Clients_Profiles()
        {
            return ClientProfileManager.GetAllClientsProfiles();
        }

        [HttpGet]
        public List<DataModels.ClientProfile> GetClientProfileById(int ID)
        {
            return ClientProfileManager.GetClientProfileById(ID);
        }

        [HttpPost]
        public Response_String CreateClientProfile([FromBody] DataModels.ClientProfile NewClientProfileModel)
        {
            var respone = new Response_String();
            if(ClientProfileManager.PutNewClientProfileToDB(NewClientProfileModel))
            {
                respone.Response = "Client profile was succesfully added to DB.";
            }
            else
            {
                respone.Response = "There occured a problem while trying to add client profile to DB.";
            }

            return respone;
        }

        [HttpPut]
        public Response_String ModifyClientProfile([FromBody] DataModels.ClientProfile ClientProfileModel)
        {
            var respone = new Response_String();
            if (ClientProfileManager.ModifyExistingClientProfile(ClientProfileModel))
            {
                respone.Response = "Client profile was succesfully modified.";
            }
            else
            {
                respone.Response = "There occured a problem while trying to modify client profile.";
            }

            return respone;
        }

        [HttpDelete]
        public Response_String DeleteClientProfile(int ID)
        {
            var respone = new Response_String();
            if (ClientProfileManager.DeleteClientProfileFromDB(ID))
            {
                respone.Response = "Client profile was succesfully removed from DB.";
            }
            else
            {
                respone.Response = "There occured a problem while trying to remove client profile from DB.";
            }

            return respone;
        }
    }
}
