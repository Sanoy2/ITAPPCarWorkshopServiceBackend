using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.AdditionalModels
{
    public class CityAndZipCode
    {
        public string City { get; set; }
        public string ZipCode { get; set; }

        public CityAndZipCode()
        {
            City = "unknown";
            ZipCode = "00-000";
        }

        public CityAndZipCode(string _city, string _zipCode)
        {
            City = _city;
            ZipCode = _zipCode;
        }

        public override string ToString()
        {
            return ZipCode + " " + City;
        }
    }
}