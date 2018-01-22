using System;
using System.Collections.Generic;

namespace DienChanCRM.Models
{
    public class Order
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }

        public DateTime OrderDate { get; set; }

        public List<Item> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Discount { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
