using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class CarBrandManager
    {
        public static Mutex mutex = new Mutex();

        public static List<Car_Brands> GetListOfAllCarBrands()
        {
            var listOfCarBrands = new List<Car_Brands>();
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                mutex.WaitOne();
                listOfCarBrands = db.Car_Brands.ToList();
                mutex.ReleaseMutex();
            }
            return listOfCarBrands;
        }

        public static Car_Brands GetCarBrandById(int carBrandId)
        {
            var carBrand = new Car_Brands();
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                mutex.WaitOne();
                carBrand = db.Car_Brands.FirstOrDefault(car_brand => car_brand.Brand_ID == carBrandId);
                mutex.ReleaseMutex();
            }
            return carBrand;
        }
    }
}