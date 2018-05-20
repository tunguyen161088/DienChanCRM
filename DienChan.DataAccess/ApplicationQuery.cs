using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class ApplicationQuery : QueryBase
    {
        public void UpdateLog(ApplicationLog log)
        {
            try
            {
                var query = Sql.Builder.Append(@"
INSERT INTO [dbo].[ApplicationLog]
           ([Message]
           ,[StackTrace]
           ,[ComputerName]
           ,[OSVersion]
           ,[CreatedDate]
           ,[ApplicationName])
     VALUES
           (@0
           ,@1
           ,@2
           ,@3
           ,@4
           ,'Dien Chan API')", log.message, log.stacktrace, log.computername, log.osversion, DateTime.Now);

                Db().Execute(query);
            }
            catch
            {
                //
            }
        }

        public void UpdateToken(ApplicationToken at)
        {
//            Db().Execute(@"
//DELETE FROM [dbo].[ApplicationToken]
//WHERE UserId = @0;", at.UserId);

            Db().Execute(@"
INSERT INTO [dbo].[ApplicationToken]
           ([Token]
           ,[Active]
           ,[UserID]
           ,[CreatedDate]
           ,[ExpirationDate])
     VALUES
           (@0
           ,1
           ,@1
           ,GETDATE()
           ,DATEADD(hh, 10, GETDATE()))", at.Token, at.UserId);
        }

        public ApplicationToken GetToken(string token, int userId)
        {
            var query = Sql.Builder.Append(@"
SELECT TOP 1 [ID],
                  [Token],
                  [Active],
                  [UserID],
                  [CreatedDate],
                  [ExpirationDate]
FROM [DienChanCRM].[dbo].[ApplicationToken]
WHERE Token = @0
      AND UserID = @1
      AND Active = 1
      AND ExpirationDate > GETDATE();", token, userId);

            var at = Db().SingleOrDefault<ApplicationToken>(query);

            return at;
        }
    }
}
