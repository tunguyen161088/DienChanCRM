using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;

namespace DienChan.Logic
{
    public class CustomersLogic
    {
        private static readonly CustomersQuery _query = new CustomersQuery();

        public static List<Customer> GetCustomers()
        {
            return _query.GetCustomers();
        }

        public static Customer GetCustomer(int id)
        {
            return _query.GetCustomer(id);
        }

        public static ActionResult UpdateCustomer(Customer customer)
        {
            return _query.UpdateCustomer(customer);
        }

        public static ActionResult CreateCustomer(Customer customer)
        {
            return _query.CreateCustomer(customer);
        }

        public static ActionResult DeleteCustomer(int id)
        {
            return _query.DeleteCustomer(id);
        }
    }
}
