using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.Authorization;

namespace ITAPP_CarWorkshopService.Controllers
{
    [Route("api2/tokentest")]
    public class TokenTestController : ApiController
    {
        /*
        [HttpPost]
        [Route("api/TokenTest")]
        public string Post([FromBody] int id)
        {
            Token token = new Token(id);
            token.RegisterToken();
            return token.TokenString;
        }
        */
        [HttpGet]
        //[Route("api2/TokenTest")]
        public string Get()
        {
            return "Looks like you have a good token";
        }
        /*
        [HttpGet]
        [Route("api/TokenTest")]
        public List<Token> GetAll()
        {
            return Token.GetAllForAdminOnly();
        }
        */
    }
}
