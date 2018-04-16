using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.ModelsManager;
using ITAPP_CarWorkshopService.AdditionalModels;
using ITAPP_CarWorkshopService.Authorization;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopProfiles
{
    public class WorkshopProfilesController : ApiController
    {
        [HttpGet]
        [Route("api/workshopprofiles/cities")]
        public List<CityAndZipCode> GetCities()
        {
            return WorkshopProfileManager.GetAllCitiesAndZipCodes();
        }

        [HttpGet]
        public List<DataModels.WorkshopProfileModel> GetWorkshopsByNameAndCity([FromUri] string city, [FromUri] string name)
        {
            return WorkshopProfileManager.GetWorkshopsByCityAndName(city, name);
        }

        [HttpPost]
        [AuthorizationFilter]
        public HttpResponseMessage CreateNewWorkshopProfile(DataModels.WorkshopProfileModel NewWorkshopProfileModel)
        {
            int userID = Token.GetUserIdFromRequestHeader(Request);
            return WorkshopProfileManager.CreateNewWorkshopProfile(NewWorkshopProfileModel, userID);
        }

    }
}
