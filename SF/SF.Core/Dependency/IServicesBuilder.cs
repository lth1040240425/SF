﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Core.Dependency
{
    /// <summary>
    /// 定义服务器映射集合创建功能
    /// </summary>
    public interface IServicesBuilder
    {
        /// <summary>
        /// 将当前服务创建为
        /// </summary>
        /// <returns>服务映射集合</returns>
        IServiceCollection Build();
    }
}
