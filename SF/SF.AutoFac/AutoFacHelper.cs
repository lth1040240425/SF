using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SF.Core.Dependency;

namespace SF.AutoFac
{
    public class AutoFacHelper
    {
        private static IServiceProvider serviceProvider { get; set; }
        public static IServiceProvider ServiceProvider { get { return serviceProvider; } }

        public static void Init(Action<IServiceCollection> actionServiceCollection, Action<ContainerBuilder> actionContainerBuilder)
        {
            IServicesBuilder builder = new ServicesBuilder(new ServiceBuildOptions());
            IServiceCollection services = builder.Build();
            actionServiceCollection(services);//添加自定义注入
            IIocBuilder iocBuilder = new AutofacIocBuilder(services, actionContainerBuilder);

            serviceProvider = iocBuilder.Build();
        }
    }
}
