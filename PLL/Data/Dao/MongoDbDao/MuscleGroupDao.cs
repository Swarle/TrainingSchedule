using MongoDB.Bson;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.MongoDbDao
{
    public class MuscleGroupDao : MongoAbstractDao<MuscleGroup>
    {
        private readonly MuscleGroupBuilder _builder;
        public MuscleGroupDao(ILogger logger) : base(logger)
        {
            _builder = new MuscleGroupBuilder();
        }

        protected override string CollectionName => nameof(MuscleGroup);

        protected override List<MuscleGroup> MapFromBsonToEntities(List<BsonDocument> bsonList)
        {
            var list = new List<MuscleGroup>();

            foreach (var doc in bsonList)
            {
                list.Add(_builder
                    .AddId(doc["_id"].AsObjectId.ToString())
                    .AddGroupName(doc["group_name"].AsString)
                    .Build());

                _builder.Reset();
            }

            return list;
        }

        protected override BsonDocument MapFromEntityToBson(MuscleGroup entity)
        {
            return new BsonDocument
            {
                { "group_name", new BsonString(entity.GroupName) },
                {"training_id", new BsonObjectId(entity.TrainingId)}
            };
        }
    }
}
