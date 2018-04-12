using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class CarProfileModel
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public string Carmodel { get; set; }
        public string CarVINNumber { get; set; }
        public int CarProductionYear { get; set; }
        public Nullable<int> CarFirstRegistrationYear { get; set; }
    }
}