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
        public List<string> Get_All_Of_Brands()
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

        [HttpPost]
        public string Add_New_Brand([FromBody] Car_Brands New_Brand)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            foreach (var Car in db.Car_Brands)
            {
                if (New_Brand.Brand_Name.ToLower() == Car.Brand_Name.ToLower())
                {
                    return "Car is exsisting";
                }
            }
            var newBrand = new Car_Brands()
            {
                Brand_Name = New_Brand.Brand_Name.ToLower()
            };
            newBrand.Brand_Name = newBrand.Brand_Name.Replace(newBrand.Brand_Name[0], newBrand.Brand_Name[0].ToString().ToUpper().ToCharArray()[0]);
            db.Car_Brands.Add(newBrand);
            db.SaveChanges();
            return "Car was added";
        }
    }
}
