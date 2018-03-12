using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ITAPP_CarWorkshopService.ResonseClass;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public Response_String Add_User([FromBody] User New_User)
        {
            var Response = new Response_String(){ Response = "User exists"};
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var User = db.Users.FirstOrDefault(user => user.User_email == New_User.User_email);
                if (User != null)
                {
                    return Response;
                }
                 User = new User()
                {
                    User_email = New_User.User_email,
                    User_ID = New_User.User_ID,
                    User_password = New_User.User_password,
                    Workshop_Employees = New_User.Workshop_Employees
                };
                db.Users.Add(User);
                db.SaveChanges();
                Response.Response= $"New user was created {User.User_email} , {User.User_ID}";
                return Response;
            }
        }
        [HttpGet]
        public User Get_User(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            return db.Users.FirstOrDefault(p => p.User_ID == ID);
        }

        [HttpGet]
        public List<User> Get_All_User()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            return db.Users.ToList();
        }

        [HttpPut]
        public Response_String Update_User([FromBody]User User)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Response = new Response_String() { Response = "User modified" };
                var user = db.Users.Remove(db.Users.FirstOrDefault(p => p.User_ID == User.User_ID));
                if(user != null)
                {
                    db.Users.Add(User);
                    db.SaveChangesAsync();
                    return Response;   
                }
                Response.Response = "User undefined";
                return Response;
            }
        }
        [HttpDelete]
        public Response_String Delete_User(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var User = db.Users.FirstOrDefault(user => user.User_ID == ID);
                if(User != null)
                {
                    db.Users.Remove(User);
                    db.SaveChangesAsync();
                    return new Response_String(){Response = "User Deleted"};
                }
                return new Response_String() { Response = "User with id was not on the list" };
            }
        }
    }
}
