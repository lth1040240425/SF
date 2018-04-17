using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF.Utility;
using SqlSugar;

namespace SF.Data
{
    public class SFContext : SqlSugarClient
    {
        public SFContext(string conStr, DbType dbType = DbType.MySql, bool IsAutoCloseConnection = true)
            : base(new ConnectionConfig()
            {
                ConnectionString = conStr,
                DbType = dbType,
                IsAutoCloseConnection = IsAutoCloseConnection
            })
        {

        }

        public SFContext(string conName)
            : this(ConfigHelper.GetConnectionString(conName), DbType.MySql, true)
        {

        }
    }
}
