using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace UserAPI.DataAccess
{
    public class ValidationDataAccessor
    {
        /// <summary>
        /// Connection string for connecting to the server
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// For configuration of connection
        /// </summary>
        private IConfiguration configuration;

        public ValidationDataAccessor()
        {
            this.configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").Build();
            connectionString = configuration["ConnectionStrings:Connection"];
        }

        public bool InsertValidationCode(string userEmail,string code)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Instert_Code", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@userEmail", userEmail);
                command.Parameters.AddWithValue("@code", code);

                return command.ExecuteNonQuery() != 0;
            }
        }

        public bool CheckValidationCode(string userEmail, string code)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Check_Code", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@userEmail", userEmail);
                command.Parameters.AddWithValue("@code", code);

                using (var reader = command.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public bool DeleteValidationCode(string userEmail)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_Delete_Code", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@userEmail", userEmail);

                return command.ExecuteNonQuery() != 0;
            }
        }
    }
}
