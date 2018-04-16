using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Net.Http;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class CarBrandManager
    {
        public static Mutex mutex = new Mutex();

        public static List<DataModels.CarBrandModel> GetListOfAllCarBrands()
        {
            List<DataModels.CarBrandModel> listOfCarBrands = new List<DataModels.CarBrandModel>();

            var db = new ITAPPCarWorkshopServiceDBEntities();

            mutex.WaitOne();

            foreach (Car_Brands brand in db.Car_Brands)
            {
                listOfCarBrands.Add(new DataModels.CarBrandModel(brand));
            }
            mutex.ReleaseMutex();

            return listOfCarBrands;
        }

        public static DataModels.CarBrandModel GetCarBrandById(int carBrandId)
        {
            DataModels.CarBrandModel carBrand;
            var db = new ITAPPCarWorkshopServiceDBEntities();

            mutex.WaitOne();
            carBrand = new DataModels.CarBrandModel(db.Car_Brands.FirstOrDefault(car_brand => car_brand.Brand_ID == carBrandId));
            mutex.ReleaseMutex();

            return carBrand;
        }

        public static HttpResponseMessage AddNewCarBrand(DataModels.CarBrandModel carBrand)
        {
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            result.Content = new StringContent("Car already exisits");
            carBrand.BrandName = AdjustCarBrandName(carBrand.BrandName);

            mutex.WaitOne();

            if (CheckIfCarBrandAlreadyExsistsInDB(carBrand.BrandName))
            {
                mutex.ReleaseMutex();
                return result;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();

            db.Car_Brands.Add(carBrand.MakeCarBrandEntityFromCarBrandModel());
            db.SaveChanges();
            mutex.ReleaseMutex();
            result = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
            result.Content = new StringContent("Brand was added to DB.");

            return result;
        }

        public static HttpResponseMessage ModifyCarBrand(DataModels.CarBrandModel carBrand)
        {
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            result.Content = new StringContent("Brand does not exists in DB.");
            carBrand.BrandName = AdjustCarBrandName(carBrand.BrandName);

            mutex.WaitOne();

            if (!CheckIfCarBrandAlreadyExsistsInDB(carBrand.BrandName))
            {
                mutex.ReleaseMutex();
                return result;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();

            Car_Brands exsistingCarBrand;
            exsistingCarBrand = db.Car_Brands.FirstOrDefault(brand => brand.Brand_Name == carBrand.BrandName);
            exsistingCarBrand.Brand_Name = carBrand.BrandName;
            db.SaveChanges();

            mutex.ReleaseMutex();

            result = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
            result.Content = new StringContent("Brand was probably modified.");

            return result;
        }

        public static HttpResponseMessage DeleteCarBrandById(int brandId)
        {
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            result.Content = new StringContent("Brand does not exists in DB.");

            mutex.WaitOne();

            if (!CheckIfCarBrandAlreadyExsistsInDB(brandId))
            {
                mutex.ReleaseMutex();
                return result;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();

            Car_Brands carBrandToDelete;
            carBrandToDelete = db.Car_Brands.FirstOrDefault(brand => brand.Brand_ID == brandId);
            db.Car_Brands.Remove(carBrandToDelete);
            db.SaveChanges();

            mutex.ReleaseMutex();

            result = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
            result.Content = new StringContent("Brand was probably deleted.");

            return result;
        }

        private static bool CheckIfCarBrandAlreadyExsistsInDB(string carBrandName)
        {
            bool result = false;
            carBrandName = AdjustCarBrandName(carBrandName);

            var db = new ITAPPCarWorkshopServiceDBEntities();

            if (db.Car_Brands.Any(brandName => brandName.Brand_Name == carBrandName))
            {
                result = true;
            }

            return result;
        }

        private static bool CheckIfCarBrandAlreadyExsistsInDB(int carBrandId)
        {
            bool result = false;

            var db = new ITAPPCarWorkshopServiceDBEntities();

            if (db.Car_Brands.Any(brandName => brandName.Brand_ID == carBrandId))
            {
                result = true;
            }

            return result;
        }

        private static string AdjustCarBrandName(string oldString)
        {
            string newString;
            newString = oldString;
            newString = StringAdjustment.RemoveSpaces(newString);
            newString = StringAdjustment.MakeFirstLetterUppercaseTheRestLowercase(newString);
            return newString;
        }
    }
}