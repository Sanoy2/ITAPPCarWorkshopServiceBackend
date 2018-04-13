using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class ClientProfile
    {
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhoneNumber { get; set; }
        public List<CarProfileModel> CarsFollowedByClient { get; set; }
        
        public ClientProfile()
        {
            ClientID = -1;
            UserID = -1;
            ClientName = "default name";
            ClientSurname = "default surname";
            ClientPhoneNumber = "000000000";

        }

        public ClientProfile(ITAPP_CarWorkshopService.Client_Profiles entityProfile)
        {
            MakeClientProfileFromEntity(entityProfile);
        }

        public void MakeClientProfileFromEntity(ITAPP_CarWorkshopService.Client_Profiles entityProfile)
        {
            ClientID = entityProfile.Client_ID;
            UserID = (int)entityProfile.User_ID;
            ClientName = entityProfile.Client_name;
            ClientSurname = entityProfile.Client_surname;
            ClientPhoneNumber = entityProfile.Client_phone_number;
        }

        public Client_Profiles MakeEntityFromModel()
        {
            var entityProfile = new Client_Profiles()
            {
                Client_ID = ClientID,
                User_ID = UserID,
                Client_name = ClientName,
                Client_surname = ClientSurname,
                Client_phone_number = ClientPhoneNumber,
            };

            return entityProfile;
        }

        public static List<DataModels.ClientProfile> ListOfEntitiesToListOfModels(List<Client_Profiles> ListOfEntities)
        {
            var ListOfModels = new List<DataModels.ClientProfile>();

            foreach (var item in ListOfEntities)
            {
                ListOfModels.Add(new ClientProfile(item));
            }

            return ListOfModels;
        }
    }
}
