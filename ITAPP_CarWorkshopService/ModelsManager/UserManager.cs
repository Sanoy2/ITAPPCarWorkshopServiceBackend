using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ITAPP_CarWorkshopService.Authorization;
using ITAPP_CarWorkshopService.DataModels;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class UserManager
    {
        public static Mutex mutex = new Mutex();

        public static List<DataModels.UserModel> GetUser(int userId)
        {
            mutex.WaitOne();

            if (!CheckIfUserExistsPrivate(userId))
            {
                mutex.ReleaseMutex();
                return null;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            DataModels.UserModel user = new DataModels.UserModel(db.Users.FirstOrDefault(n => n.User_ID == userId));
            mutex.ReleaseMutex();

            user.UserPassword = "******";

            var list = new List<DataModels.UserModel>();
            list.Add(user);

            return list;
        }

        public static HttpResponseMessage CheckIfNameValidPublic(string EmailAddress)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            if (db.Users.Any(n => n.User_email.ToLower() == EmailAddress.ToLower()))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return response;
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
        }

        public static bool RegisterUser(DataModels.UserModel UserModel)
        {
            UserModel.UserEmail = UserEmailAdjustment(UserModel.UserEmail);

            var UserEntity = UserModel.MakeUserEntityFromUserModel();

            var db = new ITAPPCarWorkshopServiceDBEntities();

            mutex.WaitOne();
            if (CheckIfUserExistsPrivate(UserModel.UserEmail))
            {
                mutex.ReleaseMutex();
                return false;
            }

            db.Users.Add(UserEntity);
            db.SaveChanges();

            mutex.ReleaseMutex();

            return true;
        }

        public static HttpResponseMessage Login(DataModels.UserModel user)
        {
            user.UserEmail = UserEmailAdjustment(user.UserEmail);

            var db = new ITAPPCarWorkshopServiceDBEntities();

            mutex.WaitOne();
            if (!CheckIfUserExistsPrivate(user.UserEmail))
            {
                mutex.ReleaseMutex();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("Account of given email address does not exists.");

                return response;
            }

            if (TryToLogIn(user))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                string TokenString = GenerateTokenForUser(user.UserEmail);
                int userID = GetUserIdByUserEmailPrivate(user.UserEmail);

                mutex.ReleaseMutex();

                var ResponseContentAsModel = new ITAPP_CarWorkshopService.AdditionalModels.LoginResponse()
                {
                    Token = TokenString,
                    UserID = userID
                };

                var ResponseContentAsJSON = JsonConvert.SerializeObject(ResponseContentAsModel);
                response.Content = new StringContent(ResponseContentAsJSON); 

                return response; 
            }
            else
            {
                mutex.ReleaseMutex();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("Wrong password");

                return response;
            }
        }

        private static bool TryToLogIn(DataModels.UserModel user)
        {
            bool result = false;

            var db = new ITAPPCarWorkshopServiceDBEntities();

            result = db.Users.Any(n => n.User_email.Equals(user.UserEmail) && n.User_password.Equals(user.UserPassword));

            return result;
        }

        public static void ChangePassword()
        {

        }

        private static string GenerateTokenForUser(string email)
        {
            int userId;
            Token token;
            email = UserEmailAdjustment(email);

            if (!CheckIfUserExistsPrivate(email))
            {
                throw NoUserOfGivenEmail(email);
            }

            userId = GetUserIdByUserEmailPrivate(email);
            token = new Token(userId);
            token.RegisterToken();
            string result = token.TokenString;
            return result;
        }

        public static int GetUserIdByUserEmail(string email)
        {
            int userId;

            mutex.WaitOne();

            userId = GetUserIdByUserEmailPrivate(email);

            mutex.ReleaseMutex();

            return userId;
        }

        private static int GetUserIdByUserEmailPrivate(string email)
        {
            User User;
            email = UserEmailAdjustment(email);
            var db = new ITAPPCarWorkshopServiceDBEntities();

            User = db.Users.FirstOrDefault(user => user.User_email.ToLower() == email);


            if (User == null)
            {
                throw NoUserOfGivenEmail(email);
            }

            return User.User_ID;
        }

        public static bool CheckIfUserExists(int userId)
        {
            mutex.WaitOne();

            bool result = CheckIfUserExistsPrivate(userId);

            mutex.ReleaseMutex();

            return result;
        }

        public static bool CheckIfUserExists(string userEmail)
        {
            mutex.WaitOne();

            bool result = CheckIfUserExistsPrivate(userEmail);

            mutex.ReleaseMutex();

            return result;
        }

        private static bool CheckIfUserExistsPrivate(int userId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Users.Any(user => user.User_ID == userId);
        }

        private static bool CheckIfUserExistsPrivate(string userEmail)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            userEmail = UserEmailAdjustment(userEmail);

            return db.Users.Any(user => user.User_email.ToLower().Equals(userEmail));
        }

        private static Exception NoUserOfGivenEmail(string email)
        {
            string exceptionMessage;
            exceptionMessage = "User with email: ";
            exceptionMessage += email;
            exceptionMessage += " does not exists.";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static Exception NoUserOfGivenId(int userId)
        {
            string exceptionMessage;
            exceptionMessage = "User with id: ";
            exceptionMessage += userId;
            exceptionMessage += " does not exists.";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static Exception WrongPassword(string email)
        {
            string exceptionMessage;
            exceptionMessage = "Wrong password for user: ";
            exceptionMessage += email;
            exceptionMessage += ".";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static Exception WrongPassword(int id)
        {
            string exceptionMessage;
            exceptionMessage = "Wrong password for user id: ";
            exceptionMessage += id;
            exceptionMessage += ".";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static string UserEmailAdjustment(string oldString)
        {
            string newString = oldString;
            newString = StringAdjustment.RemoveSpaces(newString);
            newString = newString.ToLower();
            return newString;
        }
    }
}