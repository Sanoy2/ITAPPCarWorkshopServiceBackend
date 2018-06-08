using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class CarsFollowedModel
    {
        public int CarFollowID { get; set; }
        public int CarProfileID { get; set; }
        public int ClientProfileID { get; set; }

        public CarsFollowedModel()
        {
            CarFollowID = -1;
            CarProfileID = -1;
            ClientProfileID = -1;
        }

        public CarsFollowedModel(ITAPP_CarWorkshopService.Cars_followed CarsFollowedEntity)
        {
            MakeCarsFollowedModelFromCarsFollowedEntity(CarsFollowedEntity);
        }

        public void MakeCarsFollowedModelFromCarsFollowedEntity(ITAPP_CarWorkshopService.Cars_followed CarsFollowedEntity)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();

            CarFollowID = CarsFollowedEntity.Car_follow_ID;
            CarProfileID = (int)CarsFollowedEntity.Car_profile_ID;
            ClientProfileID = (int)db.Client_Profiles.First(n => n.Client_ID == CarsFollowedEntity.Client_ID).User_ID;
        }

        public ITAPP_CarWorkshopService.Cars_followed MakeCarsFollowedEntityFromCarsFollowedModel()
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            var CarsFollowedEntity = new ITAPP_CarWorkshopService.Cars_followed()
            {
                Car_follow_ID = CarFollowID,
                Car_profile_ID = CarProfileID,
                //Client_ID = ClientProfileID // tu musi być id profilu klienta, nie usera
            };

            int userId = 0;
            userId = db.Client_Profiles.First(n => n.User_ID == ClientProfileID).Client_ID;

            CarsFollowedEntity.Client_ID = userId;

            return CarsFollowedEntity;
        }

        public static List<DataModels.CarsFollowedModel> ListOfEntitiesToListOfModels(List<ITAPP_CarWorkshopService.Cars_followed> ListOfEntities)
        {
            var ListOfModels = new List<DataModels.CarsFollowedModel>();

            foreach (var item in ListOfEntities)
            {
                ListOfModels.Add(new DataModels.CarsFollowedModel(item));
            }

            return ListOfModels;
        }
    }
}