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
using SF.Core.Reflection;
using Test.Service;
using SF.Core.Dependency;

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
                var assemblies = new DirectoryAssemblyFinder().FindAll();
                q.RegisterApiControllers(assemblies).AsSelf().PropertiesAutowired();
                q.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
                q.RegisterWebApiModelBinderProvider();
                //q.RegisterApiControllers()
            });
            var tt = AutoFacHelper.ServiceProvider.GetService<IStudentManageService>();
            var t = tt.GetStu(1);
        }
    }
}
