using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ITAPP_CarWorkshopService.AdditionalModels;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class WorkshopProfileManager
    {
        private static Mutex mutex = new Mutex();

        public static List<CityAndZipCode> GetAllCitiesAndZipCodes()
        {
            List<CityAndZipCode> ListOfCitiesAndZipCodes = new List<CityAndZipCode>();

            mutex.WaitOne();

            var db = new ITAPPCarWorkshopServiceDBEntities();

            var ListOfWorkshopProfiles = db.Workshop_Profiles.ToList();

            mutex.ReleaseMutex();

            foreach (var item in ListOfWorkshopProfiles)
            {
                ListOfCitiesAndZipCodes.Add(new CityAndZipCode(item.Workshop_address_city, item.Workshop_address_zip_code));
            }

            ListOfCitiesAndZipCodes = ListOfCitiesAndZipCodes.Distinct().ToList();

            return ListOfCitiesAndZipCodes;
        }

        /// <summary>
        /// Method to get workshop profile by its ID
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns>Return workshop profile od specified ID</returns>
        /// <remarks>Throws an exception if does not find any profile with specified ID</remarks>
        public static Workshop_Profiles GetWorkshopProfileById(int workshopId)
        {
            mutex.WaitOne();

            if (!CheckIfWorkshopProfileExists(workshopId))
            {
                mutex.ReleaseMutex();
                throw NoWorkshopOfGivenId(workshopId);
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            Workshop_Profiles workshop = db.Workshop_Profiles.FirstOrDefault(n => n.Workshop_ID == workshopId);

            CountAverageRating(workshop);

            db.SaveChanges();

            mutex.ReleaseMutex();

            return workshop;
        }

        public static List<Workshop_Profiles> GetWorkshopProfilesByCity(string city)
        {
            city = StringAdjustment.RemoveSpaces(city);
            city = StringAdjustment.MakeFirstLetterUppercaseTheRestLowercase(city);

            mutex.WaitOne();

            if (!CheckIfWorkshopProfileExistsByCity(city))
            {
                mutex.ReleaseMutex();
                throw NoWorkshopOfGivenCity(city);
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            var list = db.Workshop_Profiles.Where(n => n.Workshop_address_city.Equals(city)).ToList();

            foreach (var item in list)
            {
                CountAverageRating(item);
            }

            db.SaveChanges();

            mutex.ReleaseMutex();

            return list;
        }

        public static List<Workshop_Profiles> GetWorkshopProfilesByName(string name)
        {
            mutex.WaitOne();

            if (!CheckIfWorkshopProfileExistsByName(name))
            {
                mutex.ReleaseMutex();
                throw NoWorkshopOfGivenName(name);
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();
            var list = db.Workshop_Profiles.Where(n => n.Workshop_name.Equals(name)).ToList();

            foreach (var item in list)
            {
                CountAverageRating(item);
            }

            db.SaveChanges();

            mutex.ReleaseMutex();

            return list;
        }

        private static double CountAverageRating(Workshop_Profiles workshop) => 0.0d;

        private static bool CheckIfWorkshopProfileExists(int workshopId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_ID == workshopId);
        }

        private static bool CheckIfWorkshopProfileExistsByNIP(string NIP)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_NIP.Equals(NIP));
        }

        private static bool CheckIfWorkshopProfileExistsByCity(string city)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_address_city.Equals(city));
        }

        private static bool CheckIfWorkshopProfileExistsByName(string name)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_name.Equals(name));
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

        private static Exception NoWorkshopOfGivenCity(string city)
        {
            string exceptionMessage;
            exceptionMessage = "There are no any workshop in city: ";
            exceptionMessage += city;
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }

        private static Exception NoWorkshopOfGivenName(string name)
        {
            string exceptionMessage;
            exceptionMessage = "There are no any workshop of given name: ";
            exceptionMessage += name;
            Exception exception = new Exception(exceptionMessage);
            return exception;
        }
    }
}