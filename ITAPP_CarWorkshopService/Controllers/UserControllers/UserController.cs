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
        /// <summary>
        /// GET method &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/user + ID &#xD;
        /// </summary>
        /// <param name="ID">User_ID</param>
        /// <returns>Return a specyfic user with passed id or null if there is no such user</returns>
        [HttpGet]
        public User Get_User(int ID)
        {
            return UserManager.GetUser(ID);
        }
        /// <summary>
        /// POST method &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/user &#xD;
        /// User_ID is automaticly incremented so as a User_ID = "" &#xD;
        /// </summary>
        /// <param name="NewUser">
        /// {
        ///     User_email =, &#xD;
        ///     User_ID =, &#xD;
        ///     User_password =, &#xD;
        /// }
        /// </param>
        /// <returns>Returns JSON with { Response : string }
        /// </returns>
        [HttpPost]
        public Response_String RegisterUser([FromBody] User NewUser)
        {
            Response_String response = new Response_String()
            {
                Response = UserManager.RegisterUser(NewUser)
            };

            return response;
        }
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/user &#xD;
        /// User_ID should be passed by in body &#xD;
        /// </summary>
        /// <param name="User">
        /// {
        ///     User_email =, &#xD;
        ///     User_ID =, &#xD;
        ///     User_password =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" , "Item already exists" or "Item does not exsists"</returns>
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
        /// <summary>
        /// DELETE method &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/user/ +ID &#xD;
        /// </summary>
        /// <param name="ID">User_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
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
    }
}
