using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace DienChan.Entities
{
    [TableName("Category")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class Category
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
    }
}
