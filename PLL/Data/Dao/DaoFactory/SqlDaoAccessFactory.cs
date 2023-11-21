using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.SqlDao;

namespace PLL.Data.Dao.DaoFactory
{
    public class SqlDaoAccessFactory : DaoAccessFactory
    {
        public SqlDaoAccessFactory(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        public override IDaoAccessor GetAccessor()
        {
            return new SqlDaoAccessor(_loggerFactory);
        }
    }
}
