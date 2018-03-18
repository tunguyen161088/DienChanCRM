using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DienChan.Entities
{
    public class Product
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal weight { get; set; }
        public string category { get; set; }
        public string imageUrl { get; set; }
    }
}
