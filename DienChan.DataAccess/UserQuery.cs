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
        public User GetUser(string userName, string password)
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

        public ActionResult CreateUser(User user)
        {
            var result = new ActionResult();

            try
            {
                if (Db().ExecuteScalar<int>(@"Select COUNT(*) FROM [dbo].[User] WHERE UserName = @0", user.username) > 0)
                {
                    result.Message = "Username has been taken!";

                    return result;
                }

                var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[User]
           ([UserName]
           ,[Password]
           ,[CreatedDate]
           ,[PermissionID]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[Active])
     VALUES
           (@0
           ,@1
           ,@2
           ,2
           ,@3
           ,@4
           ,@5
           ,1)", user.username, user.password, user.createdDate, user.firstName, user.lastName, user.email);

                Db().Execute(query);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }

        public User UpdateUser(User user)
        {
            var query = Sql.Builder.Append(@"
UPDATE [dbo].[User]
  SET
      Password = @0,
      CreatedDate = @1,
      PermissionID = @2,
      FirstName = @3,
      LastName = @4,
      Email = @5
WHERE ID = @6
      AND UserName = @7;", user.password, user.createdDate, user.permissionId, user.firstName, user.lastName,
                user.email, user.id, user.username);

            Db().Execute(query);

            var userDb = GetUser(user.username, user.password);

            return userDb;
        }
    }
}
