using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Repositories.MongoConfiguration;
using Repositories.NestedDocuments;

namespace Repositories
{
    // Assemblee
    public class Assemblee : BaseTicketDocument
    {
        // DATA ASSEMBLEA
        public DateTime AssembleeDate { get; set; }
        // NOMINATIVO
        public String Label { get; set; } = "";
        // DELIBERE
        public List<Resolution> Resolutions { get; set; }
        // Tipologia Assemblea
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssembleeType AssembleeType { get; set; }

    }

    public enum AssembleeType
    {
        // Ordinaria
        [BsonRepresentation(BsonType.String)]
        Ordinary,
        // Straordinaria
        [BsonRepresentation(BsonType.String)]
        ExtraOrdinary,
    }

    // Deliberee
    public class Resolution
    {
        // Tipologia di delibera
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResolutionType ResolutionType { get; set; }

        // Riferimento Responsabile
        public String? Reference { get; set; }
        public String Description { get; set; } = "";

        // Priorità
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PriorityType Priority { get; set; }

        // Business Area
        public Area Area { get; set; }

        public String? TicketId { get; set; }
        public String? Note { get; set; }

        // Note
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResolutionState State { get; set; }
    }

    public enum ResolutionType
    {
        // Argomento prossima riunione
        [BsonRepresentation(BsonType.String)]
        AssembleeNextTopic,
        // Comunicazione ai condomini
        [BsonRepresentation(BsonType.String)]
        MessageToPeople,
        // Comunicazione ai fornitori
        [BsonRepresentation(BsonType.String)]
        MessageToSuppliers,
        // Conferma d’ordine
        [BsonRepresentation(BsonType.String)]
        OrderConfirmation,
        // Contabilità
        [BsonRepresentation(BsonType.String)]
        Accounting,
        // Intervento
        [BsonRepresentation(BsonType.String)]
        OrdinaryMaintenance,
        // Manutenzione straordinaria
        [BsonRepresentation(BsonType.String)]
        ExtraOrdinaryMaintenance,
        // Sopralluogo
        [BsonRepresentation(BsonType.String)]
        Inspection,
        // Preventivo
        [BsonRepresentation(BsonType.String)]
        Estimate,
    }

    public enum PriorityType
    {
        [BsonRepresentation(BsonType.String)]
        High,

        [BsonRepresentation(BsonType.String)]
        Medium,

        [BsonRepresentation(BsonType.String)]
        Low,
    }

    public enum Area
    {
        // Intervento
        [BsonRepresentation(BsonType.String)]
        Job,
    }

    public enum ResolutionState
    {
        // Aperto
        [BsonRepresentation(BsonType.String)]
        Opened,
        // In lavorazione
        [BsonRepresentation(BsonType.String)]
        Working,
        // Chiuso
        [BsonRepresentation(BsonType.String)]
        Closed,
    }

}
