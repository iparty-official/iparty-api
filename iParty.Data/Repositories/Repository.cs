using iParty.Business.Models;
using iParty.Business.Models.Addresses;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace iParty.Data.Repositories
{
    public class Repository<TEntity> where TEntity : Entity
    {                       
        private IMongoCollection<TEntity> _collection { get; set; }

        public Repository(string connectionString, string databaseAlias)
        {
            //("mongodb+srv://iparty:admin@iparty.mdem9.mongodb.net/iParty?retryWrites=true&w=majority");

            var settings = MongoClientSettings.FromConnectionString(connectionString);
                           
            var client = new MongoClient(settings);

            var database = client.GetDatabase(databaseAlias);

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

        public void Delete(City city)
        {
            _collection.DeleteOne(null);
        }

        public void Recover(City city)
        {
            _collection.Find(null);
        }

        public void RecoverById(City city)
        {
            _collection.Find(null);
        }
    }
}
