using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Net.Http;

namespace ITAPP_CarWorkshopService.Authorization
{
    public class Token
    {
        private static List<Token> listOfTokens = new List<Token>();

        private static string AdminTokenString = "itappADMIN";

        private static int timeOfExpirationMinutes = 0;
        private static int timeOfExpirationHours = 0;
        private static int timeOfExpirationDays = 1;

        public string TokenString { get; private set; }
        public DateTime DateOfExpiration { get; private set; }

        private static readonly Mutex mutex = new Mutex();

        public Token(int userID = -999)
        {
            GenerateTokenString(userID);
        }

        public static List<Token> GetAllForAdminOnly()
        {
            return listOfTokens;
        }

        public static int GetUserIdFromRequestHeader(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues;
            headerValues = request.Headers.GetValues("Token");
            string tokenString = headerValues.First();
            return GetUserIdFromEncryptedTokenString(tokenString);
        }

        public static string GetTokenStringFromRequestHeader(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues;
            headerValues = request.Headers.GetValues("Token");
            string tokenString = headerValues.First();
            return tokenString;
        }

        public static int GetUserIdFromEncryptedTokenString(Token token)
        {
            return GetUserIdFromEncryptedTokenString(token.TokenString);
        }

        public static int GetUserIdFromEncryptedTokenString(string encryptedTokenString)
        {
            Encryption encryption = new Encryption();
            string decryptedTokenString = encryption.Decrypt(encryptedTokenString);
            string[] splitString = decryptedTokenString.Split(':');
            int result = -1;
            Int32.TryParse(splitString[1], out result);
            return result;
        }

        private void GenerateTokenString(int userID)
        {
            // TODO: find a good way to generate token body 
            // this is only temporary solution
            Encryption encryption = new Encryption();
            TokenString = DateTime.Now.Ticks.ToString();
            TokenString += ":" + userID;
            TokenString = encryption.Encrypt(TokenString);
        }

        public void RegisterToken()
        {
            mutex.WaitOne();

            if (!listOfTokens.Exists(n => n.TokenString == this.TokenString))
            {
                DateOfExpiration = NewDateOfExpiration();

                listOfTokens.Add(this);
            }
            else
            {
                Token token = GetTokenFromList(this.TokenString);
                RefreshTokenTimeOfExpiration(token);
            }

            mutex.ReleaseMutex();
        }

        public static bool ValidateToken(string tokenString)
        {
            if (tokenString.Equals(AdminTokenString))
            {
                return true;
            }

            bool result = false;

            mutex.WaitOne();

            if (CheckIfTokenExists(tokenString))
            {
                Token token = GetTokenFromList(tokenString);
                if (CheckIfTokenUpToDate(token))
                {
                    RefreshTokenTimeOfExpiration(token);
                    result = true;
                }
                else
                {
                    RemoveToken(token);
                }
            }

            mutex.ReleaseMutex();

            return result;
        }

        private static Token GetTokenFromList(string tokenString)
        {
            mutex.WaitOne();

            Token token = null;
            token = listOfTokens.Find(n => n.TokenString.Equals(tokenString));

            mutex.ReleaseMutex();

            return token;
        }

        private static bool CheckIfTokenExists(string tokenString)
        {
            if(listOfTokens.Exists(n => n.TokenString.Equals(tokenString)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckIfTokenUpToDate(Token token)
        {
            DateTime timeNow = DateTime.Now;

            if(token.DateOfExpiration.Ticks >= timeNow.Ticks)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void RemoveToken(Token token)
        {
            RemoveToken(token.TokenString);
        }

        private static void RemoveToken(string tokenString)
        {
            listOfTokens.RemoveAll(n => n.TokenString.Equals(tokenString));
        }

        private static void RemoveExpiredAllTokens()
        {
            long expirationDateInTicks = DateTime.Now.Ticks;
            listOfTokens.RemoveAll(n => n.DateOfExpiration.Ticks > expirationDateInTicks);
        }

        private static void RefreshTokenTimeOfExpiration(Token token)
        {
            // TODO: refreshing token's time of expirarion 
            token.DateOfExpiration = NewDateOfExpiration();
        }

        private static DateTime NewDateOfExpiration()
        {
            return DateTime.Now.AddMinutes(timeOfExpirationMinutes).AddHours(timeOfExpirationHours).AddDays(timeOfExpirationDays);
        }

        public static void Logout(Token token)
        {
            Logout(token.TokenString);
        }

        public static void Logout(string tokenString)
        {
            mutex.WaitOne();

            RemoveToken(tokenString);

            mutex.ReleaseMutex();
        }

    }
}