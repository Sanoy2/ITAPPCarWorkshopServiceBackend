﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.AdditionalModels
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public int UserID { get; set; }
        public int ClientID { get; set; }
        
        public LoginResponse(string token = "", int userId = -1, int clientId = -1)
        {
            Token = token;
            UserID = userId;
            ClientID = clientId;
        }
    }
}