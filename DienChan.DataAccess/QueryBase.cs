using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class QueryBase
    {
        public Database Db()
        {
            return new Database(Configuration.ConnectionString, "System.Data.SqlClient")
            {
                CommandTimeout = 600
            };
        }
    }
}
