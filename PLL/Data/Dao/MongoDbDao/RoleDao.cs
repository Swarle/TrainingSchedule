using MongoDB.Bson;
using PLL.Data.Entity;

namespace PLL.Data.Dao.MongoDbDao
{
    public class RoleDao : MongoAbstractDao<Role>
    {
        public RoleDao(ILogger logger) : base(logger)
        {
        }

        protected override string CollectionName => nameof(Role);
        protected override List<Role> MapFromBsonToEntities(List<BsonDocument> bsonList)
        {
            var list = new List<Role>();

            foreach (var doc in bsonList)
            {
                list.Add(new Role
                {
                    Id = doc["_id"].AsObjectId.ToString(),
                    RoleName = doc["role_name"].AsString
                });
            }

            return list;
        }

        protected override BsonDocument MapFromEntityToBson(Role entity)
        {
            return new BsonDocument
            {
                {"role_name",new BsonString(entity.RoleName)}
            };
        }
    }
}
