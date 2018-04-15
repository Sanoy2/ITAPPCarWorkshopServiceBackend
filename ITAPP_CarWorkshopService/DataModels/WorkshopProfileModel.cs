using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class WorkshopProfileModel
    {
        public int WorkshopID { get; set; }
        public string WorkshopNIP { get; set; }
        public string WorkshopAddressCity { get; set; }
        public string WorkshopAddressStreet { get; set; }
        public string WorkshopAddressZipCode { get; set; }
        public string WorkshopDescription { get; set; }
        public string WorkshopEmailAddress { get; set; }
        public string WorkshopPhoneNumber { get; set; }
        public string WorkshopURL { get; set; }
        public string WorkshopLogoURL { get; set; }
        public double WorkshopAverageRating { get; set; }
        public string WorkshopName { get; set; }
        public List<DataModels.CarBrandModel> BrandsList { get; set; } 

        public WorkshopProfileModel()
        {
            WorkshopID = -1;
            WorkshopName = "default name";
            WorkshopNIP = "0000000000";
            WorkshopAddressCity = "default city";
            WorkshopAddressStreet = "default street";
            WorkshopAddressZipCode = "00-000";
            WorkshopDescription = "default description";
            WorkshopEmailAddress = "default@default.com";
            WorkshopPhoneNumber = "000000000";
            WorkshopURL = "default.com";
            WorkshopLogoURL = "default.com/img";
            WorkshopAverageRating = 0.0;
        }

        public WorkshopProfileModel(ITAPP_CarWorkshopService.Workshop_Profiles WorkshopProfileEntity)
        {
            MakeWorkshopProfileModelFromWorkshopProfileEntity(WorkshopProfileEntity);
        }

        public void MakeWorkshopProfileModelFromWorkshopProfileEntity(ITAPP_CarWorkshopService.Workshop_Profiles WorkshopProfileEntity)
        {
            WorkshopID = WorkshopProfileEntity.Workshop_ID;
            WorkshopName = WorkshopProfileEntity.Workshop_name;
            WorkshopNIP = WorkshopProfileEntity.Workshop_NIP;
            WorkshopAddressCity = WorkshopProfileEntity.Workshop_address_city;
            WorkshopAddressStreet = WorkshopProfileEntity.Workshop_address_streer;
            WorkshopAddressZipCode = WorkshopProfileEntity.Workshop_address_zip_code;
            WorkshopDescription = WorkshopProfileEntity.Workshop_description;
            WorkshopEmailAddress = WorkshopProfileEntity.Workshop_email_address;
            WorkshopPhoneNumber = WorkshopProfileEntity.Workshop_phone_number;
            WorkshopURL = WorkshopProfileEntity.Workshop_URL;
            WorkshopLogoURL = WorkshopProfileEntity.Workshop_logo_URL;
            WorkshopAverageRating = (double)WorkshopProfileEntity.Workshop_average_rating;
        }

        public ITAPP_CarWorkshopService.Workshop_Profiles MakeWorkshopProfileEntityFromWorkshopProfileModel()
        {
            ITAPP_CarWorkshopService.Workshop_Profiles WorkshopProfileEntity = new ITAPP_CarWorkshopService.Workshop_Profiles()
            {
                Workshop_ID = WorkshopID,
                Workshop_name = WorkshopName,
                Workshop_NIP = WorkshopNIP,
                Workshop_address_city = WorkshopAddressCity,
                Workshop_address_streer = WorkshopAddressStreet,
                Workshop_address_zip_code = WorkshopAddressZipCode,
                Workshop_description = WorkshopDescription,
                Workshop_email_address = WorkshopEmailAddress,
                Workshop_phone_number = WorkshopPhoneNumber,
                Workshop_URL = WorkshopURL,
                Workshop_logo_URL = WorkshopLogoURL,
                Workshop_average_rating = WorkshopAverageRating
            };

            return WorkshopProfileEntity;
        }

        public static List<DataModels.WorkshopProfileModel> MakeModelsListFromEntitiesList(List<ITAPP_CarWorkshopService.Workshop_Profiles> ListOfEntities)
        {
            var ListOfModels = new List<DataModels.WorkshopProfileModel>();
            foreach (var item in ListOfEntities)
            {
                ListOfModels.Add(new DataModels.WorkshopProfileModel(item));
            }

            return ListOfModels;
        }
    }
}