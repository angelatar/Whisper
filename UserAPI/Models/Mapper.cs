using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Models
{
    public static class Mapper
    {
        public static User CreateUser(Dictionary<string,object> userSkeleton)
        {
            return new User()
            {
                ID = int.Parse(userSkeleton["Id"].ToString()),
                Name=userSkeleton["Name"].ToString(),
                Lastname = userSkeleton["Lastname"].ToString(),
                Username = userSkeleton["Username"].ToString(),
                PasswordHash = userSkeleton["PasswordHash"].ToString(),
                Email = userSkeleton["Email"].ToString(),
                CreateDate = DateTime.Parse(userSkeleton["CreateDate"].ToString()),
                LastLoginDate = DateTime.Parse(userSkeleton["LastLoginDate"].ToString())
            };
        }

        public static Dictionary<string,object> SplitUser(User user)
        {
            var userSkeleton = new Dictionary<string, object>();

            userSkeleton.Add("Id", user.ID);
            userSkeleton.Add("Name", user.Name);
            userSkeleton.Add("Lastname", user.Lastname);
            userSkeleton.Add("Username", user.Username);
            userSkeleton.Add("PasswordHash", user.PasswordHash);
            userSkeleton.Add("Email", user.Email);
            userSkeleton.Add("CreateDate", user.CreateDate);
            userSkeleton.Add("LastLoginDate", user.LastLoginDate);

            return userSkeleton;
        }
    }
}
