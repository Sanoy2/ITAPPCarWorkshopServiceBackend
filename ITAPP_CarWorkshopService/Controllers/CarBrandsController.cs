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

        [HttpPut]
        public IHttpActionResult Put(string Brand_Name)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var Car_Brand = db.Car_Brands.FirstOrDefault((p) => p.Brand_Name == Brand_Name);
            if(Car_Brand == null)
            {
                var newCarBrand = new Car_Brands() { Brand_ID = db.Car_Brands.Last().Brand_ID + 1 , Brand_Name = Brand_Name };
                db.Car_Brands.Add(newCarBrand);
                db.SaveChanges();
            }
            else
            {
                return BadRequest("This Car_Brand exsist ID: " + Car_Brand.Brand_ID + " Name: " + Car_Brand.Brand_Name);
            }
            return Ok();
        }

        [HttpPost]
        public string Post(string carBrand)
        {
            /* TODO:
             * 1) check if this function work
             * 2) move function to model class
             * */

            return "It Works!";
        }
    }
}
