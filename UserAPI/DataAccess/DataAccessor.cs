using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace UserAPI.DataAccess
{
    public class DataAccessor
    {
        /// <summary>
        /// Connection string for connecting to the server
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// For configuration of connection
        /// </summary>
        private IConfiguration configuration;

        public DataAccessor()
        {
            this.configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").Build();
            connectionString = configuration["ConnectionStrings:Connection"];
        }

        #region getters

        /// <summary>
        /// Gets all users from storage
        /// </summary>
        /// <returns>Users collection</returns>
        public IEnumerable<Dictionary<string, object>> GetAllUsers()
        {
            var users = new List<Dictionary<string, object>>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Get_All_Users", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                
                using (var reader = command.ExecuteReader())
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
            return users;
        }

        /// <summary>
        /// Gets the user by given id
        /// </summary>
        /// <returns>User</returns>
        public Dictionary<string, object> GetUserByID(int id)
        {
            Dictionary<string, object> user = null;

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Get_User_By_ID", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new Dictionary<string, object>();
                        user.Add("Id", id);
                        user.Add("Name", reader["Name"]);
                        user.Add("Lastname", reader["Lastname"]);
                        user.Add("Username", reader["Username"]);
                        user.Add("PasswordHash", reader["PasswordHash"]);
                        user.Add("Email", reader["Email"]);
                        user.Add("CreateDate", reader["CreateDate"]);
                        user.Add("LastLoginDate", reader["LastLoginDate"]);
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Gets the user by given username
        /// </summary>
        /// <returns>User</returns>
        public Dictionary<string, object> GetUserByUsername(string username)
        {
            var user = new Dictionary<string, object>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Get_User_By_Username", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@username", username);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Add("Id", reader["Id"]);
                        user.Add("Name", reader["Name"]);
                        user.Add("Lastname", reader["Lastname"]);
                        user.Add("Username", username);
                        user.Add("PasswordHash", reader["PasswordHash"]);
                        user.Add("Email", reader["Email"]);
                        user.Add("CreateDate", reader["CreateDate"]);
                        user.Add("LastLoginDate", reader["LastLoginDate"]);
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Gets the Email of given user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>Email</returns>
        public string GetEmail(int id)
        {
            var email = "";
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Get_Email", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        email = (string)reader["Email"];
                    }
                }
            }
            return email;
        }

        /// <summary>
        /// Gets the Last login of given user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>Last login</returns>
        public DateTime GetLastLogin(int id)
        {
            var lastLogin = DateTime.Now;
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Get_Last_Login", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lastLogin = (DateTime)reader["LastLoginDate"];
                    }
                }
            }
            return lastLogin;
        }

        #endregion

        #region creator

        /// <summary>
        /// Creates new user in storage
        /// </summary>
        /// <param name="user">New user</param>
        /// <returns>if user is created retruns true, else false</returns>
        public bool CreateUser(Dictionary<string, object> user)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Create_User", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@name", user["Name"]);
                command.Parameters.AddWithValue("@lastname", user["Lastname"]);
                command.Parameters.AddWithValue("@username", user["Username"]);
                command.Parameters.AddWithValue("@passwordhash", user["PasswordHash"]);
                command.Parameters.AddWithValue("@email", user["Email"]);

                return command.ExecuteNonQuery() != 0;
            }
        }

        #endregion

        #region deletor

        /// <summary>
        /// Deletes the specific user from storage
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>if user is deleted returns true, else false</returns>
        public bool DeleteUser(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Delete_User", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@id", id);

                return command.ExecuteNonQuery() != 0;
            }
        }

        #endregion

        #region changers

        /// <summary>
        /// Updates the specific user's password in storage
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="passwordhash">new password</param>
        /// <returns>if user's password is updated returns true, else false</returns>
        public bool UpdateUserPassword(int id, string passwordhash)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Update_Password", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@passwordhash", passwordhash);

                return command.ExecuteNonQuery() != 0;
            }
        }

        /// <summary>
        /// Updates the specific user's last login date in storage
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>if user's last login date is updated returns true, else false</returns>
        public bool UpdateUserLastLogin(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Update_Last_Login_Date", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@id", id);

                return command.ExecuteNonQuery() != 0;
            }
        }

        #endregion
    }
}
