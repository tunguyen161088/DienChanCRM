using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChanCRM.Helpers;
using DienChanCRM.Models;
using DienChanCRM.ViewModels;
using PetaPoco;

namespace DienChanCRM.DAL
{
    public class OrderQuery : QueryBase
    {
        public ObservableCollection<OrderViewModel> GetListOrders(string keyword = "")
        {
            var db = Db();

            var query = Sql.Builder.Append(@"SELECT TOP 50 o.ID,
              o.CustomerID,
              o.OrderDate,
              o.SubTotal,
              o.Tax,
              o.Discount,
              o.OrderTotal
FROM DienChanCRM..Orders o ");
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Append(@"INNER JOIN DienChanCRM..Customers c ON o.CustomerID = c.ID
WHERE  CAST(o.ID as varchar(8)) = @0
      OR c.FirstName LIKE @0+'%'
      OR c.LastName LIKE @0+'%'
      OR c.Email LIKE @0+'%'
      OR c.Address1 LIKE @0+'%'
      OR c.PhoneNumber = @0
      OR c.City LIKE @0+'%' ", keyword);
            }

            query.Append(@"ORDER BY OrderDate DESC;");

            var orders = MapHelper.MapOrderModelToViewModel(db.Fetch<Order>(query));

            foreach (var order in orders)
            {
                var queryItem = Sql.Builder.Append(@"
SELECT p.ID,
       p.Name AS ItemName,
       p.Description AS ItemDescription,
       i.Quantity,
       i.UnitPrice,
       c.CategoryName
FROM DienChanCRM..Items i
     JOIN DienChanCRM..Products p ON i.ProductID = p.ID
     JOIN DienChanCRM..Category c ON p.CategoryID = c.ID
WHERE i.OrderID = @0;", order.ID);

                order.Items = MapHelper.MapItemModelToViewModel(db.Fetch<Item>(queryItem));

                var queryCustomer = Sql.Builder.Append(@"
SELECT TOP 1 [ID],
             [FirstName],
             [LastName],
             [PhoneNumber] AS Phone,
             [Email],
             [Address1],
             [Address2],
             [City],
             [State],
             [Zip],
             [Country]
FROM [DienChanCRM].[dbo].[Customers]
WHERE ID = @0;", order.CustomerID);

                order.Customer = MapHelper.MapCustomerModelToViewModel(db.SingleOrDefault<Customer>(queryCustomer));
            }

            return orders;
        }

        public void UpdateOrder(OrderViewModel order)
        {
            var db = Db();

            try
            {
                db.BeginTransaction();

                order.Customer.ID = UpdateCustomerTable(db, order);

                order.ID = UpdateOrderTable(db, order);

                UpdateItemTable(db, order);

                db.CompleteTransaction();
            }
            catch (Exception ex)
            {
                db.AbortTransaction();

                throw ex;
            }
        }

        private int UpdateCustomerTable(Database db, OrderViewModel order)
        {
            var returnCustomerID = db.ExecuteScalar<int>(@"SELECT ISNULL((Select ISNULL(ID,0) from Customers where Email = @0),0)", order.Customer.Email);

            if (returnCustomerID == 0)
            {
                var queryInsert = Sql.Builder.Append(@"
INSERT INTO [dbo].[Customers]
           ([FirstName]
           ,[LastName]
           ,[PhoneNumber]
           ,[Email]
           ,[Address1]
           ,[Address2]
           ,[City]
           ,[State]
           ,[Zip], [Country])
     VALUES
           (@0
           ,@1
           ,@2
           ,@3
           ,@4
           ,@5
           ,@6
           ,@7    
           ,@8,@9)", order.Customer.FirstName, order.Customer.LastName, order.Customer.Phone,
                    order.Customer.Email, order.Customer.Address1 ?? "", order.Customer.Address2 ?? "",
                    order.Customer.City ?? "", order.Customer.State ?? "", order.Customer.Zip ?? "", order.Customer.Country ?? "");

                db.Execute(queryInsert);

                return db.ExecuteScalar<int>(@"Select ID from Customers where Email = @0", order.Customer.Email);
            }

            var queryUpdate = Sql.Builder.Append(@"
UPDATE [dbo].[Customers]
   SET [FirstName] = @0
      ,[LastName] = @1
      ,[PhoneNumber] = @2
      ,[Address1] = @4
      ,[Address2] = @5
      ,[City] = @6
      ,[State] = @7
      ,[Zip] = @8
 WHERE Email = @3", order.Customer.FirstName, order.Customer.LastName, order.Customer.Phone,
                order.Customer.Email, order.Customer.Address1, order.Customer.Address2,
                order.Customer.City, order.Customer.State, order.Customer.Zip);

            db.Execute(queryUpdate);

            return returnCustomerID;
        }

        private int UpdateOrderTable(Database db, OrderViewModel order)
        {
            var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[Orders]
           ([CustomerID]
           ,[OrderDate]
           ,[SubTotal]
           ,[Tax]
           ,[Discount]
           ,[OrderTotal])
     VALUES
           (@0
           ,GETDATE()
           ,@1
           ,0
           ,0
           ,@2)", order.Customer.ID, order.Items.Sum(i => i.SubTotal), order.Items.Sum(i => i.SubTotal));

            db.Execute(query);

            return db.ExecuteScalar<int>(@"Select Top 1 ID from Orders where CustomerID = @0 Order By OrderDate Desc", order.Customer.ID);
        }

        private void UpdateItemTable(Database db, OrderViewModel order)
        {
            foreach (var item in order.Items)
            {
                var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[Items]
           ([OrderID]
           ,[Quantity]
           ,[UnitPrice]
           ,[ProductID])
     VALUES
           (@0
           ,@1
           ,@2
           ,@3)", order.ID, item.Quantity, item.UnitPrice, item.ID);

                db.Execute(query);
            }
        }

        public void DeleteOrder(int orderId)
        {
            var db = Db();

            try
            {
                db.BeginTransaction();

                db.Execute(Sql.Builder.Append(@"DELETE FROM [DienChanCRM].[dbo].[Items] WHERE OrderID = @0", orderId));

                db.Execute(Sql.Builder.Append(@"DELETE FROM DienChanCRM..Orders WHERE ID = @0", orderId));

                db.CompleteTransaction();
            }
            catch (Exception ex)
            {
                db.AbortTransaction();

                throw ex;
            }
        }
    }
}
