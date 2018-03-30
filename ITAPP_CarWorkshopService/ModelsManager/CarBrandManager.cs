using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class CarBrandManager
    {
        public static List<Car_Brands> GetListOfAllCarBrands()
        {
            var listOfCarBrands = new List<Car_Brands>();
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                listOfCarBrands = db.Car_Brands.ToList();
            }
            return listOfCarBrands;
        }
    }
}