using SF.AutoFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using SF.Data;
using Test.Service;
using SF.Core.Dependency;
using SF.Core.Reflection;

namespace WebApiTest
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
            });
            var tt = AutoFacHelper.ServiceProvider.GetService<IStudentManageService>();
            var t = tt.GetStu(1);
        }
    }
}
