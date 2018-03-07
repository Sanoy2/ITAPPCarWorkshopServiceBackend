using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ITAPP_CarWorkshopService.Controllers
{
    public class CarBrands2Controller : ApiController
    {


        // GET api/values/5
        public Car_Brands Get(int id)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Car_Brands.FirstOrDefault((p) => p.Brand_ID == id);
        }


    }
}
