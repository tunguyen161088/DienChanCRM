using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DienChan.DataAccess
{
    public class Configuration
    {
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"] ?? "";// "Server=184.168.194.60;uid=tunguyen;pwd=1Nothing;database=DienChanCRM;Connect Timeout=60;MultipleActiveResultSets=true";

        public static string FtpUrl => ConfigurationManager.AppSettings["FtpUrl"] ?? "";

        public static string FtpUsername => ConfigurationManager.AppSettings["FtpUsername"] ?? "";

        public static string FtpPassword => ConfigurationManager.AppSettings["FtpPassword"] ?? "";

        public static string BaseImageUrl => ConfigurationManager.AppSettings["BaseImageUrl"] ?? "";
    }
}
