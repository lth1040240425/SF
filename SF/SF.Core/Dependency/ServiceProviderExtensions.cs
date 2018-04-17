﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Properties;

namespace SF.Core.Dependency
{
    /// <summary>
    /// 服务提供者扩展辅助操作
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 获取指定类型服务的实例
        /// </summary>
        /// <typeparam name="T">要获取实例的服务类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <returns>指定类型的实例</returns>
        public static T GetService<T>(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new InvalidOperationException(Resources.Ioc_FrameworkNotInitialized);
            }
            return (T)provider.GetService(typeof(T));
        }

        /// <summary>
        /// 获取指定类型服务的非空实例
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <param name="serviceType">要获取实例的服务类型</param>
        /// <returns>指定类型的非空实例</returns>
        public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
        {
            if (provider == null)
            {
                throw new InvalidOperationException(Resources.Ioc_FrameworkNotInitialized);
            }
            object value = provider.GetService(serviceType);
            if (value == null)
            {
                throw new InvalidOperationException($"类型“{serviceType.Name}”的实现类型无法找到");
            }
            return value;
        }

        /// <summary>
        /// 获取指定类型服务的非空实例
        /// </summary>
        /// <typeparam name="T">要获取实例的服务类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <returns>指定类型的非空实例</returns>
        public static T GetRequiredService<T>(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new InvalidOperationException(Resources.Ioc_FrameworkNotInitialized);
            }
            return (T)GetRequiredService(provider, typeof(T));
        }

        /// <summary>
        /// 获取指定类型服务的所有实例
        /// </summary>
        /// <typeparam name="T">要获取实例的服务类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <returns>指定类型的所有实例</returns>
        public static IEnumerable<T> GetServices<T>(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new InvalidOperationException(Resources.Ioc_FrameworkNotInitialized);
            }
            return provider.GetRequiredService<IEnumerable<T>>();
        }

        /// <summary>
        /// 获取指定类型服务的所有实例
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <param name="serviceType">要获取实例的服务类型</param>
        /// <returns>指定类型的所有实例</returns>
        public static IEnumerable<object> GetServices(this IServiceProvider provider, Type serviceType)
        {
            if (provider == null)
            {
                throw new InvalidOperationException(Resources.Ioc_FrameworkNotInitialized);
            }
            Type genericEnumerable = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)provider.GetRequiredService(genericEnumerable);
        }
    }
}
