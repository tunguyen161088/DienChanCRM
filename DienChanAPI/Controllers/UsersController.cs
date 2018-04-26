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
    public class UsersController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetAuthentication(User user, string apiKey)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var userDb = UsersLogic.GetAuthentication(user, apiKey);

            if (userDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, userDb);
        }

        [HttpPost]
        public IHttpActionResult Register(User user, string apiKey)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var userDb = UsersLogic.Register(user, apiKey);

            if (userDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, userDb);
        }

        [HttpPut]
        public IHttpActionResult UpdateUser(User user, string token)
        {
            if (!ModelState.IsValid || !ApplicationHelper.IsTokenValid(token, user.id))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var userDb = UsersLogic.GetUser(user);

            if (userDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = UsersLogic.UpdateUser(user);

            return Content(HttpStatusCode.OK, result);
        }
    }
}