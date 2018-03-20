using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ITAPP_CarWorkshopService.Controllers
{
    public class exampleAuthorizationController : ApiController
    {
        [HttpGet]
        [Route("protected/exampleAuthorization")]
        public string GetPrivate()
        {
            return "It works as private!";
        }
    }
}
