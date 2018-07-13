using System;

namespace UserAPI.Models
{
    public class User
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Lastname { set; get; }

        public string Username { set; get; }
        
        public string PasswordHash { set; get; }

        public string Email { set; get; }
        
        public DateTime CreateDate { set; get; }

        public DateTime LastLoginDate { set; get; }
    }
}
