using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace PLL.Data.Infastracture
{
    public class DbConnectionManager
    {
        private static DbConnectionManager _instance;
        private SqlConnection? _connection;
        private readonly IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json",false,true)
            .Build();
        private static readonly object _lock = new object();

        private DbConnectionManager() {}
        
        public static DbConnectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DbConnectionManager();
                            return _instance;
                        }
                    }
                }
                return _instance;
            }
        }

        public SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                var connectionString = _configuration.GetConnectionString("Default");

                _connection = new SqlConnection(connectionString);

                return _connection;
            }

            return _connection;
        }

        public async Task OpenConnectionAsync()
        {
            if (_connection != null && _connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
        }

        public void OpenConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private async Task CloseConnectionAsync()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                await _connection.CloseAsync();
                _connection = null;
            }
        }

        private void CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
                _connection = null;
            }
        }

        ~DbConnectionManager()
        {
            CloseConnection();
        }

    }
}
