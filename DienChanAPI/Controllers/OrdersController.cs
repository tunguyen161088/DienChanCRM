using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DienChan.Entities;
using DienChan.Logic;

namespace DienChanAPI.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            var orders = OrdersLogic.GetOrders();

            if (orders == null)
                return NotFound();

            return Ok(orders);
        }

        [HttpGet]
        public IHttpActionResult GetOrder(int id)
        {
            var order = OrdersLogic.GetOrder(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPut]
        public IHttpActionResult UpdateOrder(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderDb = OrdersLogic.GetOrder(order.orderId);

            if (orderDb == null)
                return NotFound();

            OrdersLogic.UpdateOrder(order);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateOrder(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            OrdersLogic.CreateOrder(order);

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var order = OrdersLogic.GetOrder(id);

            if (order == null)
                return NotFound();

            OrdersLogic.DeleteOrder(id);

            return Ok();
        }
    }
}