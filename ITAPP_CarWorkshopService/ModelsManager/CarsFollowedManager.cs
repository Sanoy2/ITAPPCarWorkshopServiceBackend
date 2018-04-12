using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITAPP_CarWorkshopService.DataModels;
using System.Threading;
using System.Net.Http;
using System.Net;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class CarsFollowedManager
    {
        private static Mutex mutex = new Mutex();

        public static List<DataModels.CarsFollowedModel> GetListByClientId(int ClientId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            List<DataModels.CarsFollowedModel> ListOfCarsFollowedModels;
            List<ITAPP_CarWorkshopService.Cars_followed> ListOfCarsFollowedEntities;
            mutex.WaitOne();

            ListOfCarsFollowedEntities = db.Cars_followed.Where(n => n.Client_ID == ClientId).ToList();

            mutex.ReleaseMutex();

            ListOfCarsFollowedModels = DataModels.CarsFollowedModel.ListOfEntitiesToListOfModels(ListOfCarsFollowedEntities);

            return ListOfCarsFollowedModels;
        }

        public static List<DataModels.CarsFollowedModel> GetListByCarId(int CarId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            List<DataModels.CarsFollowedModel> ListOfCarsFollowedModels;
            List<ITAPP_CarWorkshopService.Cars_followed> ListOfCarsFollowedEntities;
            mutex.WaitOne();

            ListOfCarsFollowedEntities = db.Cars_followed.Where(n => n.Car_profile_ID == CarId).ToList();

            mutex.ReleaseMutex();

            ListOfCarsFollowedModels = DataModels.CarsFollowedModel.ListOfEntitiesToListOfModels(ListOfCarsFollowedEntities);

            return ListOfCarsFollowedModels;
        }

        public static List<DataModels.CarsFollowedModel> GetListByFollowId(int FollowId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            List<DataModels.CarsFollowedModel> ListOfCarsFollowedModels;
            List<ITAPP_CarWorkshopService.Cars_followed> ListOfCarsFollowedEntities;
            mutex.WaitOne();

            ListOfCarsFollowedEntities = db.Cars_followed.Where(n => n.Car_follow_ID == FollowId).ToList();

            mutex.ReleaseMutex();

            ListOfCarsFollowedModels = DataModels.CarsFollowedModel.ListOfEntitiesToListOfModels(ListOfCarsFollowedEntities);

            return ListOfCarsFollowedModels;
        }

        public static HttpResponseMessage AddCarFollowToDB(DataModels.CarsFollowedModel newCarFollowModel)
        {
            mutex.WaitOne();

            if(CheckIfFollowExists(newCarFollowModel.ClientProfileID, newCarFollowModel.CarProfileID))
            {
                mutex.ReleaseMutex();

                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("This user already follows this car.");

                return response;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            ITAPP_CarWorkshopService.Cars_followed newCarFollowEntity;
            newCarFollowEntity = newCarFollowModel.MakeCarsFollowedEntityFromCarsFollowedModel();

            try
            {
                db.Cars_followed.Add(newCarFollowEntity);
                db.SaveChanges();

                mutex.ReleaseMutex();

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Follow added to DB.");

                return response;
            }
            catch(Exception e)
            {
                mutex.ReleaseMutex();

                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("Something gone wrong while adding follow to DB.");

                return response;
            }
        }

        public static HttpResponseMessage DeleteFollowByFollowID(int FollowId)
        {
            mutex.WaitOne();

            if (CheckIfFollowExists(FollowId))
            {
                mutex.ReleaseMutex();

                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("The follow does not exists.");

                return response;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            ITAPP_CarWorkshopService.Cars_followed CarFollowEntityToRemove;
            CarFollowEntityToRemove = db.Cars_followed.FirstOrDefault(n => n.Car_follow_ID == FollowId);

            try
            {
                db.Cars_followed.Remove(CarFollowEntityToRemove);
                db.SaveChanges();

                mutex.ReleaseMutex();

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Follow removed from DB.");

                return response;
            }
            catch (Exception e)
            {
                mutex.ReleaseMutex();

                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("Something gone wrong while removing follow from DB.");

                return response;
            }
        }

        private static bool CheckIfFollowExists(int ClientId, int CarId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Cars_followed.Any(n => n.Client_ID == ClientId && n.Car_profile_ID == CarId);
        }

        private static bool CheckIfFollowExists(int followId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Cars_followed.Any(n => n.Car_follow_ID == followId);
        }
    }
}