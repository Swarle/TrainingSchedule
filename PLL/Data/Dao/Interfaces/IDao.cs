using PLL.Data.Entity;
using PLL.Data.Specification;

namespace PLL.Data.Dao.Interfaces
{
    public interface IDao<TEntity> where TEntity : IEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(string id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(string id);
        Task<TEntity?> FindSingle(ISpecification<TEntity> specification);
        Task DeleteAllAsync(string tableName);
    }
}
