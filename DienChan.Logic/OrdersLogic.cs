using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;

namespace DienChan.Logic
{
    public class OrdersLogic
    {
        private static readonly OrdersQuery _query = new OrdersQuery();
        public static List<Order> GetOrders()
        {
            return _query.GetOrders();
        }

        public static Order GetOrder(int orderId)
        {
            return _query.GetOrder(orderId);
        }

        public static void UpdateOrder(Order order)
        {
            _query.UpdateOrder(order);
        }

        public static void CreateOrder(Order order)
        {
            _query.CreateOrder(order);
        }

        public static void DeleteOrder(int orderId)
        {
            _query.DeleteOrder(orderId);
        }
    }
}
