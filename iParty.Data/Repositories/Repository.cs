using iParty.Business.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace iParty.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {                       
        private IMongoCollection<TEntity> _collection { get; set; }

        public Repository(DatabaseConfig databaseConfig)
        {            
            var settings = MongoClientSettings.FromConnectionString(databaseConfig.ConnectionString);

            var client = new MongoClient(settings);

            var database = client.GetDatabase(databaseConfig.DatabaseAlias);

            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public void Create(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
            
            _collection.ReplaceOne(filter, entity);
        }

        public void Delete(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);

            var update = Builders<TEntity>.Update.Set("Removed", true);

            _collection.UpdateOne(filter, update);
        }

        public List<TEntity> Recover()
        {
            var filter = Builders<TEntity>.Filter.Empty;
            
            return _collection.Find(filter).ToList();
        }

        public TEntity RecoverById(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);

            return _collection.Find(filter).FirstOrDefault<TEntity>();
        }
    }
}
