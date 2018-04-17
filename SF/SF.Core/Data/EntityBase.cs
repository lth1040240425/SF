using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Core.Data
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        public TKey Id { get; set; }
    }
}
