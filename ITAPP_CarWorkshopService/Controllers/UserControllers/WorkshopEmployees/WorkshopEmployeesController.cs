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
namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopEmployees
{
    /// <summary>
    /// Wokrshop Employees Controller
    /// </summary>
    public class WorkshopEmployeesController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopemployees/allemployeesofworkshop/ + ID &#xD;
        /// </summary>
        /// <param name="ID">  Workshop_ID </param>
        /// <returns>Returns all Employees from specyfic workshop by ID</returns>
        [HttpGet]
        [Route("workshopemployees/allemployeesofworkshop/{ID}")]
        public IEnumerable<Workshop_Employees> Get_all_employees(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Employees.Where(p => p.Workshop_ID == ID);
            }
        }
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopemployees/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Workshop_empoyee_ID</param>
        /// <returns>Return a Employee witch specyfic ID</returns>
        [HttpGet]
        [Route("workshopemployees/{ID}")]
        public Workshop_Employees Get_employee(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Employees.FirstOrDefault(p => p.Workshop_empoyee_ID == ID);
            }
        }
        /// <summary>
        /// POST method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopemployees &#xD;
        /// Workshop_empoyee_ID is automaticly incremented so as a Workshop_empoyee_ID = "" &#xD;
        /// </summary>
        /// <param name="New_Employee">        
        /// {
        ///    User_ID=, &#xD;
        ///    Workshop_empoyee_ID=, &#xD;
        ///    Workshop_ID=, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added"</returns>
        [HttpPost]
        public Response_String Add_workshop_employee([FromBody] Workshop_Employees New_Employee)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                db.Workshop_Employees.Add(New_Employee);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added" };
            }
        }
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopemployees &#xD;
        /// Workshop_empoyee_ID should be passed by in body &#xD;
        /// </summary>
        /// <param name="Modify_Employee">
        /// {
        ///    User_ID=, &#xD;
        ///    Workshop_empoyee_ID=, &#xD;
        ///    Workshop_ID=, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" or "Item does not exsists"</returns>
        [HttpPut]
        public Response_String Modify_workshop_employee([FromBody] Workshop_Employees Modify_Employee)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Employees.FirstOrDefault(p => Modify_Employee.Workshop_empoyee_ID == p.Workshop_empoyee_ID);
                if (Old != null)
                {
                    var ID = Old.Workshop_empoyee_ID;
                    Old = Modify_Employee;
                    Old.Workshop_empoyee_ID = ID;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item dose not exisit" };

            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/workshopemployees/ + ID &#xD;
        /// </summary>
        /// <param name="Workshop_empoyee_ID"> Workshop_empoyee_ID </param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
        [HttpDelete]
        public Response_String Delete_workshop_employee([FromUri] int Workshop_empoyee_ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Employees.FirstOrDefault(p => Workshop_empoyee_ID == p.Workshop_empoyee_ID);
                if (Old != null)
                {
                    db.Workshop_Employees.Remove(Old);
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item dose not exisit" };

            }
        }
    }
}
