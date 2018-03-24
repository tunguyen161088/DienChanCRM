using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class UserQuery: QueryBase
    {
        public User GetAuthentication(string userName, string password)
        {
            var query = Sql.Builder.Append(@"
SELECT TOP 1
        *
FROM [User]
WHERE Username = @0
AND Password = @1
AND Active = 1", userName, password);

            var user = Db().SingleOrDefault<User>(query);

            if (user == null) return null;

            user.permission = GetPermission(user.id);

            return user;
        }

        private UserPermission GetPermission(int userId)
        {
            var query = Sql.Builder.Append(@"
SELECT TOP 1 [ID],
             [PermissionName],
             [PermissionDescription]
FROM [DienChanCRM].[dbo].[UserPermission]
WHERE ID =
(
    SELECT PermissionID
    FROM [User]
    WHERE ID = @0
)", userId);

            return Db().SingleOrDefault<UserPermission>(query);
        }
    }
}
