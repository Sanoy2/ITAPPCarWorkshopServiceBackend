using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public class CarServiceMenager
    {
        private static Mutex mutex = new Mutex();
        static public List<DataModels.CarServiceModel> GetListOfCarService()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            List<DataModels.CarServiceModel> returnList;
            mutex.WaitOne();
            returnList = DataModels.CarServiceModel.ListOfEntityToListOfModels(db.Car_Services.ToList());
            mutex.ReleaseMutex();
            return returnList;
        }
        static public DataModels.CarServiceModel GetListByID(int ID)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            Car_Services returnValue;
            mutex.WaitOne();
            returnValue = db.Car_Services.First(service => service.Service_ID == ID);
            mutex.ReleaseMutex();
            return new DataModels.CarServiceModel(returnValue);
        }
        static public HttpResponseMessage AddCarService(DataModels.CarServiceModel NewService)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var returnValue = new HttpResponseMessage(HttpStatusCode.Forbidden);
            returnValue.Content = new StringContent("Service with this name is already exists");
            var CarExists = db.Car_Services.FirstOrDefault(car => car.Service_name == NewService.ServiceName);
            if (CarExists != null)
            {
                return returnValue;
            }
            mutex.WaitOne();
            db.Car_Services.Add(NewService.MakeEntityFromModel());
            db.SaveChanges();
            mutex.ReleaseMutex();
            returnValue = new HttpResponseMessage(HttpStatusCode.OK);
            returnValue.Content = new StringContent("Service was added");
            return returnValue;
        }
        static public HttpResponseMessage ModifyCarService(DataModels.CarServiceModel ModifyService)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var returnValue = new HttpResponseMessage(HttpStatusCode.Forbidden);
            returnValue.Content = new StringContent("Service with this name not exsists");
            var OldCar = db.Car_Services.FirstOrDefault(car => car.Service_ID == ModifyService.ServiceID);
            if (OldCar != null)
            {
                var ID = OldCar.Service_ID;
                OldCar = ModifyService.MakeEntityFromModel();
                OldCar.Service_ID = ID;
                mutex.WaitOne();
                db.SaveChanges();
                mutex.ReleaseMutex();
                returnValue = new HttpResponseMessage(HttpStatusCode.Accepted);
                returnValue.Content = new StringContent("Service was modifiy");
                return returnValue;
            }
            return returnValue;
        }
        static public HttpResponseMessage DeleteCarService(int ID)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var returnValue = new HttpResponseMessage(HttpStatusCode.Forbidden);
            returnValue.Content = new StringContent("Service with this name not exsists");
            var Car = db.Car_Services.FirstOrDefault(car => car.Service_ID == ID);
            if (Car != null)
            {
                returnValue = new HttpResponseMessage(HttpStatusCode.Accepted);
                returnValue.Content = new StringContent("Service was deleted");
                mutex.WaitOne();
                db.Car_Services.Remove(Car);
                db.SaveChanges();
                mutex.ReleaseMutex();
                return returnValue;
            }
            return returnValue;
        }
    }
}