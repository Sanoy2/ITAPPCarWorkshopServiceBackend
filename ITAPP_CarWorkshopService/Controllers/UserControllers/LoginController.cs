using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.Authorization;
using ITAPP_CarWorkshopService.ModelsManager;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public Response_String Login([FromBody] User user)
        {
            Response_String response = new Response_String();
            response.Response = UserManager.Login(user);
            return response;
        }
        
        [HttpPut]
        [AuthorizationFilter]
        [Route("api/login")]
        public Response_String ChangePassword(string oldPass, string newPass)
        { 
            int userId = Authorization.Token.GetUserIdFromRequestHeader(Request);

            UserManager.ChangePassword(userId, oldPass, newPass);

            var response = new Response_String();
            response.Response = "Probably changed";
            return response;
        }

        [HttpDelete]
        [AuthorizationFilter]
        [Route("api/login")]
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
