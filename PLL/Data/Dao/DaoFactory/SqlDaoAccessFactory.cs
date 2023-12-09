using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.SqlDao;
using PLL.Data.Observer.Interfaces;

namespace PLL.Data.Dao.DaoFactory
{
    public class SqlDaoAccessFactory : DaoAccessFactory
    {
        private readonly IObserver _observer;
        public SqlDaoAccessFactory(ILoggerFactory loggerFactory, IObserver observer) : base(loggerFactory)
        {
            _observer = observer;
        }

        public override IDaoAccessor GetAccessor()
        {
            return new SqlDaoAccessor(_loggerFactory);
        }
    }
}
