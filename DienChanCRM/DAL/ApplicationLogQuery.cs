using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChanCRM.Helpers;
using DienChanCRM.Models;
using PetaPoco;

namespace DienChanCRM.DAL
{
    public class ApplicationLogQuery : QueryBase
    {
        public static void UpdateLog(ApplicationLog log)
        {
            try
            {
                var database = new Database(Configuration.ConnectionString, "System.Data.SqlClient");

                database.Insert(log);
            }
            catch
            {
                //
            }
        }
    }
}
