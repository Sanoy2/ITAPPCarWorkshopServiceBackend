using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class CarServiceModel
    {
        public int ServiceID { get; set; }
        public Nullable<int> WorkshopID { get; set; }
        public Nullable<int> CarID { get; set; }
        public int Mileage { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public System.DateTime ServiceDate { get; set; }

        public CarServiceModel()
        {
            this.CarID = null;
            this.WorkshopID = null;
            this.ServiceID = -1;
            this.ServiceName = "Service Name";
            this.ServiceDescription = "Service Description";
            this.ServiceDate = System.DateTime.Now;
        }

        public CarServiceModel(ITAPP_CarWorkshopService.Car_Services entityService)
        {
            MakeCarServiceFromEntity(entityService);
        }

        public void MakeCarServiceFromEntity(ITAPP_CarWorkshopService.Car_Services entityProfile)
        {
            CarID = entityProfile.Car_ID;
            Mileage = entityProfile.Mileage;
            ServiceDate = entityProfile.Service_date;
            ServiceDescription = entityProfile.Service_description;
            ServiceID = entityProfile.Service_ID;
            WorkshopID = entityProfile.Workshop_ID;
        }

        public Car_Services MakeEntityFromModel()
        {
            var entityService = new Car_Services()
            {
                Car_ID = CarID,
                Mileage = Mileage,
                Service_date = ServiceDate,
                Service_description= ServiceDescription,
                Service_ID = ServiceID,
                Service_name = ServiceName,
                Workshop_ID = WorkshopID
            };

            return entityService;
        }
        public static List<DataModels.CarServiceModel> ListOfEntityToListOfModels(List<ITAPP_CarWorkshopService.Car_Services> ListOfEntities)
        {
            var ListOfModels = new List<DataModels.CarServiceModel>();

            foreach (var item in ListOfEntities)
            {
                ListOfModels.Add(new DataModels.CarServiceModel(item));
            }

            return ListOfModels;
        }
    }
}