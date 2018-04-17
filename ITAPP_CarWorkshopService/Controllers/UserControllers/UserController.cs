using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ITAPP_CarWorkshopService.ResonseClass;
using ITAPP_CarWorkshopService.ModelsManager;
using ITAPP_CarWorkshopService.Authorization;
/// <summary>
/// Controller
/// </summary>
namespace ITAPP_CarWorkshopService.Controllers.UserControllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    public class UserController : ApiController
    {

        [HttpGet]
        public List<DataModels.UserModel> GetUser(int ID)
        {
            return UserManager.GetUser(ID);
        }

        [HttpGet]
        public HttpResponseMessage CheckIfNameValid([FromUri] string EmailAddress)
        {
            return UserManager.CheckIfNameValidPublic(EmailAddress);
        }

        [HttpPost]
        public HttpResponseMessage RegisterUser([FromBody] DataModels.UserModel NewUser)
        {
            if(UserManager.RegisterUser(NewUser))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content =  new StringContent ("User succesfully added to DB.");

                return response;
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("User was not added to DB.");

                return response;
            }
        }
        /*
        [HttpPut]
        [AuthorizationFilter]
        public Response_String Update_User([FromBody]User User)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var user = db.Users.FirstOrDefault(p => p.User_ID == User.User_ID);
                var User_Mail_Repeted = db.Users.Find(db.Users.FirstOrDefault(p => p.User_email == User.User_email)) != null ? true : false;
                if (User_Mail_Repeted)
                {
                    return new Response_String() { Response = "Item already exists" };
                }
                if (user != null)
                {
                    var ID = user.User_ID;
                    user = User;
                    user.User_ID = ID;
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Item was modify" }; ;
                }
                return new Response_String() { Response = "Item does not exsists" }; ;
            }
        }

        [HttpDelete]
        [AuthorizationFilter]
        public Response_String Delete_User(int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var User = db.Users.FirstOrDefault(user => user.User_ID == ID);
                if (User != null)
                {
                    db.Users.Remove(User);
                    db.SaveChangesAsync();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
        */
    }
}
