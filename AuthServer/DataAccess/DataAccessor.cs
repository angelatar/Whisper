using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace AuthServer.DataAccess
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

        public Dictionary<string, object> GetUserByUsername(string username)
        {
            var user = new Dictionary<string, object>();

            using (var conn = new SqlConnection(this.connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Get_User_By_Username", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Add("Id", reader["Id"]);
                            user.Add("Username", reader["Username"]);
                            user.Add("Password", reader["PasswordHash"]);
                        }
                    }

                }
            }
            return user;
        }

        public Dictionary<string, object> GetUserByID(int id)
        {
            var user = new Dictionary<string, object>();

            using (var conn = new SqlConnection(this.connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Get_User_By_ID", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Add("Id", reader["Id"]);
                            user.Add("Username", reader["Username"]);
                            user.Add("Password", reader["PasswordHash"]);
                        }
                    }

                }
            }
            return user;
        }

    }
}
