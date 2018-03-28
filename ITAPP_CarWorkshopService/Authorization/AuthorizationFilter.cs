using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ITAPP_CarWorkshopService.Authorization
{
    public class AuthorizationFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // TODO: REFACTOR THE CODE
            bool validKey = false;
            if(actionContext.Request.Headers == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                IEnumerable<string> requestHeaders;
                bool checkApiKeyExists = actionContext.Request.Headers.TryGetValues("Token", out requestHeaders);
                if (checkApiKeyExists)
                {
                    string tokenString = requestHeaders.FirstOrDefault();

                    if (Token.ValidateToken(tokenString))
                    {
                        validKey = true;
                    }
                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            
        }
    }
}