using DienChanCRM.Models;
using PetaPoco;

namespace DienChanCRM.DAL
{
    class GetAuthenticationQuery: QueryBase
    {
        public User GetAuthentication(string userName, string password)
        {
            var query = Sql.Builder.Append(@"
SELECT TOP 1
        *
FROM [User]
WHERE Username = @0
AND Password = @1", userName, password);

            var user = Db().SingleOrDefault<User>(query);

            if (user == null) return null;

            user.Permission = GetPermission(user.ID);

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
