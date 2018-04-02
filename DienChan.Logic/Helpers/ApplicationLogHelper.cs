using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;

namespace DienChan.Logic.Helpers
{
    public class ApplicationLogHelper
    {
        private static readonly ApplicationLogQuery _applicationLogQuery = new ApplicationLogQuery();
        public static void Log(string message)
        {
            var log = new ApplicationLog
            {
                applicationname = "Dien Chan API",
                computername = Environment.MachineName,
                createdate = DateTime.Now,
                message = message,
                osversion = Environment.OSVersion.VersionString,
                stacktrace = ""
            };

            _applicationLogQuery.UpdateLog(log);
        }
    }
}
