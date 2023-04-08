using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Robbie.Repositories.MongoConfiguration
{
    public interface IMongoDocument
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public class MongoDocument : IMongoDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; } = default!;

        public string TenantId { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
