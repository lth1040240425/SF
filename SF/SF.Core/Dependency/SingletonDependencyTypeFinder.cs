﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Reflection;

namespace SF.Core.Dependency
{
    /// <summary>
    /// <see cref="ISingletonDependency"/>接口实现类查找器
    /// </summary>
    public class SingletonDependencyTypeFinder : ITypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="SingletonDependencyTypeFinder"/>类型的新实例
        /// </summary>
        public SingletonDependencyTypeFinder()
        {
            AssemblyFinder = new DirectoryAssemblyFinder();
        }

        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Type[] FindAll()
        {
            try
            {
                Assembly[] assemblies = AssemblyFinder.FindAll();
                return assemblies.SelectMany(assembly =>
                        assembly.GetTypes().Where(type =>
                            typeof(ISingletonDependency).IsAssignableFrom(type) && !type.IsAbstract))
                    .Distinct().ToArray();
            }
            catch (ReflectionTypeLoadException e)
            {
                string msg = e.Message;
                Exception[] exs = e.LoaderExceptions;
                msg = msg + "\r\n详情：" + string.Join("---", exs.Select(m => m.Message));
                throw new Exception(msg, e);
            }
        }

    }
}
