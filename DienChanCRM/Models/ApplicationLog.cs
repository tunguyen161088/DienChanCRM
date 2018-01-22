using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using PetaPoco;

namespace DienChanCRM.Models
{
    [TableName("ApplicationLog")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class ApplicationLog
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string ComputerName { get; set; }
        public string OSVersion { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
