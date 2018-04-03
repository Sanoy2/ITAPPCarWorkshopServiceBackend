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

        private static readonly int minTimeBetweenCommentsInDays = 30;

        public static void AddComment(Workshop_Comments newComment)
        {
            // TODO:
            // check if the user has already added a comment of the workshop in last XXX hours/days/weeks/months - method - may throw exception
            // check if user exists - method from user manager - may throw exception
            // check if workshop exists - method from workshop profile manager - may throw exception
            // add a new comment to DB
            // return the result

            // remember about mutex
        }

        private static bool CheckIfCommentCanBeAdded(Workshop_Comments comment)
        {
            // TODO:
            // Check if there is any sense in here
            // I have no idea if it works correctly xD
            var mustBeOlderThat = DateTime.Now.AddDays(-minTimeBetweenCommentsInDays);

            var db = new ITAPPCarWorkshopServiceDBEntities();

            if(!db.Workshop_Comments.Any(n => n.Workshop_ID == comment.Workshop_ID && n.Client_ID == comment.Client_ID))
            {
                return true;
            }
           
            var list = db.Workshop_Comments.Where(n => n.Workshop_ID == comment.Workshop_ID && n.Client_ID == comment.Client_ID).OrderByDescending(n => n.Comment_date).ToList();

            var lastCommentDate = list.First().Comment_date;

            if(lastCommentDate < mustBeOlderThat)
            {
                return true;
            }

            return false;
        }

        public static void DeleteComment(int commentId)
        {
            // TODO:
            // check if a comment exists - method - may throw exception
            // delete a comment
            // return the result

            // remember about mutex
        }

        public static void EditComment(Workshop_Comments editedComment)
        {
            // TODO:
            // check if the comment exists - method - may throw exception
            // get the comment to be edited
            // edit the comment to be edited
            // save changes to DB
            // return the result
            
            // remember about mutex
        }

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