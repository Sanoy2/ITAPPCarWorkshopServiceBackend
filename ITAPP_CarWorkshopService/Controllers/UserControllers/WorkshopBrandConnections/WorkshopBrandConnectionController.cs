using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;
/// <summary>
/// Controller
/// </summary>
namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopBrandConnections
{
    /// <summary>
    /// Workshop Brand Connection Controller
    /// </summary>
    public class WorkshopBrandConnectionController : ApiController
    {
        [HttpGet]
        public List<DataModels.WorkshopBrandConnectionsModels> Get_All_Connections()
        {
            return ModelsManager.WorksopBrandConnectionMenager.GetAllConnections();
        }
        [HttpPost]
        public HttpResponseMessage Add_Workshop_Brand([FromBody] DataModels.WorkshopBrandConnectionsModels NewWorkshopBrand) 
        {
            return ModelsManager.WorksopBrandConnectionMenager.AddNewWorkshopConnectionBrand(NewWorkshopBrand);
        }
        [HttpPut]
        public HttpResponseMessage Modify_Workshop_Brand([FromBody] DataModels.WorkshopBrandConnectionsModels ModifyWorkshopBrand)
        {
            return ModelsManager.WorksopBrandConnectionMenager.ModifyWorkshopConnectionBrand(ModifyWorkshopBrand);
        }
        [HttpDelete]
        public HttpResponseMessage Delete_Workshop_Brand(int ID)
        {
            return ModelsManager.WorksopBrandConnectionMenager.DeleteWorkshopConnectionBrand(ID);
        }
    }
}
