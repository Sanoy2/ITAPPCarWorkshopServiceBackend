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
        public List<DataModels.CarBrandModel> GetAllCarBrands()
        {
            return CarBrandManager.GetListOfAllCarBrands();
        }
        
        [HttpGet]
        public DataModels.CarBrandModel GetCarBrandSpecifiedById(int id)
        {
            return CarBrandManager.GetCarBrandById(id);
        }

        [HttpPost]
        [AuthorizationFilter]
        public HttpResponseMessage AddNewCarBrand([FromBody] DataModels.CarBrandModel CarBrandToBeAdded)
        {
            return CarBrandManager.AddNewCarBrand(CarBrandToBeAdded);
        }

        [HttpPut]
        [AuthorizationFilter]
        public HttpResponseMessage ModifyExistingCarBrand([FromBody] DataModels.CarBrandModel CarBrandToBeModified)
        {
            return CarBrandManager.ModifyCarBrand(CarBrandToBeModified);
        }

        [HttpDelete]
        [AuthorizationFilter]
        public HttpResponseMessage DeleteExistingCarBrand(int carBrandId)
        {
            return CarBrandManager.DeleteCarBrandById(carBrandId);
        }
    }
}
