using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using ITAPP_CarWorkshopService.Authorization;
using System.Net.Http;
using System.Web.Http.Dispatcher;

namespace ITAPP_CarWorkshopService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Konfiguracja i usługi składnika Web API

            // Trasy składnika Web API
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // List of delegating handlers.
            DelegatingHandler[] handlers = new DelegatingHandler[]
            {
                new TokenMessageHandler()
            };

            // Create a message handler chain with an end-point.
            var routeHandlers = HttpClientFactory.CreatePipeline(
                new HttpControllerDispatcher(config), handlers);

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api2/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: routeHandlers
            );

            config.MessageHandlers.Add(new TokenMessageHandler()); 
        }
    }
}
