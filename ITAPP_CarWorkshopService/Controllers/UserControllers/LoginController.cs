using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.Authorization;

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
            catch(Exception e)
            {
                return new List<string>();
            }
        }

        [Route("api/login/token")]
        [HttpGet]
        public Response_String Token()
        {
            Response_String response = new Response_String();
            Token token = new Token();
            response.Response = token.TokenString;
            return response;
        }

        [HttpPost]
        public Response_String Login(string userEmail, string password)
        {
            Response_String response = new Response_String();
            response.Response = "hehe";
            return response;
        }
    }
}
