using System.Data.SqlClient;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Infastracture;

namespace PLL.Data.Dao.SqlDao
{
    public abstract class SqlDaoAbstract<TEntity> : IDao<TEntity> where TEntity : IEntity
    {
        private readonly DbConnectionManager _connectionManager;
        protected readonly SqlConnection _connection;
        private readonly ILogger _logger;
        protected virtual string SelectAllRequest { get; }
        protected virtual string SelectByIdRequest { get; }
        protected virtual string InsertRequest { get; }
        protected virtual string UpdateRequest { get; }
        protected virtual string DeleteRequest { get; }

        protected SqlDaoAbstract(ILogger logger)
        {
            _logger = logger;
            _connectionManager = DbConnectionManager.Instance;
            _connection = _connectionManager.GetConnection();
            _connectionManager.OpenConnection();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var list = new List<TEntity>();

            var command = new SqlCommand(SelectAllRequest, _connection);

            _logger.LogInformation("Executing Sql-Query: {0}",command.CommandText);

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(MapDataReaderToEntity(reader));
                    }
                }
            }

            return list;
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            var list = new List<TEntity>();

            var command = new SqlCommand(SelectByIdRequest, _connection);

            command.Parameters.AddWithValue("@Id", id);

            _logger.LogInformation("Executing Sql-Query: {0}", command.CommandText);

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(MapDataReaderToEntity(reader));
                    }
                }
            }

            if (list.Count > 1)
                throw new InvalidOperationException("Очікувалася лише одна сутність, але отримано більше одного рядка.");

            return list.FirstOrDefault();
        }

        public Task CreateAsync(TEntity entity)
        {
            var command = ToSqlRequest(entity);

            _logger.LogInformation("Executing Sql-Query: {0}", command.CommandText);

            command.ExecuteNonQuery();

            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            var command = ToSqlRequest(entity);

            command.Parameters.AddWithValue("@Id", entity.Id);

            _logger.LogInformation("Executing Sql-Query: {0}", command.CommandText);

            command.ExecuteNonQuery();

            return Task.CompletedTask;
        }

        public Task DeleteAsync(string id)
        {
            var command = new SqlCommand(DeleteRequest, _connection);

            command.Parameters.AddWithValue("@Id", id);

            _logger.LogInformation("Executing Sql-Query: {0}", command.CommandText);

            command.ExecuteNonQuery();

            return Task.CompletedTask;
        }

        protected abstract TEntity MapDataReaderToEntity(SqlDataReader reader);
        protected abstract SqlCommand ToSqlRequest(TEntity entity);
    }
}
