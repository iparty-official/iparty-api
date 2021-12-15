using iParty.Business.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace iParty.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
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

        public void Update(Guid id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);

            entity.Id = id;

            _collection.ReplaceOne(filter, entity);
        }

        public void Delete(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);

            var update = Builders<TEntity>.Update.Set("Removed", true);

            _collection.UpdateOne(filter, update);
        }

        public List<TEntity> Recover(Expression<Func<TEntity, bool>> field)
        {
            var filter = Builders<TEntity>.Filter.Eq(field, false);

            return _collection.Find(filter).ToList();
        }

        public List<TEntity> Recover()
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Removed, false);

            return _collection.Find(filter).ToList();
        }

        public TEntity RecoverById(Guid id)
        {
            var builder = Builders<TEntity>.Filter;

            var filter = builder.And(builder.Eq("_id", id), builder.Eq("Removed", false));

            return _collection.Find(filter).FirstOrDefault<TEntity>();
        }
    }
}