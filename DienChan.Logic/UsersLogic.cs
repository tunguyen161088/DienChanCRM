using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;

namespace DienChan.Logic
{
    public class UsersLogic
    {
        private static readonly UserQuery _query = new UserQuery();
        public static User GetAuthentication(string userName, string password)
        {
            return _query.GetAuthentication(userName, password);
        }
    }
}
