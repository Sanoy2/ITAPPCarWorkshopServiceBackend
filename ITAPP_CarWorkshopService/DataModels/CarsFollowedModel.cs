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
            CarFollowID = CarsFollowedEntity.Car_follow_ID;
            CarProfileID = (int)CarsFollowedEntity.Car_profile_ID;
            ClientProfileID = (int)CarsFollowedEntity.Client_ID;
        }

        public ITAPP_CarWorkshopService.Cars_followed MakeCarsFollowedEntityFromCarsFollowedModel()
        {
            var CarsFollowedEntity = new ITAPP_CarWorkshopService.Cars_followed()
            {
                Car_follow_ID = CarFollowID,
                Car_profile_ID = CarProfileID,
                Client_ID = ClientProfileID
            };

            return CarsFollowedEntity;
        }
    }
}