using System.Data.SqlClient;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Infastracture;
using PLL.Data.Observer.Interfaces;
using PLL.Data.Specification;

namespace PLL.Data.Dao.SqlDao
{
    public enum DaoState
    {
        Get,
        GetById,
        Update,
        Create,
        Delete
    }

    public abstract class SqlDaoAbstract<TEntity> : ISubject, IDao<TEntity> where TEntity : IEntity

    {
    private readonly DbConnectionManager _connectionManager;
    protected readonly SqlConnection _connection;
    private readonly ILogger _logger;
    protected virtual string SelectAllRequest { get; }
    protected virtual string SelectByIdRequest { get; }
    protected virtual string InsertRequest { get; }
    protected virtual string UpdateRequest { get; }
    protected virtual string DeleteRequest { get; }

    public DaoState _state { get; set; }

    private List<IObserver> _observers = new List<IObserver>();

    protected SqlDaoAbstract(ILogger logger)
    {
        _logger = logger;
        _connectionManager = DbConnectionManager.Instance;
        _connection = _connectionManager.GetConnection();
        _connectionManager.OpenConnection();
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var list = new List<TEntity>();

        var command = new SqlCommand(SelectAllRequest, _connection);

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

        _state = DaoState.Get;

        Notify();

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

        _state = DaoState.GetById;

        Notify();

        return list.FirstOrDefault();
    }

    public Task CreateAsync(TEntity entity)
    {
        var command = ToSqlRequest(entity, InsertRequest);

        _logger.LogInformation("Executing Sql-Query: {0}", command.CommandText);

        command.ExecuteNonQuery();

        _state = DaoState.Create;

        Notify();

            return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity)
    {
        var command = ToSqlRequest(entity, UpdateRequest);

        command.Parameters.AddWithValue("@Id", entity.Id);

        _logger.LogInformation("Executing Sql-Query: {0}", command.CommandText);

        command.ExecuteNonQuery();

        _state = DaoState.Update;

        Notify();

            return Task.CompletedTask;
    }

    public Task DeleteAsync(string id)
    {
        var command = new SqlCommand(DeleteRequest, _connection);

        command.Parameters.AddWithValue("@Id", id);

        _logger.LogInformation("Executing Sql-Query: {0}", command.CommandText);

        command.ExecuteNonQuery();

        _state = DaoState.Delete;

        Notify();

        return Task.CompletedTask;
    }

    public async Task<TEntity?> FindSingle(ISpecification<TEntity> specification)
    {
        var list = new List<TEntity>();

        var command = specification.CreateCommand(_connection);

        await using (var reader = await command.ExecuteReaderAsync())
        {
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(specification.MapEntity(reader));
                }
            }
        }

        if (list.Count > 1)
            throw new InvalidOperationException("Очікувалася лише одна сутність, але отримано більше одного рядка.");

        _state = DaoState.GetById;

        Notify();

        return list.FirstOrDefault();
        }

    protected abstract TEntity MapDataReaderToEntity(SqlDataReader reader);
    protected abstract SqlCommand ToSqlRequest(TEntity entity, string request);
    }

    
}
