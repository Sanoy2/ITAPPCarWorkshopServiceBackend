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

        public static List<Workshop_Comments> GetAllComments()
        {
            mutex.WaitOne();
            var db = new ITAPPCarWorkshopServiceDBEntities();

            List<Workshop_Comments> list = null;
            list = db.Workshop_Comments.OrderByDescending(n => n.Comment_date).ToList();

            mutex.ReleaseMutex();
            return list;
        }

        public static List<Workshop_Comments> GetComments(int workshopId)
        {
            mutex.WaitOne();
            var db = new ITAPPCarWorkshopServiceDBEntities();

            List<Workshop_Comments> list = null;
            list = db.Workshop_Comments.Where(n => n.Workshop_ID == workshopId).OrderByDescending(n => n.Comment_date).ToList();

            mutex.ReleaseMutex();
            return list;
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