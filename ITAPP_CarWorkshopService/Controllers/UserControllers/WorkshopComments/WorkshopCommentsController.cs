using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using System.Web.Http;

namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopComments
{
    public class WorkshopCommentsController : ApiController
    {
        [HttpGet]
        [Route ("comments/bycommentid/{ID}")]
        public Workshop_Comments Get_comment([FromUri] int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Comments.FirstOrDefault(p => p.Comment_ID == ID);
            }
        }
        [HttpGet]
        [Route("comments/byclientid/{ID}")]
        public IEnumerable<Workshop_Comments> Get_all_comments_from_Client_ID([FromUri] int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Comments.Where(p => p.Client_ID == ID);
            }
        }
        [HttpGet]
        [Route("comments/byworkshopid/{ID}")]
        public IEnumerable<Workshop_Comments> Get_all_comments_from_Workshop_ID([FromUri] int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Comments.Where(p => p.Workshop_ID == ID);
            }
        }
        [HttpPost]
        public Response_String Add_Comment([FromBody] Workshop_Comments New_Comment)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                db.Workshop_Comments.Add(New_Comment);
                db.SaveChanges();
                return new Response_String() { Response = "Item was added" }; 
            }
        }
        [HttpPut]
        public Response_String Modify_Comment([FromBody] Workshop_Comments Modify_Comment)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Comments.FirstOrDefault(p => Modify_Comment.Comment_ID == p.Comment_ID);
                if (Old != null)
                {
                    var ID = Old.Comment_ID;
                    Old = Modify_Comment;
                    Old.Comment_ID = ID;
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was modify" };
                }
                return new Response_String() { Response = "Item does not exsist" };
            }
        }
        [HttpDelete]
        public Response_String Delete_Comment(int ID)
        {
            using(var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                var Old = db.Workshop_Comments.Remove(db.Workshop_Comments.FirstOrDefault(p => p.Comment_ID == ID));
                if(Old != null)
                {
                    db.SaveChanges();
                    return new Response_String() { Response = "Item was removed" };
                }
                return new Response_String() { Response = "Item does not exsist" };
            }
        }
    }
}
