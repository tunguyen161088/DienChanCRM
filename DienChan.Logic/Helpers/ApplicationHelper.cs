using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;

namespace DienChan.Logic.Helpers
{
    public class ApplicationHelper
    {
        private static readonly ApplicationQuery ApplicationQuery = new ApplicationQuery();
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

            new EmailHelper().SendError(log);

            ApplicationQuery.UpdateLog(log);
        }

        public static string GenerateToken(int userId)
        {
            var at = new ApplicationToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId
            };

            ApplicationQuery.UpdateToken(at);

            return at.Token;
        }

        public static ApplicationToken GetToken(string token, int userId)
        {
            return ApplicationQuery.GetToken(token, userId);
        }

        public static bool IsTokenValid(string token, int userId)
        {
            var at = GetToken(token, userId);

            return at != null;
        }
    }
}
