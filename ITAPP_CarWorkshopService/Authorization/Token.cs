﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.Authorization
{
    public class Token
    {
        private static List<Token> listOfTokens = new List<Token>();

        private static string AdminTokenString = "AdminTokenString";

        private static int timeOfExpirationMinutes = 0;
        private static int timeOfExpirationHours = 0;
        private static int timeOfExpirationDays = 1;

        public string TokenString { get; private set; }
        public DateTime TermOfExpiration { get; private set; }

        public Token(int userID = -999)
        {
            GenerateTokenString(userID);
        }

        public void GenerateTokenString(int userID)
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
            if (!listOfTokens.Exists(n => n.TokenString == this.TokenString))
            {
                TermOfExpiration = NewTimeOfExpiration();

                listOfTokens.Add(this);
            }
        }

        public static bool ValidateToken(string tokenString)
        {
            if (tokenString.Equals(AdminTokenString))
            {
                return true;
            }

            bool result = false;
            
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

            return result;
        }

        private static Token GetTokenFromList(string tokenString)
        {
            Token token = null;
            token = listOfTokens.Find(n => n.TokenString.Equals(tokenString));
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

            if(token.TermOfExpiration.Ticks >= timeNow.Ticks)
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
            listOfTokens.RemoveAll(n => n.TermOfExpiration.Ticks > expirationDateInTicks);
        }

        private static void RefreshTokenTimeOfExpiration(Token token)
        {
            // TODO: refreshing token's time of expirarion 
            token.TermOfExpiration = NewTimeOfExpiration();
        }

        private static DateTime NewTimeOfExpiration()
        {
            return DateTime.Now.AddMinutes(timeOfExpirationMinutes).AddHours(timeOfExpirationHours).AddDays(timeOfExpirationDays);
        }

        public static void Logout(Token token)
        {
            Logout(token.TokenString);
        }

        public static void Logout(string tokenString)
        {
            RemoveToken(tokenString);
        }

    }
}