using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.Authorization;
using ITAPP_CarWorkshopService.ModelsManager;
using ITAPP_CarWorkshopService.DataModels;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public Response_String Login([FromBody] DataModels.UserModel user)
        {
            Response_String response = new Response_String();
            response.Response = UserManager.Login(user);
            return response;
        }
        
        [HttpDelete]
        [AuthorizationFilter]
        public Response_String Logout()
        {
            string tokenString = Authorization.Token.GetTokenStringFromRequestHeader(Request);
            Authorization.Token.Logout(tokenString);

            var response = new Response_String();
            response.Response = "Probably logged out and removed: " + tokenString;
            response.Response += " from token list.";
            return response;
        }
    }
}
