using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ITAPP_CarWorkshopService.AdditionalModels;
using System.Net;
using System.Net.Http;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class WorkshopProfileManager
    {
        private static Mutex mutex = new Mutex();

        private static int minimumCityLength = 3;
        private static int minimumNameLength = 3;

        public static HttpResponseMessage CreateNewWorkshopProfile(DataModels.WorkshopProfileModel WorkshopProfileModel, int UserID)
        {
            
            mutex.WaitOne();

            if(CheckIfWorkshopProfileExistsByNIP(WorkshopProfileModel.WorkshopNIP))
            {
                mutex.ReleaseMutex();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("Workshop of given NIP already exists.");

                return response;
            }

            var db = new ITAPPCarWorkshopServiceDBEntities();

            ITAPP_CarWorkshopService.Workshop_Profiles WorkshopProfileEntity = WorkshopProfileModel.MakeWorkshopProfileEntityFromWorkshopProfileModel();

            try
            {
                db.Workshop_Profiles.Add(WorkshopProfileEntity);
                db.SaveChanges();

                var NewWorkshopEmployee = new Workshop_Employees()
                {
                    Workshop_empoyee_ID = UserID,
                    Workshop_ID = WorkshopProfileEntity.Workshop_ID
                };

                db.Workshop_Employees.Add(NewWorkshopEmployee);
                db.SaveChanges();

                mutex.ReleaseMutex();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Workshop profile was succesfully created");

                return response;
            }
            catch(Exception e)
            {
                mutex.ReleaseMutex();
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("Something gone wrong while adding the workshop profile to DB");

                return response;
            }
        }

        public static List<DataModels.WorkshopProfileModel> GetWorkshopsByCityAndName(string city, string name)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            city = PrepareToCompare(city);
            name = PrepareToCompare(name);
            mutex.WaitOne();
            if(city.Length >= minimumCityLength && name.Length >= minimumNameLength)
            {
                var list = db.Workshop_Profiles.Where(n => n.Workshop_address_city.Replace(" ", string.Empty).ToLower().Equals(city) && n.Workshop_name.ToLower().Replace(" ", string.Empty).Contains(name)).ToList();
                mutex.ReleaseMutex();
                var ListOfModels = DataModels.WorkshopProfileModel.MakeModelsListFromEntitiesList(list);
                foreach(DataModels.WorkshopProfileModel Connection in ListOfModels)
                {
                    foreach(Workshop_Brand_Connections conn in db.Workshop_Brand_Connections.Where(con => con.Workshop_ID == Connection.WorkshopID))
                    {
                        Connection.BrandsList.Add(new DataModels.CarBrandModel(db.Car_Brands.FirstOrDefault(Brand => Brand.Brand_ID == conn.Car_brand_ID)));
                    }
                }
                return ListOfModels;
            }
            if (city.Length >= minimumCityLength)
            {
                var list = db.Workshop_Profiles.Where(n => n.Workshop_address_city.ToLower().Replace(" ", string.Empty).Equals(city)).ToList();
                mutex.ReleaseMutex();
                var ListOfModels = DataModels.WorkshopProfileModel.MakeModelsListFromEntitiesList(list);
                foreach (DataModels.WorkshopProfileModel Connection in ListOfModels)
                {
                    foreach (Workshop_Brand_Connections conn in db.Workshop_Brand_Connections.Where(con => con.Workshop_ID == Connection.WorkshopID))
                    {
                        Connection.BrandsList.Add(new DataModels.CarBrandModel(db.Car_Brands.FirstOrDefault(Brand => Brand.Brand_ID == conn.Car_brand_ID)));
                    }
                }
                return ListOfModels;
            }
            if (name.Length >= minimumNameLength)
            {
                var list = db.Workshop_Profiles.Where(n => n.Workshop_name.ToLower().Replace(" ", string.Empty).Contains(name)).ToList();
                mutex.ReleaseMutex();
                var ListOfModels = DataModels.WorkshopProfileModel.MakeModelsListFromEntitiesList(list);
                foreach (DataModels.WorkshopProfileModel Connection in ListOfModels)
                {
                    foreach (Workshop_Brand_Connections conn in db.Workshop_Brand_Connections.Where(con => con.Workshop_ID == Connection.WorkshopID))
                    {
                        Connection.BrandsList.Add(new DataModels.CarBrandModel(db.Car_Brands.FirstOrDefault(Brand => Brand.Brand_ID == conn.Car_brand_ID)));
                    }
                }
                return ListOfModels;
            }
            mutex.ReleaseMutex();
            return new List<DataModels.WorkshopProfileModel>();
        }

        private static bool CheckIfCityFitPrecisely(string workshopCity, string city)
        {
            workshopCity = PrepareToCompare(workshopCity);
            city = PrepareToCompare(city);

            return workshopCity.Equals(city);
        }

        private static bool CheckIfNameFitPrecisely(string workshopName, string name)
        {
            workshopName = PrepareToCompare(workshopName);
            name = PrepareToCompare(name);

            if (workshopName.Equals(name) || workshopName.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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


        private static bool CheckIfWorkshopProfileExists(int workshopId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_ID == workshopId);
        }

        private static bool CheckIfWorkshopProfileExistsByNIP(string NIP)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            NIP = PrepareToCompare(NIP);

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_NIP.Equals(NIP));
        }

        private static bool CheckIfWorkshopProfileExistsByCity(string city)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            city = PrepareToCompare(city);

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_address_city.ToLower().Equals(city));
        }

        private static bool CheckIfWorkshopProfileExistsByName(string name)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            name = PrepareToCompare(name);

            return db.Workshop_Profiles.Any(workshop => workshop.Workshop_name.ToLower().Equals(name));
        }

        private static string PrepareToCompare(string text)
        {
            text = StringAdjustment.RemoveSpaces(text);
            text = text.ToLower();
            return text;
        }
    }

}