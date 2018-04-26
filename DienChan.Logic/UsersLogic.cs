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
    public class UsersLogic
    {
        private static readonly UserQuery _query = new UserQuery();
        public static User GetAuthentication(User u, string apiKey)
        {
            if (apiKey != Configuration.ApiKey)
                return null;

            var user = _query.GetUser(u.username, u.password);

            if (user != null)
                user.token = ApplicationHelper.GenerateToken(user.id);

            return user;
        }

        public static User GetUser(User user)
        {
            if (!ApplicationHelper.IsTokenValid(user.token, user.id)) return null;

            var userDb = _query.GetUser(user.username, user.password);

            return userDb;
        }

        public static ActionResult Register(User user, string apiKey)
        {
            if (apiKey != Configuration.ApiKey)
                return new ActionResult { Message = "Api Key invalid!" };

            return _query.CreateUser(user);
        }

        public static User UpdateUser(User user)
        {
            var userDb = _query.UpdateUser(user);

            if (userDb != null)
                userDb.token = user.token;

            return userDb;
        }
    }
}
