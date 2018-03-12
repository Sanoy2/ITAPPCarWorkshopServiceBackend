using ITAPP_CarWorkshopService.Helpfukl_classes;
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
        [HttpDelete]
        public IHttpActionResult Delete_Car_Brand(IntClass ID_Brand)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var car = db.Car_Brands.FirstOrDefault(p => p.Brand_ID == ID_Brand.tmp);
                if(car != null)
                {
                    db.Car_Brands.Remove(car);
                    db.SaveChanges();
                    return Ok("car removed");
                }
            }
            return Ok("There is no such car brand");
        }

        // GET api/values/5
        [HttpGet]
        public List<Car_Brands> Get_All_Of_Brands()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            
            List<Car_Brands> list = new List<Car_Brands>();

            var carBrands = db.Car_Brands;

            foreach (var carBrand in carBrands)
            {
                list.Add(carBrand);
            }
            return list;
        }

        [HttpPost]
        public IHttpActionResult Add_New_Brand([FromBody] Car_Brands New_Brand)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            foreach (var Car in db.Car_Brands)
            {
                if (New_Brand.Brand_Name.ToLower() == Car.Brand_Name.ToLower())
                {
                    return Ok($"Car is exisiting : {Car.Brand_ID} , {Car.Brand_Name} , {Car.Car_Profiles} , {Car.Workshop_Brand_Connections}");
                }
            }
            var newBrand = new Car_Brands()
            {
                Brand_Name = New_Brand.Brand_Name.ToLower()
            };
            newBrand.Brand_Name = newBrand.Brand_Name.Replace(newBrand.Brand_Name[0], newBrand.Brand_Name[0].ToString().ToUpper().ToCharArray()[0]);
            db.Car_Brands.Add(newBrand);
            db.SaveChanges();
            return Ok("Car was added");
        }
    }
}
