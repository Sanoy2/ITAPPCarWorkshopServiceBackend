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

        [HttpPost]
        public Response_String AddNewCarBrand([FromBody] Car_Brands CarBrandToAdd)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.AddNewCarBrand(CarBrandToAdd);
            return response;
        }

        [HttpPut]
        public Response_String ModifyExistingCarBrand([FromBody] Car_Brands CarBrandToBeModified)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.ModifyCarBrand(CarBrandToBeModified);
            return response;
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
