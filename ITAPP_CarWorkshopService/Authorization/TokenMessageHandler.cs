using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using ITAPP_CarWorkshopService.Authorization;

namespace ITAPP_CarWorkshopService.Authorization
{
    public class TokenMessageHandler : DelegatingHandler
    {
        private const string AdminApiKey = "ITAPP";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            bool validKey = false;
            IEnumerable<string> requestHeaders;
            bool checkApiKeyExists = httpRequestMessage.Headers.TryGetValues("Token", out requestHeaders);
            if(checkApiKeyExists)
            {
                string tokenString = requestHeaders.FirstOrDefault();

                if(Token.ValidateToken(tokenString))
                {
                    validKey = true;
                }
            }

            if(!validKey)
            {
                return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Invalid Access Token");
            }

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);

            return response; 
        }
    }
}