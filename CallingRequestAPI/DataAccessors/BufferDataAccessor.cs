using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CallingRequestAPI.DataAccessors
{
    public class BufferDataAccessor
    {
        /// <summary>
        /// Connection string for connecting to the server
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// For configuration of connection
        /// </summary>
        private IConfiguration configuration;

        public BufferDataAccessor()
        {
            this.configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").Build();
            connectionString = configuration["ConnectionStrings:Connection"];
        }

        #region Buffer

        public bool InsertBuffer(Dictionary<string, object> buffer)
        {
            var user = new Dictionary<string, object>();

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Calling", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Insert_Buffer");
                    cmd.Parameters.AddWithValue("@senderID", buffer["SenderID"]);
                    cmd.Parameters.AddWithValue("@receiverID", buffer["ReceiverID"]);
                    cmd.Parameters.AddWithValue("@traffic", buffer["Traffic"]);

                    var ans = cmd.ExecuteNonQuery();
                    return ans == 1;
                }
            }
        }

        public bool DeleteBuffer(Dictionary<string, object> buffer)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Calling", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Delete_Buffer");
                    cmd.Parameters.AddWithValue("@senderID", buffer["SenderID"]);
                    cmd.Parameters.AddWithValue("@receiverID", buffer["ReceiverID"]);

                    var ans = cmd.ExecuteNonQuery();
                    return ans >= 1;
                }
            }
        }

        public Dictionary<string, object> GetBuffer(int senderID,int receiverID)
        {
            var request = new Dictionary<string, object>();

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Calling", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Get_Buffer");
                    cmd.Parameters.AddWithValue("@senderID", senderID);
                    cmd.Parameters.AddWithValue("@receiverID", receiverID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            request.Add("SenderID", reader["SenderID"]);
                            request.Add("ReceiverID", reader["ReceiverID"]);
                            request.Add("Traffic", reader["Traffic"]);
                        }
                    }
                }
            }

            return request;
        }

        #endregion
    }
}
