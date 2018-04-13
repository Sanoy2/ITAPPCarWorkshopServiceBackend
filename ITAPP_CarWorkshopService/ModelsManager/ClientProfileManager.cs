using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ITAPP_CarWorkshopService.DataModels;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class ClientProfileManager
    {
        private static Mutex mutex = new Mutex();

        public static List<DataModels.ClientProfile> GetClientProfileById(int id , bool withCarsFollowed)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            if (withCarsFollowed)
            {
                ClientProfile clientProfile;
                var ListCarsFollowed = new List<DataModels.CarProfileModel>();

                mutex.WaitOne();

                clientProfile = new ClientProfile(db.Client_Profiles.FirstOrDefault(n => n.Client_ID == id));
                foreach (Cars_followed Car in db.Cars_followed.Where(car => car.Client_ID == id))
                {
                    ListCarsFollowed.Add(new CarProfileModel(Car.Car_Profiles));
                }
                mutex.ReleaseMutex();
                clientProfile.CarsFollowedByClient = ListCarsFollowed;
                var list = new List<ClientProfile>();
                list.Add(clientProfile);

                return list;
            }
            else
            {
                ClientProfile clientProfile;
                mutex.WaitOne();
                clientProfile = new ClientProfile(db.Client_Profiles.FirstOrDefault(n => n.Client_ID == id));
                mutex.ReleaseMutex();
                var list = new List<ClientProfile>();
                list.Add(clientProfile);
                return list;
            }
        }

        public static List<DataModels.ClientProfile> GetAllClientsProfiles()
        {
            
            var db = new ITAPPCarWorkshopServiceDBEntities();

            var ListOfEntities = new List<Client_Profiles>();
            var ListOfModels = new List<DataModels.ClientProfile>();

            mutex.WaitOne();
            ListOfEntities = db.Client_Profiles.ToList();
            mutex.ReleaseMutex();

            ListOfModels = DataModels.ClientProfile.ListOfEntitiesToListOfModels(ListOfEntities);

            return ListOfModels;
        }

        public static bool PutNewClientProfileToDB(DataModels.ClientProfile NewClientProfileModel)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            Client_Profiles NewClientProfileEntity = NewClientProfileModel.MakeEntityFromModel();

            try
            {
                mutex.WaitOne();

                db.Client_Profiles.Add(NewClientProfileEntity);
                db.SaveChanges();

                mutex.ReleaseMutex();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public static bool ModifyExistingClientProfile(DataModels.ClientProfile ClientProfileModel)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            bool profileExists = false;

            mutex.WaitOne();

            if (!CheckIfClientProfileExists(ClientProfileModel))
            {
                profileExists = false;
                mutex.ReleaseMutex();
                return profileExists;
            }

            try
            {
                Client_Profiles ClientProfileEntity = db.Client_Profiles.FirstOrDefault(n => n.Client_ID == ClientProfileModel.ClientID);

                ClientProfileEntity.Client_name = ClientProfileModel.ClientName;
                ClientProfileEntity.Client_surname = ClientProfileModel.ClientSurname;
                ClientProfileEntity.Client_phone_number = ClientProfileModel.ClientPhoneNumber;

                db.SaveChanges();
                mutex.ReleaseMutex();
                return true;
            }
            catch (Exception e)
            {
                mutex.ReleaseMutex();
                return false;
            }
        }

        public static bool DeleteClientProfileFromDB(int ClientProfileID)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            bool profileExists = false;

            mutex.WaitOne();

            if(!CheckIfClientProfileExists(ClientProfileID))
            {
                profileExists = false;
                mutex.ReleaseMutex();
                return profileExists;
            }

            try
            {
                Client_Profiles ClientProfileEntity = db.Client_Profiles.FirstOrDefault(n => n.Client_ID == ClientProfileID);
                db.Client_Profiles.Remove(ClientProfileEntity);
                db.SaveChanges();
                mutex.ReleaseMutex();
                return true;
            }
            catch(Exception e)
            {
                mutex.ReleaseMutex();
                return false;
            }
        }

        private static bool CheckIfClientProfileExists(DataModels.ClientProfile ClientProfileModel)
        {
            return CheckIfClientProfileExists(ClientProfileModel.ClientID);
        }

        private static bool CheckIfClientProfileExists(int ClientProfileID)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            return db.Client_Profiles.Any(n => n.Client_ID == ClientProfileID);
        }
    }
}