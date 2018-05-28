using DienChan.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DienChan.Logic;
using DienChan.Entities;

namespace DienChanAPI.Controllers
{
    public class CustomersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCustomers(string token, int userId)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var customers = CustomersLogic.GetCustomers();

            if (customers == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, customers);
        }

        [HttpGet]
        public IHttpActionResult GetCustomer(string token, int userId, int id)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var customer = CustomersLogic.GetCustomer(id);

            if (customer == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, customer);
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(string token, int userId, Customer customer)
        {
            if (!ModelState.IsValid || !ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var customerDb = CustomersLogic.GetCustomer(customer.customerId);

            if (customerDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = CustomersLogic.UpdateCustomer(customer);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(string token, int userId, Customer customer)
        {
            if (!ModelState.IsValid || !ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var result = CustomersLogic.CreateCustomer(customer);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(string token, int userId, int id)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var customerDb = CustomersLogic.GetCustomer(id);

            if (customerDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = CustomersLogic.DeleteCustomer(id);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }
    }
}