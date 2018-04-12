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

        public static ClientProfile GetClientProfileById(int id)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            ClientProfile clientProfile;

            mutex.WaitOne();

            var profile = db.Client_Profiles.FirstOrDefault(n => n.Client_ID == id);
            clientProfile = new ClientProfile()
            {
                Client_ID = profile.Client_ID,
                Client_name = profile.Client_name,
                Client_phone_number = profile.Client_phone_number,
                Client_surname = profile.Client_surname,
                User_ID = (int)profile.User_ID
            };
            mutex.ReleaseMutex();

            return clientProfile;
        }
    }
}