using iParty.Business.Models;
using iParty.Business.Models.Addresses;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace iParty.Data.Repositories
{
    public class CityRepository<TEntity> where TEntity : Entity
    {
        private MongoClientSettings _settings { get; set; }
        
        private MongoClient _client { get; set; }

        public IMongoDatabase _database { get; set; }                

        public CityRepository()
        {
            _settings = MongoClientSettings.FromConnectionString("mongodb+srv://iparty:admin@iparty.mdem9.mongodb.net/iParty?retryWrites=true&w=majority");

            _client = new MongoClient(_settings);

            _database = _client.GetDatabase("iParty");           
        }

        public void Create(TEntity entity)
        {
            var collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);

            collection.InsertOne(entity);
        }

        public void Update(City city)
        {
            throw new NotImplementedException();
        }

        public void Delete(City city)
        {
            throw new NotImplementedException();
        }

        public void Recover(City city)
        {
            throw new NotImplementedException();
        }

        public void RecoverById(City city)
        {
            throw new NotImplementedException();
        }
    }
}
