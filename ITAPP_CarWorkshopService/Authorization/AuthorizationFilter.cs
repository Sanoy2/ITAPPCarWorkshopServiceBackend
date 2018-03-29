using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net;

namespace ITAPP_CarWorkshopService.Authorization
{
    public class AuthorizationFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool tokenValidated = CheckIfTokenValidated(actionContext);

            if (!tokenValidated)
            {
                actionContext.Response = CreateResponseIfRequestUnauthorized();
            }
        }

        private static HttpResponseMessage CreateResponseIfRequestUnauthorized()
        {
            var message = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            message.Content = new StringContent("401 Unauthorized");

            return message;
        }

        private static bool CheckIfTokenValidated(HttpActionContext actionContext)
        {
            bool validKey = false;
            IEnumerable<string> requestHeaders;
            bool checkIfTokenExists = actionContext.Request.Headers.TryGetValues("Token", out requestHeaders);
            if (checkIfTokenExists)
            {
                string tokenString = requestHeaders.FirstOrDefault();

                if (Token.ValidateToken(tokenString))
                {
                    validKey = true;
                }
            }

            return validKey;
        }
    }
}