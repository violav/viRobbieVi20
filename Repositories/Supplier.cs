using Robbie.Repositories.MongoConfiguration;
using Repositories.NestedDocuments;

namespace Repositories
{
    public class Supplier : MongoDocument
    {
        public string Name { get; set; } = null!;
        public Address? Address { get; set; }
        // Codice fiscale
        public string? FiscalCode { get; set; }
        // Partita IVA
        public string? VatCode { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
        public string Email { get; set; }
        public string? Pec { get; set; }
        public string? Note { get; set; }
        public Person? Owner { get; set; }
        public Bank? Bank { get; set; }

        //[BsonRepresentation(BsonType.String)]
        //public List<BusinessAreaType> BusinessArea { get; set; } = new List<BusinessAreaType>();
    }


    // TODO: va pensato meglio, anche in ottima apertura ad esterni
    // 1 a N
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    //public enum BusinessAreaType
    //{
    //    WaterManagement,    // Manutenzione impianto antenna Tv
    //    ElectricManagement, // Manutenzione impianto elettrico - citofonico
    //    Architect,          // Architetto
    //    Fire,               // 
    //    Door,               // Manutenzione porte scorrevoli
    //    Insurance,          // Agenzia assicurazione
    //    Lift,               // Verifica periodica impianto ascensore
    //    Batherm,            // Bagnante
    //    Gas,                // Fornitura gas
    //    Cleaning,           // Servizio di pulizia
    //    Smith,              // Fabbro
    //    RatManagement,      // Servizio derattizzazione - dezanzarizzazione
    //    Bank,               // Banca
    //    GardenManagement,   // Manutenzione giardino
    //    Hardare,            // Ferramenta
    //    Surveyor,           // Geometra
    //    Bar,                // Bar
    //    City,               // Comune
    //    Garbage,            // Servizio ritiro rifiuti
    //}
}
