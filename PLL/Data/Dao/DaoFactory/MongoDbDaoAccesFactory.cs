using PLL.Data.Dao.Interfaces;
using PLL.Data.Dao.MongoDbDao;

namespace PLL.Data.Dao.DaoFactory
{
    public class MongoDbDaoAccesFactory : DaoAccessFactory
    {
        public MongoDbDaoAccesFactory(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        public override IDaoAccessor GetAccessor()
        {
            return new MongoDbAccessor(_loggerFactory);
        }
    }
}
