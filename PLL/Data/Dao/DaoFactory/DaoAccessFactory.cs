using PLL.Data.Dao.Interfaces;

namespace PLL.Data.Dao.DaoFactory
{
    public abstract class DaoAccessFactory
    {
        protected readonly ILoggerFactory _loggerFactory;

        protected DaoAccessFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public abstract IDaoAccessor GetAccessor();
    }
}
