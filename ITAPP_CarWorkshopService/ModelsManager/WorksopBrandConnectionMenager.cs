using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;

namespace ITAPP_CarWorkshopService.ModelsManager
{
    public class WorksopBrandConnectionMenager
    {
        private static Mutex mutex = new Mutex();
        static public List<DataModels.WorkshopBrandConnectionsModels> GetAllConnections()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            mutex.WaitOne();
            List<DataModels.WorkshopBrandConnectionsModels> List = new List<DataModels.WorkshopBrandConnectionsModels>();
            foreach(ITAPP_CarWorkshopService.Workshop_Brand_Connections data in db.Workshop_Brand_Connections)
            {
                List.Add(new DataModels.WorkshopBrandConnectionsModels(data));
            }
            mutex.ReleaseMutex();
            return List;
        }
        static public HttpResponseMessage AddNewWorkshopConnectionBrand(DataModels.WorkshopBrandConnectionsModels newConnection)
        {
            var Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            Response.Content = new StringContent("Conection already exists");
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var WorkshopConnectionBrand = db.Workshop_Brand_Connections.FirstOrDefault(connection => connection.Workshop_ID == newConnection.WorkshopID && connection.Car_brand_ID == newConnection.CarbrandID);
            if(WorkshopConnectionBrand != null)
            {
                return Response;
            }
            mutex.WaitOne();
            db.Workshop_Brand_Connections.Add(newConnection.MakWorksopBrandConnectionEntityFroWorksopBrandConnectioneModel());
            db.SaveChanges();
            mutex.ReleaseMutex();
            Response = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
            Response.Content = new StringContent("Connection added");
            return Response;
        }
        static public HttpResponseMessage ModifyWorkshopConnectionBrand(DataModels.WorkshopBrandConnectionsModels NewConnetion)
        {
            var Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            Response.Content = new StringContent("Conection does not exists");
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var WorkshopConnectionBrand = db.Workshop_Brand_Connections.First(connection => connection.WBC_ID == NewConnetion.WBCID);
            if (WorkshopConnectionBrand != null)
            {

                mutex.WaitOne();
                WorkshopConnectionBrand = NewConnetion.MakWorksopBrandConnectionEntityFroWorksopBrandConnectioneModel();
                db.SaveChanges();
                mutex.ReleaseMutex();
                Response = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
                Response.Content = new StringContent("Connection added");
                return Response;
            }
            return Response;
        }
        static public HttpResponseMessage DeleteWorkshopConnectionBrand(int ID)
        {

            var Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            Response.Content = new StringContent("Conection does not exists");
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var WorkshopConnectionBrand = db.Workshop_Brand_Connections.First(connection => connection.WBC_ID == ID);
            if (WorkshopConnectionBrand != null)
            {

                mutex.WaitOne();
                db.Workshop_Brand_Connections.Remove(WorkshopConnectionBrand);
                db.SaveChanges();
                mutex.ReleaseMutex();
                Response = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
                Response.Content = new StringContent("Connection deleted");
                return Response;
            }
            return Response;
        }
    }
}