using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAPP_CarWorkshopService.DataModels
{
    public class ReservationModel
    {
        public int ReservationId { get; set; }
        public System.DateTime ReservationDateTime { get; set; }
        public string ReservationStatus { get; set; }
        public int WorkshopId { get; set; }
        public int UserId { get; set; }

        public ReservationModel()
        {
            ReservationId = -1;
            ReservationDateTime = DateTime.Now;
            ReservationStatus = "WAITING";
            WorkshopId = -1;
            UserId = -1;
        }

        public ReservationModel(ITAPP_CarWorkshopService.Reservation ReservationEntity)
        {
            MakeReservationModelFromReservationEntity(ReservationEntity);
        }

        public void MakeReservationModelFromReservationEntity(ITAPP_CarWorkshopService.Reservation ReservationEntity)
        {
            ReservationId = ReservationEntity.ReservationId;
            ReservationDateTime = ReservationEntity.ReservationDateTime;
            ReservationStatus = ReservationEntity.ReservationStatus;
            WorkshopId = (int) ReservationEntity.Workshop_ID;
            UserId = (int) ReservationEntity.User_ID;
        }

        public ITAPP_CarWorkshopService.Reservation MakeReservationEntityFromReservationModel()
        {
            var ReservationEntity = new ITAPP_CarWorkshopService.Reservation()
            {
                ReservationId = this.ReservationId,
                ReservationDateTime = this.ReservationDateTime,
                ReservationStatus = this.ReservationStatus,
                Workshop_ID = this.WorkshopId,
                User_ID = this.UserId
            };

            return ReservationEntity;
        }

        public static List<DataModels.ReservationModel> ListOfEntityToListOfModels(
            List<ITAPP_CarWorkshopService.Reservation> ListOfEntities)
        {
            var ListOfModels = new List<DataModels.ReservationModel>();

            foreach (var item in ListOfEntities)
            {
                ListOfModels.Add(new DataModels.ReservationModel(item));
            }

            return ListOfModels;
        }
    }

}