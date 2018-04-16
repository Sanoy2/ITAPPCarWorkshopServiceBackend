using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.ModelsManager;

namespace ITAPP_CarWorkshopService.Controllers.Car.CarsFollowed
{
    public class CarsFollowedController : ApiController
    {

        [HttpGet]
        [Route("api/CarsFollowed/byClient/{ID}")]
        public List<DataModels.CarsFollowedModel> GetListOfFollowsByClientId(int ID)
        {
            return CarsFollowedManager.GetListByClientId(ID);
        }

        [HttpGet]
        [Route("api/CarsFollowed/byFollow/{ID}")]
        public List<DataModels.CarsFollowedModel> GetListOfFollowsByFollowId(int ID)
        {
            return CarsFollowedManager.GetListByFollowId(ID);
        }

        [HttpGet]
        [Route("api/CarsFollowed/byCar/{ID}")]
        public List<DataModels.CarsFollowedModel> GetListOfFollowsByCarId(int ID)
        {
            return CarsFollowedManager.GetListByCarId(ID);
        }

        [HttpPost]
        public HttpResponseMessage AddCarFollow([FromBody] DataModels.CarsFollowedModel newCarFollowModel)
        {
            return CarsFollowedManager.AddCarFollowToDB(newCarFollowModel);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCarFollow(int ID)
        {
            return CarsFollowedManager.DeleteFollowByFollowID(ID);
        }
    }
}
