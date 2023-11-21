﻿using MongoDB.Bson;
using MongoDB.Driver;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;
using PLL.Data.Infastracture;

namespace PLL.Data.Dao.MongoDbDao
{
    public abstract class MongoAbstractDao<TEntity> : IDao<TEntity> where TEntity : IEntity
    {
        private ILogger _logger;

        protected IMongoCollection<BsonDocument> _collection;
        protected abstract string CollectionName { get; }

        public MongoAbstractDao(ILogger logger)
        {
            _logger = logger;
            _collection = MongoDbConnectionManager.Instance.GetDatabase()
                .GetCollection<BsonDocument>(CollectionName);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var bsonList = await _collection.Find(new BsonDocument()).ToListAsync();
            
            var list = MapFromBsonToEntities(bsonList);

            return list;
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            var bsonDoc = await _collection.Find(new BsonDocument{ {"_id", new ObjectId(id)} }).ToListAsync();

            var entity = MapFromBsonToEntities(bsonDoc).FirstOrDefault();

            return entity;
        }  

        public async Task CreateAsync(TEntity entity)
        {
            var bson = MapFromEntityToBson(entity);

            await _collection.InsertOneAsync(bson);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var filter = MapFromEntityToBson(await GetByIdAsync(entity.Id));

            var updateSettings = new BsonDocument("$set", MapFromEntityToBson(entity));

            await _collection.UpdateOneAsync(filter, updateSettings);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(new BsonDocument{{"_id",new ObjectId(id)}});
        }

        protected abstract List<TEntity> MapFromBsonToEntities(List<BsonDocument> bsonList);

        protected abstract BsonDocument MapFromEntityToBson(TEntity entity);

    }
}