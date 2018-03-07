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
        public List<Car_Brands> Get()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var List = new List<Car_Brands>();
            foreach (var car in db.Car_Brands)
            {
                List.Add(car);
            }
            return List;
        }


    }
}
