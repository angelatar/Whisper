using System;

namespace UserAPI.Models
{
    /// <summary>
    /// The class represents the User Construction
    /// </summary>
    public class User
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// User lastname
        /// </summary>
        public string Lastname { set; get; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// User password
        /// </summary>
        public string PasswordHash { set; get; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// User create datetime
        /// </summary>
        public DateTime CreateDate { set; get; }

        /// <summary>
        /// User last login datetime
        /// </summary>
        public DateTime LastLoginDate { set; get; }
    }
}
