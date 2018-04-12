using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class User
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }

        public User()
        {
            UserID = -1;
            UserEmail = "uknown email";
            UserPassword = "unknown password";
        }

        public User(ITAPP_CarWorkshopService.User entityUser)
        {
            MakeUserFromEntity(entityUser);
        }

        public void MakeUserFromEntity(ITAPP_CarWorkshopService.User entityUser)
        {
            UserID = entityUser.User_ID;
            UserEmail = entityUser.User_email;
            UserPassword = entityUser.User_password;
        }
    }
}