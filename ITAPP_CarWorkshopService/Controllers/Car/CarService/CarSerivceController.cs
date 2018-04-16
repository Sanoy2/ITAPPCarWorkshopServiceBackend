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
namespace ITAPP_CarWorkshopService.Controllers.Car.CarService
{
    /// <summary>
    /// Car Service Controller
    /// </summary>
    public class CarSerivceController : ApiController
    {
        
        [HttpGet]
        public List<DataModels.CarServiceModel> Get_Cars_Service()
        {
                return  ModelsManager.CarServiceMenager.GetListOfCarService();
        }

        [HttpGet]
        public DataModels.CarServiceModel Get_Car_Service(int ID)
        {
                return ModelsManager.CarServiceMenager.GetListByID(ID);
        }
        [HttpPost]
        public HttpResponseMessage Add_Car_Service([FromBody] DataModels.CarServiceModel New_Car_Profile)
        {
            return ModelsManager.CarServiceMenager.AddCarService(New_Car_Profile);
        }
        [HttpPut]
        public HttpResponseMessage Change_Car_Service([FromBody] DataModels.CarServiceModel Modifi_car)
        {
            return ModelsManager.CarServiceMenager.ModifyCarService(Modifi_car);
        }
        [HttpDelete]
        public HttpResponseMessage Delete_Car_Service(int ID)
        {
            return ModelsManager.CarServiceMenager.DeleteCarService(ID);
        }
    }
}
