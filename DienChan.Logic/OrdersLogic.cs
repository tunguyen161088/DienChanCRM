using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;
using DienChan.Logic.Helpers;

namespace DienChan.Logic
{
    public class OrdersLogic
    {
        private static readonly OrdersQuery _query = new OrdersQuery();
        private static readonly EmailHelper _emailHelper = new EmailHelper();
        private static  readonly ReportHelper _reportHelper = new ReportHelper();
        public static List<Order> GetOrders()
        {
            return _query.GetOrders();
        }

        public static Order GetOrder(int orderId)
        {
            return _query.GetOrder(orderId);
        }

        public static ActionResult UpdateOrder(Order order)
        {
            var result = new ActionResult();

            if (IsOrderValid(order))
                return _query.UpdateOrder(order);

            result.Message = "Order not valid!";

            return result;
        }

        public static ActionResult CreateOrder(Order order)
        {
            var result = new ActionResult();

            if (IsOrderValid(order))
                return _query.CreateOrder(order);

            result.Message = "Order not valid!";

            return result;
        }

        public static ActionResult DeleteOrder(int orderId)
        {
            return _query.DeleteOrder(orderId);
        }

        private static bool IsOrderValid(Order order)
        {
            var subtotal = order.items.Sum(i => i.quantity * i.unitPrice);

            if (order.subTotal != subtotal) return false;

            if (order.orderTotal != subtotal + order.tax - order.discount) return false;

            //add more validation here

            return true;
        }

        public static ActionResult SendReport(Order order, string email)
        {
            var result = new ActionResult();

            try
            {
                var report = _reportHelper.CreateReport(order);

                _emailHelper.SendReportEmail(order, report, email);

                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = "Email report failed!";
            }

            return result;
        }
    }
}
