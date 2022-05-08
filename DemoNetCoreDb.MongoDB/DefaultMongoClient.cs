using MongoDB.Driver;

namespace DemoNetCoreDb.MongoDB
{
    public class DefaultMongoClient : MongoClient
    {
        public DefaultMongoClient(MongoClientSettings settings) : base(settings) { }
        public DefaultMongoClient(MongoUrl url) : base(url) { }
        public DefaultMongoClient(string connectionString) : base(connectionString) { }
    }
}
