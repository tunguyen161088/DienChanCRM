using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DienChanCRM.Models;
using PetaPoco;

namespace DienChanCRM.DAL
{
    public class ProductQuery : QueryBase
    {
        public List<Models.Product> SearchProducts(string textSearch)
        {
            var query = Sql.Builder.Append(@"
SELECT TOP 50 [ID],
              [Name],
              [Description],
              [Price],
              [Weight],
              [CategoryID],
              [Reference],
(
    SELECT CategoryName
    FROM Category
    WHERE ID = CategoryID
) AS Category
FROM [DienChanCRM].[dbo].[Products]
WHERE ID = @0
      OR Name LIKE '%' + @0+'%'
      OR Description LIKE '%' + @0+'%'", textSearch);

            return Db().Fetch<Models.Product>(query);
        }

        public List<Models.Product> GetProducts()
        {
            var query = Sql.Builder.Append(@"
SELECT [ID],
              [Name],
              [Description],
              [Price],
              [Weight],
              [CategoryID],
              [Reference],
(
    SELECT CategoryName
    FROM Category
    WHERE ID = CategoryID
) AS Category
FROM [DienChanCRM].[dbo].[Products]");

            return Db().Fetch<Models.Product>(query);
        }

        public void RemoveProduct(string productId)
        {
            var query = Sql.Builder.Append(@"
DELETE FROM [DienChanCRM].[dbo].[Products]
WHERE ID = @0", productId);

            Db().Execute(query);
        }

        public void UpdateProduct(Models.Product product)
        {
            if (Db().ExecuteScalar<int>(@"Select COUNT(*) FROM [DienChanCRM].[dbo].[Products] WHERE Reference = @0", product.Reference) > 0)
                Db().Update(product);
            else
                Db().Insert(product);
        }
    }
}
