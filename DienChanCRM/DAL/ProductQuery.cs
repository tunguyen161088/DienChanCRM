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
    }
}
