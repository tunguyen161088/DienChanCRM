using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChanCRM.Models;
using PetaPoco;

namespace DienChanCRM.DAL
{
    public class OrderQuery : QueryBase
    {
        public List<Order> GetListOrders(string keyword = "")
        {
            var query = Sql.Builder.Append(@"
SELECT TOP 1
        *
FROM [User]
WHERE Username = @0
AND Password = @1", keyword);

            return Db().Fetch<Order>(query);
        }

        public void UpdateOrder(Order order)
        {
            
        }
    }
}
