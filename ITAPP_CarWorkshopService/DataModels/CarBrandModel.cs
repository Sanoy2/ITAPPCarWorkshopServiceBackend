using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class CarBrandModel
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public CarBrandModel()
        {
            BrandID = -1;
            BrandName = "default";
        }

        public CarBrandModel(ITAPP_CarWorkshopService.Car_Brands CarBrandEntity)
        {
            MakeCarBrandModelFromCarBrandEntity(CarBrandEntity);
        }

        public void MakeCarBrandModelFromCarBrandEntity(ITAPP_CarWorkshopService.Car_Brands CarBrandEntity)
        {
            BrandID = CarBrandEntity.Brand_ID;
            BrandName = CarBrandEntity.Brand_Name;
        }

        public ITAPP_CarWorkshopService.Car_Brands MakeCarBrandEntityFromCarBrandModel()
        {
            var CarBrandEntity = new ITAPP_CarWorkshopService.Car_Brands()
            {
                Brand_ID = BrandID,
                Brand_Name = BrandName
            };

            return CarBrandEntity;
        }

        public List<DataModels.CarBrandModel> ListOfEntitiesToListOfModels(List<ITAPP_CarWorkshopService.Car_Brands> ListOfEntities)
        {
            var ListOfModels = new List<DataModels.CarBrandModel>();

            foreach (var item in ListOfEntities)
            {
                ListOfModels.Add(new DataModels.CarBrandModel(item));
            }

            return ListOfModels;
        }
    }
}