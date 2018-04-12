using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class CarProfileModel
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public string CarModel { get; set; }
        public string CarVINNumber { get; set; }
        public int CarProductionYear { get; set; }
        public int CarFirstRegistrationYear { get; set; }

        public CarProfileModel()
        {
            CarID = -1;
            BrandID = -1;
            CarModel = "default";
            CarVINNumber = "0000000000";
            CarProductionYear = 1900;
            CarFirstRegistrationYear = 1900;
        }

        public CarProfileModel(ITAPP_CarWorkshopService.Car_Profiles CarProfileEntity)
        {
            MakeCarProfileModelFromCarProfileEntity(CarProfileEntity);
        }

        public void MakeCarProfileModelFromCarProfileEntity(ITAPP_CarWorkshopService.Car_Profiles CarProfileEntity)
        {
            CarID = CarProfileEntity.Car_ID;
            BrandID = (int)CarProfileEntity.Brand_ID;
            CarModel = CarProfileEntity.Car_model;
            CarVINNumber = CarProfileEntity.Car_VIN_number;
            CarProductionYear = CarProfileEntity.Car_production_year;
            CarFirstRegistrationYear = (int)CarProfileEntity.Car_first_registration_year;
        }

        public ITAPP_CarWorkshopService.Car_Profiles MakeCarProfileEntityFromCarProfileModel()
        {
            var CarProfileEntity = new ITAPP_CarWorkshopService.Car_Profiles()
            {
                Car_ID = CarID,
                Brand_ID = BrandID,
                Car_model = CarModel,
                Car_VIN_number = CarVINNumber,
                Car_production_year = CarProductionYear,
                Car_first_registration_year = CarFirstRegistrationYear
            };

            return CarProfileEntity;
        }

        public static List<DataModels.CarProfileModel> ListOfEntityToListOfModels(List<ITAPP_CarWorkshopService.Car_Profiles> ListOfEntities)
        {
            var ListOfModels = new List<DataModels.CarProfileModel>();

            foreach (var item in ListOfEntities)
            {
                ListOfModels.Add(new DataModels.CarProfileModel(item));
            }

            return ListOfModels;
        }
    }
}