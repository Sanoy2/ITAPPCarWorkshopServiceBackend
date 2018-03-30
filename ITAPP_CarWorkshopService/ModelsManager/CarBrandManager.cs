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

        public static string AddNewCarBrand(Car_Brands carBrand)
        {
            string result = "";

            carBrand.Brand_Name = MakeFirstLetterUppercaseTheRestLowercase(carBrand.Brand_Name);

            mutex.WaitOne();

            if (CheckIfCarBrandAlreadyExsistsInDB(carBrand.Brand_Name))
            {
                result = "Brand already exists in DB.";
                mutex.ReleaseMutex();
                return result;
            }

            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                db.Car_Brands.Add(carBrand);
                db.SaveChanges();
                mutex.ReleaseMutex();
            }

            result = "Brand was added to DB.";

            return result;
        }

        public static string ModifyCarBrand(Car_Brands carBrand)
        {
            string result = "";

            carBrand.Brand_Name = MakeFirstLetterUppercaseTheRestLowercase(carBrand.Brand_Name);

            mutex.WaitOne();

            if (!CheckIfCarBrandAlreadyExsistsInDB(carBrand.Brand_Name))
            {
                result = "Brand does not exists in DB.";
                mutex.ReleaseMutex();
                return result;
            }

            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                Car_Brands exsistingCarBrand;
                exsistingCarBrand = db.Car_Brands.FirstOrDefault(brand => brand.Brand_Name == carBrand.Brand_Name);
                exsistingCarBrand.Brand_Name = carBrand.Brand_Name;
                db.SaveChanges();
            }

            mutex.ReleaseMutex();

            result = "Brand was probably modified.";

            return result;
        }

        public static string DeleteCarBrandById(int brandId)
        {
            string result = "";

            mutex.WaitOne();

            if (!CheckIfCarBrandAlreadyExsistsInDB(brandId))
            {
                result = "Brand does not exists in DB.";
                mutex.ReleaseMutex();
                return result;
            }

            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                Car_Brands carBrandToDelete;
                carBrandToDelete = db.Car_Brands.FirstOrDefault(brand => brand.Brand_ID == brandId);
                db.Car_Brands.Remove(carBrandToDelete);
                db.SaveChanges();
            }

            mutex.ReleaseMutex();

            result = "Brand was probably deleted.";

            return result;
        }

        private static bool CheckIfCarBrandAlreadyExsistsInDB(string carBrandName)
        {
            bool result = false;
            carBrandName = MakeFirstLetterUppercaseTheRestLowercase(carBrandName);

            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                if (db.Car_Brands.Any(brandName => brandName.Brand_Name == carBrandName))
                {
                    result = true;
                }
            }

            return result;
        }

        private static bool CheckIfCarBrandAlreadyExsistsInDB(int carBrandId)
        {
            bool result = false;

            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                if (db.Car_Brands.Any(brandName => brandName.Brand_ID == carBrandId))
                {
                    result = true;
                }
            }

            return result;
        }

        private static string MakeFirstLetterUppercaseTheRestLowercase(string oldString)
        {
            if (oldString.Length == 0)
            {
                return oldString;
            }

            string newString = oldString;

            newString = newString.ToLower();

            string firstLetter = newString.Substring(0, 1);
            firstLetter = firstLetter.ToUpper();

            newString = newString.Replace(newString[0], firstLetter[0]);

            return newString;
        }
    }
}