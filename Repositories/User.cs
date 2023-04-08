using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Robbie.Repositories.MongoConfiguration;

namespace Repositories
{
    public class User : MongoDocument
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }

        public List<string> SupplierIds { get; set; } = new();
    }

    public enum Role
    {
        // Amministratore
        [BsonRepresentation(BsonType.String)]
        Admin,
        // Editore
        [BsonRepresentation(BsonType.String)]
        Editor,
        // Fornitore
        [BsonRepresentation(BsonType.String)]
        Supplier,
        // Fornitore Solo letture
        [BsonRepresentation(BsonType.String)]
        SupplierReviewer
    }
}


