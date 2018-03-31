using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.ModelsManager;
using ITAPP_CarWorkshopService.Authorization;

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
        public List<Car_Brands> GetAllCarBrands()
        {
            return CarBrandManager.GetListOfAllCarBrands();
        }
        
        [HttpGet]
        public Car_Brands GetCarBrandSpecifiedById(int id)
        {
            return CarBrandManager.GetCarBrandById(id);
        }

        [HttpPost]
        [AuthorizationFilter]
        public Response_String AddNewCarBrand([FromBody] Car_Brands CarBrandToBeAdded)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.AddNewCarBrand(CarBrandToBeAdded);
            return response;
        }

        [HttpPut]
        [AuthorizationFilter]
        public Response_String ModifyExistingCarBrand([FromBody] Car_Brands CarBrandToBeModified)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.ModifyCarBrand(CarBrandToBeModified);
            return response;
        }

        [HttpDelete]
        [AuthorizationFilter]
        public Response_String DeleteExistingCarBrand(int carBrandId)
        {
            var response = new Response_String();
            response.Response = CarBrandManager.DeleteCarBrandById(carBrandId);
            return response;
        }
    }
}
