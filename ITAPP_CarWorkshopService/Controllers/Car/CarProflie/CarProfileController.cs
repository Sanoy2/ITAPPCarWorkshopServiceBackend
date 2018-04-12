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
namespace ITAPP_CarWorkshopService.Controllers.Car.CarProflie
{
    /// <summary>
    /// Car Profile Controller
    /// </summary>
    public class CarProfileController : ApiController
    {

        [HttpGet]
        public List<DataModels.CarProfileModel> GetAllCarProfiles()
        {
            return CarProfileManager.GetAllCarProfiles();
        }

        [HttpGet]
        public List<DataModels.CarProfileModel> GetCarProfilesByCarId(int ID)
        {
            return CarProfileManager.GetCarProfileById(ID);
        }

        [HttpPost]
        public HttpResponseMessage AddCarProfileToDB([FromBody] DataModels.CarProfileModel NewCarProfileModel)
        {
            return CarProfileManager.AddCarToDB(NewCarProfileModel);
        }
    }
}
