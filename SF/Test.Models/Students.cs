using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Data;

namespace Test.Models
{
    public class Students : EntityBase<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public bool Agent { get; set; }
        public string Code { get; set; }
        public decimal Height { get; set; }
        public int ClassesId { get; set; }
    }
}
