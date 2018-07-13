using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CallingDAL
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

        #region Request

        public bool InsertRequest(Dictionary<string,object> request)
        {
            var user = new Dictionary<string, object>();

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Calling", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Insert_Request");
                    cmd.Parameters.AddWithValue("@senderID", request["SenderID"]);
                    cmd.Parameters.AddWithValue("@receiverID", request["ReceiverID"]);
                    cmd.Parameters.AddWithValue("@stateID", request["StateID"]);

                    var ans = cmd.ExecuteNonQuery();
                    return ans == 1;
                }
            }
        }

        public bool DeleteRequests(Dictionary<string, object> request)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Calling", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Delete_Request");
                    cmd.Parameters.AddWithValue("@senderID", request["SenderID"]);
                    cmd.Parameters.AddWithValue("@receiverID", request["ReceiverID"]);

                    var ans = cmd.ExecuteNonQuery();
                    return ans >= 1;
                }
            }
        }

        public IEnumerable<Dictionary<string, object>> GetRequests(int receiverID)
        {
            var requests = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Calling", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Get_Request");
                    cmd.Parameters.AddWithValue("@receiverID", receiverID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var request = new Dictionary<string, object>();
                            request.Add("SenderID", reader["SenderID"]);
                            request.Add("ReceiverID", reader["ReceiverID"]);
                            request.Add("StateID", reader["StateID"]);

                            requests.Add(request);
                        }
                    }
                }
            }

            return requests;
        }

        #endregion

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

        public Dictionary<string, object> GetBuffer(int receiverID)
        {
            var request = new Dictionary<string, object>();

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("sp_Calling", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "Get_Buffer");
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
