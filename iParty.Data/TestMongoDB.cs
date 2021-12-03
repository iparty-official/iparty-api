using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace iParty.Data
{
    public class TestMongoDB
    {
        public async Task TestAsync()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://iparty:<password>@iparty.mdem9.mongodb.net/iParty?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("iParty");

            var collection = database.GetCollection<BsonDocument>("Document");

            var document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            };

            await collection.InsertOneAsync(document);
        }
    }
}
