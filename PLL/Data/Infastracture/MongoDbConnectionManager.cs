using MongoDB.Driver;

namespace PLL.Data.Infastracture
{
    public class MongoDbConnectionManager
    {
        private static MongoDbConnectionManager _instance;
        private MongoClient? _client;
        private IMongoDatabase? _database;
        private readonly IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        private static readonly object _lock = new object();

        private MongoDbConnectionManager() { }

        public static MongoDbConnectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MongoDbConnectionManager();
                            return _instance;
                        }
                    }
                }
                return _instance;
            }
        }

        public MongoClient GetClient()
        {
            if (_client == null) 
                _client = new MongoClient(_configuration.GetConnectionString("MongoDb"));
            
            return _client;
        }

        public IMongoDatabase GetDatabase()
        {
            if (_client == null)
                GetClient();

            if (_database == null)
                _database = _client.GetDatabase("TrainingSchedule");

            return _database;
        }

    }
}
