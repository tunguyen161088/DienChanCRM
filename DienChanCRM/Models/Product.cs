using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace DienChanCRM.Models
{
    [TableName("Products")]
    [PrimaryKey("ID", AutoIncrement = false)]
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        [ResultColumn]
        public string Category { get; set; }
        public int CategoryID { get; set; }
        [ResultColumn]
        public int Reference { get; set; }
    }
}
