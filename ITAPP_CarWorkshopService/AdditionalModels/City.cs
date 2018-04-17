using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.AdditionalModels
{
    public class CityModel
    {
        public string City { get; set; }
        
        public CityModel(string s = "default")
        {
            City = s;
        }
    }
}