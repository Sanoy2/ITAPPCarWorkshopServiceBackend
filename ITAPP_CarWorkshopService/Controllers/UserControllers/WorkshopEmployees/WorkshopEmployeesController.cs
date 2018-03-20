using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopEmployees
{
    public class WorkshopEmployeesController : ApiController
    {
        [HttpGet]
        [Route("workshopemployees/allofworkshop/{ID}")]
        public IEnumerable<Workshop_Employees> Get_all_employees(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Employees.Where(p => p.Workshop_ID == ID);
            }
        }
        [HttpGet]
        [Route("workshopemployees/{ID}")]
        public Workshop_Employees Get_employee(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Employees.FirstOrDefault(p => p.Workshop_empoyee_ID == ID);
            }
        }
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
        [HttpPut]
        public Response_String Modify_workshop_employee([FromBody] Workshop_Employees Modify_Employee)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Employees.FirstOrDefault(p => Modify_Employee.Workshop_empoyee_ID == p.Workshop_empoyee_ID);
                if (Old != null)
                {
                    Old = Modify_Employee;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item dose not exisit" };

            }
        }
    }
}
