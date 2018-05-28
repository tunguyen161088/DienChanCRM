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
        public List<Customer> GetCustomers()
        {
            var query = Sql.Builder.Append(@"
SELECT *
FROM (SELECT *,
         ROW_NUMBER() OVER (PARTITION BY Email ORDER BY UpdateDate DESC) AS rn
   FROM [DienChanCRM].[dbo].[Customers]
   WHERE Active = 1) a
WHERE a.rn = 1 ORDER BY a.FirstName, a.LastName, a.Email");

            var customers = Db().Fetch<Customer>(query);

            return customers;
        }

        public Customer GetCustomer(int customerId)
        {
            var query = Sql.Builder.Append(@"
SELECT *
   FROM [DienChanCRM].[dbo].[Customers] (NOLOCK)
   WHERE CustomerID = @0 AND Active = 1", customerId);

            var customer = Db().FirstOrDefault<Customer>(query);

            return customer;
        }

        public ActionResult UpdateCustomer(Customer customer)
        {
            var result = new ActionResult();

            try
            {
                UpdateCustomer(Db(), customer);

                result.Success = true;
            }
            catch(Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }
        

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

        public ActionResult CreateCustomer(Customer customer)
        {
            var result = new ActionResult();

            try
            {
                CreateCustomer(Db(), customer);

                result.Success = true;
            }
            catch(Exception e)
            {
                result.Message = e.Message;
            }

            return result;
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
           ,[UpdateDate]
           ,[Active])
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
           ,@10
           ,1) Select SCOPE_IDENTITY()", customer.firstName, customer.lastName, customer.phoneNumber, customer.email,
                customer.address1, customer.address2, customer.city, customer.state, customer.zip, customer.country, customer.updateDate);

            var customerId = db.ExecuteScalar<int>(query);

            return customerId;
        }

        public ActionResult DeleteCustomer(int customerId)
        {
            var result = new ActionResult();

            try
            {
                var query = Sql.Builder.Append(@"
  Update [DienChanCRM].[dbo].[Customers]
  SET Active = 0
  WHERE Email = (Select Top 1 Email FROM [DienChanCRM].[dbo].[Customers] (NOLOCK) WHERE CustomerID = @0)", customerId);

                Db().Execute(query);

                result.Success = true;
            }
            catch(Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }
    }
}
