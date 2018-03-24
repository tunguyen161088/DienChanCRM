using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class ProductsQuery : QueryBase
    {
        public List<Product> GetProducts()
        {
            var query = Sql.Builder.Append(@"
SELECT p.*, c.*
FROM [DienChanCRM].[dbo].[Products] p (NOLOCK)
     INNER JOIN [DienChanCRM].[dbo].[Category] c(NOLOCK) ON p.CategoryID = c.ID;");

            return Db().Fetch<Product, Category>(query);
        }

        public Product GetProduct(int productId)
        {
            var query = Sql.Builder.Append(@"
SELECT *
FROM [DienChanCRM].[dbo].[Products](NOLOCK)
WHERE ProductID = @0", productId);

            return Db().FirstOrDefault<Product>(query);
        }

        public void DeleteProduct(int productId)
        {
            var query = Sql.Builder.Append(@"
DELETE FROM [DienChanCRM].[dbo].[Products]
WHERE ProductID = @0", productId);

            Db().Execute(query);
        }

        public void UpdateProduct(Product product)
        {
            var query = Sql.Builder.Append(@"
UPDATE [dbo].[Products]
   SET [Name] = @0
      ,[Description] = @1
      ,[Price] = @2
      ,[Weight] = @3
      ,[CategoryID] = @4
      ,[ImageUrl] = @5
 WHERE ProductID = @6", product.name, product.description, product.price, product.weight, product.categoryId,
                product.imageUrl, product.productId);

            Db().Execute(query);
        }

        public int CreateProduct(Product product)
        {
            var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[Products]
           ([Name]
           ,[Description]
           ,[Price]
           ,[Weight]
           ,[CategoryID])
     VALUES
           (@0
           ,@1
           ,@2
           ,@3
           ,@4) Select Scope_Identity()", product.name, product.description, product.price, product.weight, product.categoryId);

            var productId = Db().ExecuteScalar<int>(query);

            var imageUrl = Configuration.BaseImageUrl + $"{productId}.jpg";

            Db().Execute("Update [dbo].Products Set ImageUrl = @0 Where ProductID = @1", imageUrl, productId);

            return productId;
        }
    }
}
