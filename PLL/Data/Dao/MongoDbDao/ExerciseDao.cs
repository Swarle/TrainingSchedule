using MongoDB.Bson;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.MongoDbDao
{
    public class ExerciseDao : MongoAbstractDao<Exercise>
    {
        private readonly ExerciseBuilder _builder;
        public ExerciseDao(ILogger logger) : base(logger)
        {
            _builder = new ExerciseBuilder();
        }

        protected override string CollectionName => nameof(Exercise);

        protected override List<Exercise> MapFromBsonToEntities(List<BsonDocument> bsonList)
        {
            var list = new List<Exercise>();

            foreach (var doc in bsonList)
            {
                list.Add(_builder
                    .AddId(doc["_id"].AsObjectId.ToString())
                    .AddExerciseName(doc["exercise_name"].AsString)
                    .AddMuscleGroupId(doc["muscle_group_id"].AsObjectId.ToString())
                    .Build()
                );

                _builder.Reset();
            }

            return list;
        }

        protected override BsonDocument MapFromEntityToBson(Exercise entity)
        {
            return new BsonDocument
            {
                { "exercise_name", new BsonString(entity.ExerciseName) },
                { "muscle_group_id", new BsonObjectId(entity.MuscleGroupId) }
            };
        }
    }
}
