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
        u.*, p.*
FROM [DienChanCRM].[dbo].[User] u
INNER JOIN [DienChanCRM].[dbo].[UserPermission] p ON u.PermissionID = p.ID
WHERE Username = @0
AND Password = @1
AND Active = 1", userName, password);

            return Db().Fetch<User, UserPermission>(query).FirstOrDefault();
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
