using System.Data.SqlClient;

namespace PLL.Data.Specification
{
    public interface ISpecification<TEntity>
    {
        SqlCommand CreateCommand(SqlConnection connection);
        TEntity MapEntity(SqlDataReader reader);
    }
}
