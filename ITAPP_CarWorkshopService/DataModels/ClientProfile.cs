using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class ClientProfile
    {
        public int Client_ID { get; set; }
        public string Client_name { get; set; }
        public string Client_surname { get; set; }
        public string Client_phone_number { get; set; }
        public int User_ID { get; set; }
    }
}