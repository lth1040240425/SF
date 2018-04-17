using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Data;

namespace Test.Models
{
    public class Classes:EntityBase<int>
    {
        public string Name { get; set; }
    }
}
