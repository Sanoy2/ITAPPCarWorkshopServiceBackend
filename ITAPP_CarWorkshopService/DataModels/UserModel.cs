using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }

        public UserModel()
        {
            UserID = -1;
            UserEmail = "uknown email";
            UserPassword = "unknown password";
        }

        public UserModel(ITAPP_CarWorkshopService.User entityUser)
        {
            MakeUserModelFromUserEntity(entityUser);
        }

        public void MakeUserModelFromUserEntity(ITAPP_CarWorkshopService.User userEntity)
        {
            UserID = userEntity.User_ID;
            UserEmail = userEntity.User_email;
            UserPassword = userEntity.User_password;
        }

        public ITAPP_CarWorkshopService.User MakeUserEntityFromUserModel()
        {
            var userEntity = new ITAPP_CarWorkshopService.User()
            {
                User_ID = UserID,
                User_email = UserEmail,
                User_password = UserPassword
            };

            return userEntity;
        }
    }
}