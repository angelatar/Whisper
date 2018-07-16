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

        public bool InsertValidationCode(int userID,string code)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("[sp_Validator]", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@Mode", "Instert_Code");
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@code", code);

                return command.ExecuteNonQuery() != 0;
            }
        }

        public bool CheckValidationCode(int userID, string code)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("[sp_Validator]", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@Mode", "Check_Code");
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@code", code);

                using (var reader = command.ExecuteReader())
                {
                    return reader != null;
                }
            }
        }

        public bool DeleteValidationCode(int userID)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("[sp_Validator]", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@Mode", "Delete_Code");
                command.Parameters.AddWithValue("@userID", userID);

                return command.ExecuteNonQuery() != 0;
            }
        }
    }
}
