using MongoDB.Bson;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.MongoDbDao
{
    public class TrainingDao : MongoAbstractDao<Training>
    {
        protected override string CollectionName => nameof(Training);
        private readonly TrainingBuilder _builder;

        public TrainingDao(ILogger logger) : base(logger)
        {
            _builder = new TrainingBuilder();
        }

        protected override List<Training> MapFromBsonToEntities(List<BsonDocument> bsonList)
        {
            var list = new List<Training>();

            foreach (var doc in bsonList)
            {
                list.Add(_builder
                    .AddId(doc["_id"].AsObjectId.ToString())
                    .AddDate(doc["date"].ToUniversalTime())
                    .Build()
                );

                _builder.Reset();
            }

            return list;
        }

        protected override BsonDocument MapFromEntityToBson(Training entity)
        {
            var doc = new BsonDocument
            {
                { "date", entity.Date }
            };

            return doc;
        }
    }
}
