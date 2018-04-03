using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;


namespace ITAPP_CarWorkshopService.ModelsManager
{
    public static class WorkshopCommentManager
    {
        private static Mutex mutex = new Mutex();

        public static double GetAvarageRating(Workshop_Profiles workshop)
        {
            mutex.WaitOne();
            double result = GetAvarageRatingPrivate(workshop.Workshop_ID);
            mutex.ReleaseMutex();

            return result;
        }

        public static double GetAvarageRating(int workshopId)
        {
            mutex.WaitOne();
            double result = GetAvarageRatingPrivate(workshopId);
            mutex.ReleaseMutex();

            return result;
        }

        private static double GetAvarageRatingPrivate(int workshopId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            if(CheckIfCommentExists(workshopId))
            {
                var list = db.Workshop_Comments.Where(n => n.Workshop_ID == workshopId).ToList();
                double result = list.Average(n => n.Comment_rating);
                return result;
            }
            else
            {
                return 0.0;
            }
        }

        private static bool CheckIfCommentExists(int workshopId)
        {
            var db = new ITAPPCarWorkshopServiceDBEntities();
            return db.Workshop_Comments.Any(n => n.Workshop_ID == workshopId);
        }
    }
}