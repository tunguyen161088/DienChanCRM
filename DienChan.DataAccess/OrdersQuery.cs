using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class OrdersQuery: QueryBase
    {
        public List<Order> GetOrders()
        {
            var query = Sql.Builder.Append(@"
SELECT o.*,
       c.*
FROM [DienChanCRM].[dbo].[Orders] o(NOLOCK)
     INNER JOIN [DienChanCRM].[dbo].[Customers] c(NOLOCK) ON o.CustomerID = c.CustomerID;");

            return Db().Fetch<Order, Customer>(query);
        }

        public Order GetOrder(int orderId)
        {
            var query = Sql.Builder.Append(@"
SELECT *
FROM [DienChanCRM].[dbo].[Orders](NOLOCK)
WHERE OrderID = @0", orderId);

            return Db().FirstOrDefault<Order>(query);
        }

        public void UpdateOrder(Order order)
        {
            
        }

        public void CreateOrder(Order order)
        {
            
        }

        public void DeleteOrder(int orderId)
        {
            
        }
    }
}
