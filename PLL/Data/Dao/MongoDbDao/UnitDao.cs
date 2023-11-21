using MongoDB.Bson;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.MongoDbDao
{
    public class UnitDao : MongoAbstractDao<Unit>
    {
        private readonly UnitBuilder _builder;
        public UnitDao(ILogger logger) : base(logger)
        {
            _builder = new UnitBuilder();
        }

        protected override string CollectionName => nameof(Unit);
        protected override List<Unit> MapFromBsonToEntities(List<BsonDocument> bsonList)
        {
            var list = new List<Unit>();

            foreach (var doc in bsonList)
            {
                list.Add(_builder
                    .AddId(doc["_id"].AsObjectId.ToString())
                    .AddUnitName(doc["unit_name"].AsString)
                    .Build());

                _builder.Reset();
            }

            return list;
        }

        protected override BsonDocument MapFromEntityToBson(Unit entity)
        {
            return new BsonDocument
            {
                { "unit_name", new BsonString(entity.UnitName) }
            };
        }
    }
}
