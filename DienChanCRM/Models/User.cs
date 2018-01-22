using System;

namespace DienChanCRM.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserPermission Permission { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
