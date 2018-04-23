using SF.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;

namespace SF.AutoFac
{
    /// <summary>
    /// 本地程序-Autofac依赖注入初始化
    /// </summary>
    public class AutofacIocBuilder : IocBuilderBase
    {
        /// <summary>
        /// 初始化一个<see cref="AutofacIocBuilder"/>类型的新实例
        /// </summary>
        /// <param name="services">服务信息集合</param>
        public AutofacIocBuilder(IServiceCollection services)
            : base(services)
        { }

        public AutofacIocBuilder(IServiceCollection services, Action<ContainerBuilder> _action, ProjectType projectType = ProjectType.WebApi)
            : base(services)
        {
            ProjectType = projectType;
            action = _action;
        }

        private Action<ContainerBuilder> action;
        private ProjectType ProjectType;
        /// <summary>
        /// 获取 依赖注入解析器
        /// </summary>
        public IIocResolver Resolver { get; private set; }

        /// <summary>
        /// 添加自定义服务映射
        /// </summary>
        /// <param name="services">服务信息集合</param>
        protected override void AddCustomTypes(IServiceCollection services)
        {
            services.AddInstance(this);
            switch (ProjectType)
            {
                case ProjectType.Console:
                    services.AddSingleton<IIocResolver, LocalIocResolver>();
                    break;
                case ProjectType.MVC:
                    services.AddSingleton<IIocResolver, MvcIocResolver>();
                    break;
                case ProjectType.WebApi:
                    services.AddSingleton<IIocResolver, WebApiIocResolver>();
                    break;
            }
            //services.AddSingleton<IFunctionHandler, NullFunctionHandler>();
        }

        /// <summary>
        /// 构建服务并设置本地程序平台的Resolver
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="assemblies">要检索的程序集集合</param>
        /// <returns>服务提供者</returns>
        protected override IServiceProvider BuildAndSetResolver(IServiceCollection services, Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();
            action(builder);
            switch (ProjectType)
            {
                case ProjectType.WebApi:
                    builder.RegisterApiControllers(assemblies).AsSelf().PropertiesAutowired();
                    builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
                    builder.RegisterWebApiModelBinderProvider();
                    break;
                case ProjectType.MVC:
                    builder.RegisterControllers(assemblies).AsSelf().PropertiesAutowired();
                    builder.RegisterFilterProvider();
                    break;
            }
            builder.Populate(services);
            IContainer container = builder.Build();
            IServiceProvider ServiceProvider;
            switch (ProjectType)
            {
                case ProjectType.WebApi:
                    AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
                    GlobalConfiguration.Configuration.DependencyResolver = resolver;
                    ServiceProvider = (IServiceProvider)resolver.GetService(typeof(IServiceProvider));
                    break;
                case ProjectType.MVC:
                    AutofacDependencyResolver resolvermvc = new AutofacDependencyResolver(container);
                    DependencyResolver.SetResolver(resolvermvc);
                    MvcIocResolver.GlobalResolveFunc = t => resolvermvc.ApplicationContainer.Resolve(t);
                    ServiceProvider = resolvermvc.GetService<IServiceProvider>();
                    break;
                default:
                    LocalIocResolver.Container = container;
                    Resolver = container.Resolve<IIocResolver>();
                    ServiceProvider = Resolver.Resolve<IServiceProvider>();
                    break;
            }

            return ServiceProvider;
        }


    }
}
