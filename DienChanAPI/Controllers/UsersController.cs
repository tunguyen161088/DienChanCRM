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
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public User Get(int id)
        {
            return new User
            {
                id = 1,
                email = "tunguyen161088@gmail.com",
                firstName = "Tu",
                lastName = "Nguyen",
                permission = new UserPermission
                {
                    id = 2,
                    permissionDescription = "Admin",
                    permissionName = "Admin"
                }
            };
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public IHttpActionResult GetAuthentication(User user)
        {
            var userDb = UsersLogic.GetAuthentication(user.username, user.password);

            if (userDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, userDb);
        }
    }
}