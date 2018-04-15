using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class WorkshopBrandConnectionsModels
    {
        public int WBCID { get; set; }
        public Nullable<int> WorkshopID { get; set; }
        public Nullable<int> CarbrandID { get; set; }
        public WorkshopBrandConnectionsModels()
        {
            WBCID = -1;
            WorkshopID = null;
            CarbrandID = null;
        }
        public WorkshopBrandConnectionsModels(Workshop_Brand_Connections newConnection)
        {
            WorksopBrandConnectionModelFromWorksopBrandConnectionEntity(newConnection);
        }
        public void WorksopBrandConnectionModelFromWorksopBrandConnectionEntity (Workshop_Brand_Connections newConnection)
        {
            WBCID = newConnection.WBC_ID;
            WorkshopID = newConnection.Workshop_ID;
            CarbrandID = newConnection.Car_brand_ID;
        }
        public ITAPP_CarWorkshopService.Workshop_Brand_Connections MakWorksopBrandConnectionEntityFroWorksopBrandConnectioneModel()
        {
            ITAPP_CarWorkshopService.Workshop_Brand_Connections WorksopBrandConnectionEntity = new ITAPP_CarWorkshopService.Workshop_Brand_Connections()
            {
                WBC_ID = WBCID,
                Car_brand_ID = CarbrandID,
                Workshop_ID = WorkshopID
            };

            return WorksopBrandConnectionEntity;
        }
    }
}