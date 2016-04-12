using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using StatusPage.Web.Security;

namespace StatusPage.Web.Client
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // api authentication
            config.SuppressHostPrincipal();
            config.Filters.Add(new ApiAuthenticationAttribute());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
