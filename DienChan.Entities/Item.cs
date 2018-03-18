using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DienChan.Entities
{
    public class Item : Product
    {
        public int itemId { get; set; }
        public int quantity { get; set; }
        public DateTime updateDate { get; set; }
    }
}
