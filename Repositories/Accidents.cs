using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Repositories.MongoConfiguration;
using Repositories.NestedDocuments;

namespace Repositories
{
    // Gestione sinistri
    public class Accident : SupplierTicketDocument
    {
        // Tipologia sinistri
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Type Type { get; set; }

        // CONDOMINO
        public Person Person { get; set; }

        // DATA APERTURA SINISTRO
        public DateTime OpenDate { get; set; }

        // DATA INVIO DOCUMENTAZIONE FOTOGRAFICA
        public DateTime? SendPhotosDate { get; set; }

        // DATA INVIO SPESE DI RICERCA E RIPARAZIONE
        public DateTime? FixAndSeekingDate { get; set; }

        // DATA INVIO SPESE DI RIPARAZIONE DANNEGGIATO
        public DateTime? SendFixDamageDate { get; set; }

        // DATA INVIO DOCUMENTAZIONE AL PERITO
        public DateTime? SendDocToConsultantDate { get; set; }

        // DATA CHIUSURA SINISTRO
        public DateTime? CloseDate { get; set; }

        // SINISTRO INDENNIZZATO
        public bool Compensated { get; set; }

        // IMPORTO TOTALE SPESE SOSTENUTE PER IL SINISTRO
        public int AmountAccident { get; set; }

        // IMPORTO TOTALE LIQUIDATO PER IL SINISTRO
        public int AmountPayOff { get; set; }

        // SINISTRO INDENNIZZATO MA NON FIRMATO L'ATTO DI ACCERTAMENTO
        public bool PaidButNotVerified { get; set; }

        // SINISTRO NON INDENNIZZATO ATTO DI ACCERTAMENTO NON FIRMATO
        public bool NotPaid { get; set; }

        // ATTIVA ASSICURAZIONE PRIVATA
        public bool ActivatedSelfAssurance { get; set; } = false;
        
        // DATA PAGAMENTO  RIMBORSO SINISTRO DALL'ASSICURAZIONE
        public DateTime? PaidAmoutDate { get; set; }

        // DATA PAGAMENTO  RISARCIMENTO AL CONDOMINO
        public DateTime? PaidAmoutToCondomiumDate { get; set; }
    }

    public enum Type
    {
        // Acuqa condotta
        [BsonRepresentation(BsonType.String)]
        Water,
        // Editore
        [BsonRepresentation(BsonType.String)]
        Electricity,
        // Ricerca Guasto
        [BsonRepresentation(BsonType.String)]
        SeekingDamage,
        // Eventi Atmosferi
        [BsonRepresentation(BsonType.String)]
        Atmosphere
    }
}
