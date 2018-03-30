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
        public Car_Brands Get_Brand(int carBrandId)
        {
            return CarBrandManager.GetCarBrandById(carBrandId);
        }

        [HttpPost]
        [Route("api/carbrands")]
        public Response_String AddNewCarBrand([FromBody] Car_Brands CarBrandToBeAdded)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.AddNewCarBrand(CarBrandToBeAdded);
            return response;
        }

        [HttpPut]
        [Route("api/carbrands")]
        public Response_String ModifyExistingCarBrand([FromBody] Car_Brands CarBrandToBeModified)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.ModifyCarBrand(CarBrandToBeModified);
            return response;
        }

        [HttpDelete]
        [Route("api/carbrands")]
        public Response_String DeleteExistingCarBrand(int carBrandId)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.DeleteCarBrandById(carBrandId);
            return response;
        }
    }
}
