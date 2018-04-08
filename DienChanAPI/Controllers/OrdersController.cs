using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DienChan.Entities;
using DienChan.Logic;
using DienChan.Logic.Helpers;

namespace DienChanAPI.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult SendReport(int orderId, string email)
        {
            var order = OrdersLogic.GetOrder(orderId);

            if (order == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = OrdersLogic.SendReport(order, email);

            if (!result.Success)
                ApplicationLogHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            var orders = OrdersLogic.GetOrders();

            if (orders == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, orders);
        }

        [HttpGet]
        public IHttpActionResult GetOrder(int id)
        {
            var order = OrdersLogic.GetOrder(id);

            if (order == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, order);
        }

        [HttpPut]
        public IHttpActionResult UpdateOrder(Order order)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var orderDb = OrdersLogic.GetOrder(order.orderId);

            if (orderDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = OrdersLogic.UpdateOrder(order);

            if (!result.Success)
                ApplicationLogHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpPost]
        public IHttpActionResult CreateOrder(Order order)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var result = OrdersLogic.CreateOrder(order);

            if (!result.Success)
                ApplicationLogHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var order = OrdersLogic.GetOrder(id);

            if (order == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = OrdersLogic.DeleteOrder(id);

            if (!result.Success)
                ApplicationLogHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }
    }
}