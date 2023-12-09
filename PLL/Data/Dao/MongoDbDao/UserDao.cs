using MongoDB.Bson;
using PLL.Data.Builder;
using PLL.Data.Entity;

namespace PLL.Data.Dao.MongoDbDao
{
    public class UserDao : MongoAbstractDao<User>
    {
        public UserDao(ILogger logger) : base(logger)
        {
        }

        protected override string CollectionName => nameof(User);
        protected override List<User> MapFromBsonToEntities(List<BsonDocument> bsonList)
        {
            var list = new List<User>();

            foreach (var doc in bsonList)
            {
                list.Add(new User
                {
                    Id = doc["_id"].AsObjectId.ToString(),
                    Login = doc["login"].AsString,
                    Password = doc["password"].AsString,
                    Email = doc["email"].AsString,
                    Age = doc["age"].AsInt32,
                    RoleId = doc["role_id"].AsObjectId.ToString()
                });
            }

            return list;
        }

        protected override BsonDocument MapFromEntityToBson(User entity)
        {
            return new BsonDocument
            {
                { "login", new BsonString(entity.Login) },
                {"password",new BsonString(entity.Login)},
                {"email", new BsonString(entity.Email)},
                {"age",new BsonInt32(entity.Age)},
                {"role_id",new BsonObjectId(entity.RoleId)}
            };
        }

        public void CreateManyAsync(List<User> users)
        {
            var bsonUsers = new List<BsonDocument>();

            foreach (var user in users)
            {
                bsonUsers.Add(MapFromEntityToBson(user));
            }

            _collection.InsertMany(bsonUsers);
        }
    }
}
