using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Services.Models.Contracts
{
    public enum BusinessModule
    {
        [BsonRepresentation(BsonType.String)]
        Ground,
        [BsonRepresentation(BsonType.String)]
        Lift,
        [BsonRepresentation(BsonType.String)]
        Fire,
        [BsonRepresentation(BsonType.String)]
        Condominium
    }

    public class BusinessDocumentRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string UserId { get; set; }
        public bool? IsOpened { get; set; }
        public string[] SupplierIds { get; set; }
    }
}

