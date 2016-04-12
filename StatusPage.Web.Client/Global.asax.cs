using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace StatusPage.Web.Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
