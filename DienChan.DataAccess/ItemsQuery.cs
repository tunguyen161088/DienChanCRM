using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class ItemsQuery : QueryBase
    {
        public void UpdateItems(Database db, List<Item> orderItems, int orderId)
        {
            DeleteItems(db, orderId);

            CreateItems(db, orderItems, orderId);
        }

        public void CreateItems(Database db, List<Item> orderItems, int orderId)
        {
            foreach (var item in orderItems)
            {
                var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[Items]
           ([OrderID]
           ,[Quantity]
           ,[UnitPrice]
           ,[ProductID]
           ,[Name]
           ,[Description]
           ,[Weight]
           ,[ImageUrl]
           ,[UpdateDate]
           ,[CategoryName])
     VALUES
           (@0
           ,@1
           ,@2
           ,@3
           ,@4
           ,@5
           ,@6
           ,@7
           ,@8)", orderId, item.quantity, item.unitPrice, item.productId, item.name, 
                    item.description, item.weight, item.imageUrl, item.updateDate, item.categoryName);

                db.Execute(query);
            }
        }

        private void DeleteItems(Database db, int orderId)
        {
            var query = Sql.Builder.Append(@"
DELETE FROM [DienChanCRM].[dbo].[Items]
WHERE OrderID = @0;", orderId);

            db.Execute(query);
        }

        public List<Item> GetItems(int orderId)
        {
            var query = Sql.Builder.Append(@"
SELECT i.*
FROM [DienChanCRM].[dbo].[Items] i(NOLOCK)
WHERE OrderId = @0;", orderId);

            return Db().Fetch<Item>(query);
        }
    }
}
