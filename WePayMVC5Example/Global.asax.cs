using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WePayMVC5Example.IoC;
using WePayMVC5HttpClientSDK;

namespace WePayMVC5Example
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //injects GlobalVaribles in to Controllers base class
            //see IoC folder
            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //applies WePay account information to SDK's WePayConfiguration class so that SDK methods can use.
            //you must set these in the MVC apps' Web.config (see this apps' web.config)
            WePayConfiguration.accessToken = ConfigurationManager.AppSettings["WepayAccessToken"];
            WePayConfiguration.accountId = Convert.ToInt32(ConfigurationManager.AppSettings["WepayAccountId"]);
            WePayConfiguration.clientId = Convert.ToInt32(ConfigurationManager.AppSettings["WepayClientId"]);
            WePayConfiguration.clientSecret = ConfigurationManager.AppSettings["WepayClientSecret"];
            WePayConfiguration.productionMode = Convert.ToBoolean(ConfigurationManager.AppSettings["ProductionMode"]);
        }
    }
}
