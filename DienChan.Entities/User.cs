using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DienChan.Entities
{
    public class User
    {
        public int id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public UserPermission permission { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public DateTime createdDate { get; set; }
    }

    public class UserPermission
    {
        public int id { get; set; }

        public string pemissionName { get; set; }

        public string pemissionDescription { get; set; }
    }
}
