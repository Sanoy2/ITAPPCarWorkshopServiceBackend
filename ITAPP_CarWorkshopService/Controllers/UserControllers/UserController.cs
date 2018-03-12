using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ITAPP_CarWorkshopService.Helpfukl_classes;
using System.Threading.Tasks;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public async Task<string> Add_User([FromBody] User New_User)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                User User = null;
                User = db.Users.FirstOrDefault(user => user.User_email == User.User_email);
                if (User == null)
                {
                    User = new User()
                    {
                        User_email = User.User_email,
                        User_ID = User.User_ID,
                        User_password = User.User_password,
                        Workshop_Employees = User.Workshop_Employees
                    };
                    db.Users.Add(User);
                    await db.SaveChangesAsync();
                    return $"New user was created {User.User_email} , {User.User_ID}";
                }
            }
            return "User exists";
        }
        [HttpGet]
        public User Get_User(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var User = db.Users.FirstOrDefault(p => p.User_ID == ID);
                if (User != null)
                {
                    return User;
                }
            }
            return new User() {  User_email = "User does not exist's" };
        }
        [HttpPut]
        public async Task<string> Update_User([FromBody]User User)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var user = db.Users.Remove(db.Users.FirstOrDefault(p => p.User_ID == User.User_ID));
                if(user != null)
                {
                    db.Users.Add(User);
                    await db.SaveChangesAsync();
                    return "User modified";   
                }
            }
            return "There is no such User";
        }
        [HttpDelete]
        public async Task<string> Delete_User(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var User = db.Users.FirstOrDefault(user => user.User_ID == ID);
                if(User != null)
                {
                    db.Users.Remove(User);
                    await db.SaveChangesAsync();
                    return "User Deleted";
                }
                return "User with id was not on the list";
            }
        }
    }
}
