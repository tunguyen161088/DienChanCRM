using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace DienChanCRM.DAL
{
    public class QueryBase
    {
        public Database Db()
        {
            return new Database(ConfigurationManager.AppSettings["ConnectionString"] ?? "Server=(local);database=VceDb;Integrated Security=SSPI", "System.Data.SqlClient")
            {
                CommandTimeout = 600
            };
        }
    }
}
