﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ITAPP_CarWorkshopService.Controllers
{
    public class tempController : ApiController
    {
        [HttpGet]
        [Route("protected/temp")]
        public string Get()
        {
            return "It Works!";
        }
    }
}
