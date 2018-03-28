using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ITAPP_CarWorkshopService.ResonseClass;
using System.Web.Http;
/// <summary>
/// Controller
/// </summary>
namespace ITAPP_CarWorkshopService.Controllers.UserControllers.WorkshopComments
{
    /// <summary>
    /// Workshop Comments Controller
    /// </summary>
    public class WorkshopCommentsController : ApiController
    {
        /// <summary>
        /// GET method &#xD;
        /// URL = http://itappcarworkshopservice.azurewebsites.net/api/comments/bycommentid/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Comment_ID</param>
        /// <returns>Returns comment with specyfic ID or null if there is no comment</returns>
        [HttpGet]
        [Route ("comments/bycommentid/{ID}")]
        public Workshop_Comments Get_comment([FromUri] int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Comments.FirstOrDefault(p => p.Comment_ID == ID);
            }
        }
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/comments/byclientid/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Client_ID </param>
        /// <returns>Returns list of client comment's with specyfic ID or null if there is no comment</returns>
        [HttpGet]
        [Route("comments/byclientid/{ID}")]
        public IEnumerable<Workshop_Comments> Get_all_comments_from_Client_ID([FromUri] int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Comments.Where(p => p.Client_ID == ID);
            }
        }
        /// <summary>
        /// GET method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/comments/byworkshopid/ + ID &#xD;
        /// </summary>
        /// <param name="ID">Workshop_ID</param>
        /// <returns>Returns list of workshop comment's with specyfic ID or null if there is no comment</returns>
        [HttpGet]
        [Route("comments/byworkshopid/{ID}")]
        public IEnumerable<Workshop_Comments> Get_all_comments_from_Workshop_ID([FromUri] int ID)
        {
            using (var db = new ITAPPCarWorkshopServiceDBEntities())
            {
                return db.Workshop_Comments.Where(p => p.Workshop_ID == ID);
            }
        }
        /// <summary>
        /// POST method &#xD; 
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/comments &#xD;
        /// Comment_ID is automaticly incremented so as a Comment_ID = ""
        /// </summary>
        /// <param name="New_Comment">        
        /// {
        ///     Client_ID =, &#xD;
        ///     Comment_date =, &#xD;
        ///     Comment_description =, &#xD;
        ///     Comment_ID =, &#xD;
        ///     Comment_rating =, &#xD;
        ///     Workshop_ID =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was added"</returns>
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
        /// <summary>
        /// PUT method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/comments &#xD;
        /// Workshop_empoyee_ID should be passed by in body
        /// </summary>
        /// <param name="Modify_Comment">        
        /// {
        ///     Client_ID =, &#xD;
        ///     Comment_date =, &#xD;
        ///     Comment_description =, &#xD;
        ///     Comment_ID =, &#xD;
        ///     Comment_rating =, &#xD;
        ///     Workshop_ID =, &#xD;
        /// }</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was modify" or "Item does not exsists"</returns>
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
                return new Response_String() { Response = "Item does not exsists" };
            }
        }
        /// <summary>
        /// DELETE method &#xD;
        /// URL : http://itappcarworkshopservice.azurewebsites.net/api/comments + ID &#xD;
        /// </summary>
        /// <param name="ID">Comment_ID</param>
        /// <returns>Returns JSON with { Response : string }, string countains : "Item was removed" or "Item does not exsists"</returns>
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
