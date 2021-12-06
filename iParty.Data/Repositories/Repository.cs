using iParty.Business.Models;
using MongoDB.Driver;

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
            _collection.UpdateOne(null, null);
        }

        public void Delete(TEntity entity)
        {
            _collection.DeleteOne(null);
        }

        public void Recover(TEntity entity)
        {
            _collection.Find(null);
        }

        public void RecoverById(TEntity entity)
        {
            _collection.Find(null);
        }
    }
}
