using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class WorkshopProfileManager
    {
        private static Mutex mutex = new Mutex();

        /// <summary>
        /// Method to get workshop profile by its ID
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns>Return workshop profile od specified ID</returns>
        /// <remarks>Throws an exception if does not find any profile with specified ID</remarks>
        public static Workshop_Profiles GetWorkshopProfile(int workshopId)
        {
            mutex.WaitOne();

            if (!CheckIfWorkshopProfileExists(workshopId))
            {
                mutex.ReleaseMutex();
                throw NoWorkshopOfGivenId(workshopId);
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            Workshop_Profiles workshop = db.Workshop_Profiles.FirstOrDefault(n => n.Workshop_ID == workshopId);
            mutex.ReleaseMutex();

            return workshop;
        }

        private static bool CheckIfWorkshopProfileExists(int workshopId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_ID == workshopId);
        }

        private static bool CheckIfWorkshopProfileExists(string NIP)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_NIP == NIP);
        }

        private static Exception NoWorkshopOfGivenId(int workshopId)
        {
            string exceptionMessage;
            exceptionMessage = "Workshop with id: ";
            exceptionMessage += workshopId;
            exceptionMessage += " does not exists.";
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }
    }
}