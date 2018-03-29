using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ITAPP_CarWorkshopService.Authorization;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class UserManager
    {
        public static Mutex mutex = new Mutex();

        public static string Login(User user)
        {
            User User;
            string email = user.User_email.ToLower();
            string password = user.User_password;

            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                mutex.WaitOne();
                User = db.Users.FirstOrDefault(record => record.User_email.ToLower() == email);
                mutex.ReleaseMutex();
            }

            if (User == null)
            {
                throw NoUserOfGivenEmail(email);
            }

            if (User.User_password.Equals(password))
            {
                return GenerateTokenForUser(email);
            }
            else
            {
                throw WrongPassword(email);
            }
        }

        public static void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            mutex.WaitOne();
            User User;

            var db = new ITAPPCarWorkshopServiceDBEntities();

            User = db.Users.FirstOrDefault(user => user.User_ID == userId);

            if (User == null)
            {
                throw NoUserOfGivenId(userId);
            }

            if (User.User_password.Equals(oldPassword))
            {
                User.User_password = newPassword;
                db.SaveChanges();
            }
            else
            {
                throw WrongPassword(userId);
            }

            mutex.ReleaseMutex();
        }

        private static string GenerateTokenForUser(string email)
        {
            int userId;
            Token token;
            string result;

            try
            {
                userId = GetUserIdByUserEmailPrivate(email);
                token = new Token(userId);
                token.RegisterToken();
                result = token.TokenString;
                return result;
            }
            catch (Exception)
            {
                throw NoUserOfGivenEmail(email);
            }
        }

        private static int GetUserIdByUserEmailPrivate(string email)
        {
            User User;
            email = email.ToLower();
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                User = db.Users.FirstOrDefault(user => user.User_email.ToLower() == email);
            }

            if (User == null)
            {
                throw NoUserOfGivenEmail(email);
            }

            return User.User_ID;
        }

        public static int GetUserIdByUserEmail(string email)
        {
            int userId;

            mutex.WaitOne();

            userId = GetUserIdByUserEmailPrivate(email);

            mutex.ReleaseMutex();

            return userId;
        }

        public static bool CheckIfUserExistsByID(int userId)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                mutex.WaitOne();
                User User = db.Users.FirstOrDefault(user => user.User_ID == userId);
                mutex.ReleaseMutex();
                if (User == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
    }
}