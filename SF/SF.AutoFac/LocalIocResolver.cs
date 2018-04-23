﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SF.Core.Dependency;

namespace SF.AutoFac
{
    /// <summary>
    /// 本地应用依赖注入解析
    /// </summary>
    public class LocalIocResolver : IIocResolver
    {
        /// <summary>
        /// 获取 依赖注入容器
        /// </summary>
        internal static IContainer Container { get; set; }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return Container.ResolveOptional(type);
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return Container.ResolveOptional<IEnumerable<T>>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            Type typeToResolve = typeof(IEnumerable<>).MakeGenericType(type);
            Array array = Container.ResolveOptional(typeToResolve) as Array;
            if (array != null)
            {
                return array.Cast<object>();
            }
            return new object[0];
        }
    }
}
