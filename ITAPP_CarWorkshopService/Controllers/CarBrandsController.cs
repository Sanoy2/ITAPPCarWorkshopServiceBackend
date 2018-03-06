using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ITAPP_CarWorkshopService.Controllers
{
    public class CarBrandsController : ApiController
    {


        // GET api/values/5
        public List<string> Get()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            
            List<string> list = new List<string>();

            var carBrands = db.Car_Brands;

            foreach (var carBrand in carBrands)
            {
                list.Add(carBrand.Brand_Name);
            }

            return list;
        }


    }
}
