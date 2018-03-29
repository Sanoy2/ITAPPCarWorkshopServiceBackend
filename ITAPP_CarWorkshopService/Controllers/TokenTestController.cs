using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.Authorization;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers
{
    public class TokenTestController : ApiController
    {
        
        [HttpPost]
        [Route("api/TokenTest")]
        public string Post([FromBody] int id)
        {
            Token token = new Token(id);
            token.RegisterToken();
            return token.TokenString;
        }
        
        [HttpGet]
        [AuthorizationFilter]
        [Route("api/TokenTest")]
        public Response_String Get()
        {
            int userId = Authorization.Token.GetUserIdFromRequestHeader(Request);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.AppendLine(" Looks like you have a good token. ");
            builder.AppendLine(" Id '-999' is Admin token. ");
            builder.AppendLine(" User ID encrypted inside the token: " + userId);

            var response = new Response_String();
            response.Response = builder.ToString();
            return response;
        }
        
        [HttpGet]
        [Route("api/TokenTest/all")]
        public List<Token> GetAll()
        {
            return Token.GetAllForAdminOnly();
        }
    }
}
