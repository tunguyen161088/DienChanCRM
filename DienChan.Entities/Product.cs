using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace DienChan.Entities
{
    [TableName("Products")]
    [PrimaryKey("ProductID", AutoIncrement = true)]
    public class Product
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal weight { get; set; }
        public int categoryId { get; set; }
        [Ignore]
        public Category category { get; set; }
        public string imageUrl { get; set; }
        [Ignore]
        public string image { get; set; }
        [Ignore]
        public bool isImageUpdate { get; set; }
    }
}
