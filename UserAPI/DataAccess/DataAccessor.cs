using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.DataAccess
{
    public class DataAccessor
    {
        private readonly string connString;

        private IConfiguration configuration;

        public DataAccessor()
        {
            this.configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").Build();
            connString = configuration["ConnectionStrings:Connection"];
        }

        public IEnumerable<Dictionary<string, object>> GetAllUsers()
        {
            var users = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(this.connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_User_CRUD", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Get_All_Users");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new Dictionary<string, object>();
                            user.Add("Id", reader["Id"]);
                            user.Add("Name", reader["Name"]);
                            user.Add("Lastname", reader["Lastname"]);
                            user.Add("Username", reader["Username"]);
                            user.Add("PasswordHash", reader["PasswordHash"]);
                            user.Add("Email", reader["Email"]);
                            user.Add("CreateDate", reader["CreateDate"]);
                            user.Add("LastLoginDate", reader["LastLoginDate"]);

                            users.Add(user);
                        }
                    }

                }
            }
            return users;
        }

        public void CreateUser(Dictionary<string, object> user)
        {

            using (var conn = new SqlConnection(this.connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_User_CRUD", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Create_User");
                    cmd.Parameters.AddWithValue("@name", user["Name"]);
                    cmd.Parameters.AddWithValue("@lastname", user["Lastname"]);
                    cmd.Parameters.AddWithValue("@username", user["Username"]);
                    cmd.Parameters.AddWithValue("@passwordhash", user["PasswordHash"]);
                    cmd.Parameters.AddWithValue("@email", user["Email"]);

                    cmd.ExecuteNonQuery();

                }
            }
        }

    }
}
