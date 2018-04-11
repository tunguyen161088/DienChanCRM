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
        public IHttpActionResult SendReport(string token, int userId, int orderId, string email)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var order = OrdersLogic.GetOrder(orderId);

            if (order == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = OrdersLogic.SendReport(order, email);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpGet]
        public IHttpActionResult GetOrders(string token, int userId)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var orders = OrdersLogic.GetOrders();

            if (orders == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, orders);
        }

        [HttpGet]
        public IHttpActionResult GetOrder(string token, int userId, int id)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var order = OrdersLogic.GetOrder(id);

            if (order == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, order);
        }

        [HttpPut]
        public IHttpActionResult UpdateOrder(string token, int userId, Order order)
        {
            if (!ModelState.IsValid || !ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var orderDb = OrdersLogic.GetOrder(order.orderId);

            if (orderDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = OrdersLogic.UpdateOrder(order);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpPost]
        public IHttpActionResult CreateOrder(string token, int userId, Order order)
        {
            if (!ModelState.IsValid || !ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var result = OrdersLogic.CreateOrder(order);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpDelete]
        public IHttpActionResult DeleteOrder(string token, int userId, int id)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var order = OrdersLogic.GetOrder(id);

            if (order == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = OrdersLogic.DeleteOrder(id);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }
    }
}