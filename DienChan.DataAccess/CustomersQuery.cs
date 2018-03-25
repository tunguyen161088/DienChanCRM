using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class CustomersQuery : QueryBase
    {
        public void UpdateCustomer(Database db, Customer customer)
        {
            var query = Sql.Builder.Append(@"
UPDATE [dbo].[Customers]
   SET [FirstName] = @0
      ,[LastName] = @1
      ,[PhoneNumber] = @2
      ,[Email] = @3
      ,[Address1] = @4
      ,[Address2] = @5
      ,[City] = @6
      ,[State] = @7
      ,[Zip] = @8
      ,[Country] = @9
      ,[UpdateDate] = @10
 WHERE CustomerID = @11", customer.firstName, customer.lastName, customer.phoneNumber, customer.email, customer.address1, customer.address2,
                customer.city, customer.state, customer.zip, customer.country, customer.updateDate, customer.customerId);

            db.Execute(query);
        }

        public int CreateCustomer(Database db, Customer customer)
        {
            var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[Customers]
           ([FirstName]
           ,[LastName]
           ,[PhoneNumber]
           ,[Email]
           ,[Address1]
           ,[Address2]
           ,[City]
           ,[State]
           ,[Zip]
           ,[Country]
           ,[UpdateDate])
     VALUES
           (@0
           ,@1
           ,@2
           ,@3
           ,@4
           ,@5
           ,@6
           ,@7
           ,@8
           ,@9
           ,@10) Select SCOPE_IDENTITY()", customer.firstName, customer.lastName, customer.phoneNumber, customer.email,
                customer.address1, customer.address2, customer.city, customer.state, customer.zip, customer.country, customer.updateDate);

            var customerId = db.ExecuteScalar<int>(query);

            return customerId;
        }
    }
}
