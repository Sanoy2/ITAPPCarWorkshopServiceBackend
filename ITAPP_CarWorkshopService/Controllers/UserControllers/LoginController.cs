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
        [Route("api/login/header")]
        [HttpGet]
        public IEnumerable<string> Header()
        {
            try
            {
                IEnumerable<string> headerValues;
                headerValues = Request.Headers.GetValues("Token");
                return headerValues;
            }
            catch (Exception e)
            {
                var result = new List<string>();
                result.Add("Token is missing in header");
                return result;
            }
        }

        [Route("api/login/token")]
        [HttpGet]
        public List<string> Token()
        {
            var list = new List<string>();
            Token token = new Token();
            list.Add("Encrypted Token: " + token.TokenString);
            string decryptedToken = new Encryption().Decrypt(token.TokenString);
            list.Add("Decrypted Token: " + decryptedToken);
            var splitedString = decryptedToken.Split(':');
            list.Add("Splited String:");
            foreach (var item in splitedString)
            {
                list.Add(item);
            }
            list.Add("This should be user id: " + splitedString[1]);
            return list;
        }

        [HttpPost]
        [Route("api/login")]
        public Response_String Login([FromBody] User user)
        {
            Response_String response = new Response_String();
            response.Response = UserManager.Login(user.User_email, user.User_password);
            return response;
        }

        [HttpDelete]
        [AuthorizationFilter]
        [Route("api/login")]
        public void Logout()
        {
            IEnumerable<string> headerValues;
            headerValues = Request.Headers.GetValues("Token");
            string tokenString = headerValues.First();
            Authorization.Token.Logout(tokenString);
        }
    }
}
