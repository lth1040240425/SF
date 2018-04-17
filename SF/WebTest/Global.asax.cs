using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using SF.AutoFac;
using SF.Data;
using SqlSugar;
using Autofac.Integration.WebApi;

namespace WebTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoFacHelper.Init(p =>
            {

            }, q =>
            {
                q.Register(c => new SFContext("default")).InstancePerLifetimeScope();
                //q.RegisterApiControllers()
            });
        }
    }
}
