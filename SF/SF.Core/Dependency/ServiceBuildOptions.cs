using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Reflection;

namespace SF.Core.Dependency
{
    /// <summary>
    /// 服务创建配置信息
    /// </summary>
    public class ServiceBuildOptions
    {
        /// <summary>
        /// 初始化一个<see cref="ServiceBuildOptions"/>类型的新实例
        /// </summary>
        public ServiceBuildOptions()
        {
            TransientTypeFinder = new TransientDependencyTypeFinder();
            ScopeTypeFinder = new ScopeDependencyTypeFinder();
            SingletonTypeFinder = new SingletonDependencyTypeFinder();
        }

        /// <summary>
        /// 获取或设置 即时生命周期依赖类型查找器
        /// </summary>
        public ITypeFinder TransientTypeFinder { get; set; }

        /// <summary>
        /// 获取或设置 范围生命周期依赖类型查找器
        /// </summary>
        public ITypeFinder ScopeTypeFinder { get; set; }

        /// <summary>
        /// 获取或设置 单例生命周期依赖类型查找器
        /// </summary>
        public ITypeFinder SingletonTypeFinder { get; set; }
    }
}
