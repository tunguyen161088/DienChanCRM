using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;
using System.Reflection;

namespace DienChan.DataAccess
{
    public class OrdersQuery : QueryBase
    {
        private readonly CustomersQuery _customersQuery = new CustomersQuery();

        private readonly ItemsQuery _itemsQuery = new ItemsQuery();

        public List<Order> GetOrders()
        {
            var query = Sql.Builder.Append(@"
SELECT o.*,
       c.*
FROM [DienChanCRM].[dbo].[Orders] o(NOLOCK)
     INNER JOIN [DienChanCRM].[dbo].[Customers] c(NOLOCK) ON o.CustomerID = c.CustomerID
WHERE Active = 1;");

            var orders = Db().Fetch<Order, Customer>(query);

            orders.ForEach(o => o.items = _itemsQuery.GetItems(o.orderId));

            return orders;
        }

        public Order GetOrder(int orderId)
        {
            var query = Sql.Builder.Append(@"
SELECT o.*,
       c.*
FROM [DienChanCRM].[dbo].[Orders] o(NOLOCK)
     INNER JOIN [DienChanCRM].[dbo].[Customers] c(NOLOCK) ON o.CustomerID = c.CustomerID
WHERE o.OrderID = @0 AND Active = 1;", orderId);

            var order = Db().Fetch<Order, Customer>(query).FirstOrDefault();

            if (order != null)
                order.items = _itemsQuery.GetItems(order.orderId);

            return order;
        }

        public ActionResult UpdateOrder(Order order)
        {
            var db = Db();

            var result = new ActionResult();

            try
            {
                db.BeginTransaction();

                UpdateOrderInfo(db, order);

                _customersQuery.UpdateCustomer(db, order.customer);

                if (order.isItemUpdate)
                    _itemsQuery.UpdateItems(db, order.items, order.orderId);

                db.CompleteTransaction();

                result.Success = true;
            }
            catch (Exception e)
            {
                db.AbortTransaction();

                result.Message = e.Message;
            }

            return result;
        }

        public ActionResult CreateOrder(Order order)
        {
            var db = Db();

            var result = new ActionResult();

            try
            {
                db.BeginTransaction();

                order.customer.customerId = _customersQuery.CreateCustomer(db, order.customer);

                order.orderId = CreateOrderInfo(db, order);

                _itemsQuery.CreateItems(db, order.items, order.orderId);

                db.CompleteTransaction();

                result.Success = true;
            }
            catch (Exception e)
            {
                db.AbortTransaction();

                result.Message = e.Message;
            }

            return result;
        }

        public ActionResult DeleteOrder(int orderId)
        {
            var result = new ActionResult();

            try
            {
                var query = Sql.Builder.Append(@"
UPDATE [dbo].[Orders]
   SET Active = 0
 WHERE OrderID = @0", orderId);

                Db().Execute(query);

                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }

        private void UpdateOrderInfo(Database db, Order order)
        {
            var query = Sql.Builder.Append(@"
UPDATE [dbo].[Orders]
   SET 
       [SubTotal] = @0
      ,[Tax] = @1
      ,[Discount] = @2
      ,[OrderTotal] = @3
      ,[UpdateDate] = @4
 WHERE OrderID = @5", order.subTotal, order.tax, order.discount, order.orderTotal, order.updateDate, order.orderId);

            db.Execute(query);
        }

        private int CreateOrderInfo(Database db, Order order)
        {
            var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[Orders]
           ([CustomerID]
           ,[OrderDate]
           ,[SubTotal]
           ,[Tax]
           ,[Discount]
           ,[OrderTotal]
           ,[Active]
           ,[UpdateDate])
     VALUES
           (@0
           ,@1
           ,@2
           ,@3
           ,@4
           ,@5
           ,1
           ,@6) SELECT SCOPE_IDENTITY()", order.customer.customerId, order.orderDate, order.subTotal,
                order.tax, order.discount, order.orderTotal, order.orderDate);

            var orderId = db.ExecuteScalar<int>(query);

            return orderId;
        }
    }
}
