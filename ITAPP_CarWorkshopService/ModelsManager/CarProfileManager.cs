using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Net.Http;
using System.Net;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class CarProfileManager
    {
        private static Mutex mutex = new Mutex();

        public static List<DataModels.CarProfileModel> GetAllCarProfiles()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            mutex.WaitOne();

            var ListOfEntities = db.Car_Profiles.ToList();

            mutex.ReleaseMutex();

            var ListOfModels = DataModels.CarProfileModel.ListOfEntityToListOfModels(ListOfEntities);

            return ListOfModels;
        }

        public static List<DataModels.CarProfileModel> GetCarProfileById(int CarProfileId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            mutex.WaitOne();

            var ListOfEntities = db.Car_Profiles.Where(n => n.Car_ID == CarProfileId).ToList();

            mutex.ReleaseMutex();

            var ListOfModels = DataModels.CarProfileModel.ListOfEntityToListOfModels(ListOfEntities);

            return ListOfModels;
        }

        public static HttpResponseMessage AddCarToDB(DataModels.CarProfileModel NewCarProfileModel)
        {
            mutex.WaitOne();
            if (!CheckIfCarProfileExistsByNIP(NewCarProfileModel.CarVINNumber))
            {
                mutex.ReleaseMutex();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("Car profile of given VIN number already exists.");

                return response;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            var CarProfileEntity = NewCarProfileModel.MakeCarProfileEntityFromCarProfileModel();
            try
            {
                db.Car_Profiles.Add(CarProfileEntity);
                db.SaveChanges();

                mutex.ReleaseMutex();

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Car profile was succesfully created");

                return response;
            }
            catch(Exception e)
            {
                mutex.ReleaseMutex();
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("Something gone wrong while adding the car profile to DB");

                return response;
            }
        }

        private static bool CheckIfCarProfileExistsByNIP(string VIN)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            return db.Car_Profiles.Any(n => n.Car_VIN_number == VIN);
        }
    }
}