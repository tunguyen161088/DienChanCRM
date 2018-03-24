using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using PetaPoco;

namespace DienChan.DataAccess
{
    public class ApplicationLogQuery : QueryBase
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
    }
}
