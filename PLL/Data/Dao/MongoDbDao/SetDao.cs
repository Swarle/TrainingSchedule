using MongoDB.Bson;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.MongoDbDao
{
    public class SetDao : MongoAbstractDao<Set>
    {
        private readonly SetBuilder _builder;

        public SetDao(ILogger logger) : base(logger)
        {
            _builder = new SetBuilder();
        }

        protected override string CollectionName => nameof(Set);
        protected override List<Set> MapFromBsonToEntities(List<BsonDocument> bsonList)
        {
            var list = new List<Set>();

            foreach (var doc in bsonList)
            {
                list.Add(_builder
                    .AddId(doc["_id"].AsObjectId.ToString())
                    .AddNumberRepetition(doc["number_repetition"].AsInt32)
                    .AddWeight(doc["weight"].AsInt32)
                    .AddExerciseId(doc["exercise_id"].AsObjectId.ToString())
                    .AddUnitId(doc["unit_id"].AsObjectId.ToString())
                    .Build());

                _builder.Reset();
            }

            return list;
        }

        protected override BsonDocument MapFromEntityToBson(Set entity)
        {
            return new BsonDocument
            {
                { "number_repetition", new BsonInt32(entity.NumberRepetitons) },
                { "weight", new BsonInt32(entity.Weight) },
                { "exercise_id", new BsonObjectId(entity.ExerciseId) },
                { "unit_id", new BsonObjectId(entity.UnitId) }
            };
        }
    }
}
