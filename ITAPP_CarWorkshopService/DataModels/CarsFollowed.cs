using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class CarsFollowed
    {
        public int CarFollowID { get; set; }
        public Nullable<int> CarProfileID { get; set; }
        public Nullable<int> ClientID { get; set; }

        public CarsFollowed()
        {
            CarFollowID = -1;
            CarProfileID = null;
            ClientID = null;
        }
        public CarsFollowed(ITAPP_CarWorkshopService.Cars_followed entityCarsFollowed)
        {
            MakeUserFromEntity(entityCarsFollowed);
        }
        public void MakeUserFromEntity(ITAPP_CarWorkshopService.Cars_followed entityCarsFollowed)
        {
            CarFollowID = entityCarsFollowed.Car_follow_ID;
            CarProfileID = entityCarsFollowed.Car_profile_ID;
            ClientID = entityCarsFollowed.Client_ID;
        }
    }
}