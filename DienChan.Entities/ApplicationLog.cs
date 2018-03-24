using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DienChan.Entities
{
    public class ApplicationLog
    {
        public string message { get; set; }
        public string stacktrace { get; set; }
        public string computername { get; set; }
        public string osversion { get; set; }
        public DateTime createdate { get; set; }
        public string applicationname { get; set; }
    }
}
