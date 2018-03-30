using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.ModelsManager;

/// <summary>
/// Controller
/// </summary>
namespace ITAPP_CarWorkshopService.Controllers.Car.CarBrands
{
    /// <summary>
    /// Car Brands Controller
    /// </summary>
    public class CarBrandsController : ApiController
    {
        [HttpGet]
        [Route("api/carbrands")]
        public List<Car_Brands> Get_All_Of_Brands()
        {
            return CarBrandManager.GetListOfAllCarBrands();
        }

        [HttpGet]
        [Route("api/carbrands/5")]
        public Car_Brands Get_Brand(int id)
        {
            return CarBrandManager.GetCarBrandById(id);
        }
        /// <summary>
        /// POST method &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/carprofile &#xD;
        /// Brand_ID is automaticly incremented so as a Brand_ID = "" &#xD;
        /// </summary>
        /// <param name="New_Brand">{ Brand_ID =, &#xD; Brand_Name = &#xD; }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added" or "Item already exists"</returns>
        [HttpPost]
        public Response_String Add_New_Brand([FromBody] Car_Brands New_Brand)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            foreach (var Car in db.Car_Brands)
            {
                if (New_Brand.Brand_Name.ToLower() == Car.Brand_Name.ToLower())
                {
                    return new Response_String() { Response = "Item already exists" };
                }
            }
            var newBrand = new Car_Brands()
            {
                Brand_Name = New_Brand.Brand_Name.ToLower()
            };
            newBrand.Brand_Name = newBrand.Brand_Name.Replace(newBrand.Brand_Name[0], newBrand.Brand_Name[0].ToString().ToUpper().ToCharArray()[0]);
            db.Car_Brands.Add(newBrand);
            db.SaveChanges();
            return new Response_String() { Response = "Item was added" };
        }
        /// <summary>
        /// PUT method &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/carprofile &#xD;
        /// Brand_ID should be passed by in body &#xD;
        /// </summary>
        /// <param name="ModifyCarBrand">{ Brand_ID =, &#xD; Brand_Name = &#xD; }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" or "Item does not exsists"</returns>
        [HttpPut]
        public Response_String Modify_Brand([FromBody] Car_Brands ModifyCarBrand)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Car_Brands.FirstOrDefault(p => p.Brand_ID == ModifyCarBrand.Brand_ID);
                if( Old != null)
                {
                    Old = ModifyCarBrand;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item does not exists" };
            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/carprofile/ + ID &#xD;
        /// </summary>
        /// <param name="ID_Brand">ID_Brand</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
        [HttpDelete]
        public Response_String Delete_Car_Brand(int ID_Brand)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var car = db.Car_Brands.FirstOrDefault(p => p.Brand_ID == ID_Brand);
                if(car != null)
                {
                    db.Car_Brands.Remove(car);
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
            }
            return new Response_String() { Response = "Item does not exsists" };
        }
    }
}
