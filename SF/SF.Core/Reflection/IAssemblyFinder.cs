using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SF.Core.Reflection
{
    /// <summary>
    /// 定义程序集查找器
    /// </summary>
    public interface IAssemblyFinder : IFinder<Assembly>
    { }
}
