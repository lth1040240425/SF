using Autofac;
using SF.AutoFac;
using SF.Data;
using Test.Service;
using SF.Core.Dependency;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoFacHelper.Init(p =>
            {

            }, q =>
            {
                q.Register(c => new SFContext("default")).InstancePerLifetimeScope();
                //var assemblies = new DirectoryAssemblyFinder().FindAll();
                //q.RegisterApiControllers(assemblies).AsSelf().PropertiesAutowired();
                //q.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
                //q.RegisterWebApiModelBinderProvider();
                //q.RegisterApiControllers()
            });
            var tt = AutoFacHelper.ServiceProvider.GetService<IStudentManageService>();
            var t = tt.GetStu(1);
            Console.ReadKey();
        }
    }
}
