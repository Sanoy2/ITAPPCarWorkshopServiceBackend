using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.AdditionalModels
{
    public class CityAndZipCode
    {
        public string City { get; set; }
        public string ZipCode
        { get
            {
                return ZipCode;
            }
          set
            {
                ZipCode = AdjustZipCode(value);
            }
        }

        public CityAndZipCode()
        {
            City = "unknown";
            ZipCode = "00-000";
        }

        public CityAndZipCode(string _city, string _zipCode)
        {
            City = _city;
            ZipCode = AdjustZipCode(_zipCode);
        }

        private string AdjustZipCode(string ZipCode = "00000")
        {
            ZipCode = ZipCode.Substring(0, 5);
            ZipCode = ZipCode.Substring(0, 2) + "-" + ZipCode.Substring(2);
            return ZipCode;
        }
    }
}