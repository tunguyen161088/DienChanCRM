using DienChanCRM.Helpers;
using PetaPoco;

namespace DienChanCRM.DAL
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
