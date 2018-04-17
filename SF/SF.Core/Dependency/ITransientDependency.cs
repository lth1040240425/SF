﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Core.Dependency
{
    /// <summary>
    /// 实现此接口的类型将自动注册为<see cref="LifetimeStyle.Transient"/>模式
    /// </summary>
    public interface ITransientDependency:IDependency
    {
    }
}
