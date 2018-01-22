using System.Configuration;

namespace DienChanCRM.Helpers
{
    public class Configuration
    {
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"] ?? "";

        public static string Version => ConfigurationManager.AppSettings["Version"] ?? "";
    }
}
