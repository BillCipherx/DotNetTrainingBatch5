using System.Data;
using System.Data.SqlClient;

namespace DotNetTrainingBatch5.Shared
{
    public class AdoDotNetService
    {

        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (var sqlParamater in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParamater.Name, sqlParamater.Value);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            return dt;
        }

        public int Execute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (var sqlParamater in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParamater.Name, sqlParamater.Value);
                }
            }

            var result = cmd.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }

    public class SqlParameterModel
    { 
        public string Name { get; set; }
        public object Value { get; set; }
        public SqlParameterModel(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
