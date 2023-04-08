using Repositories.MongoConfiguration;
using Repositories.NestedDocuments;

namespace Repositories
{
    // Gestione verifiche periodiche di messa a terra
    public class Ground : CheckTicketDocument
    {
        // Periodicità verifica periodica di messa a terra
        public int Last { get; set; }
        // Data stipula contratto
        public DateTime DateSignedContract { get; set; }
        // Data della verifica
        public DateTime DateInitialCheck { get; set; }
        // Data della verifica del verbale
        public DateTime DateCheckContract { get; set; }
        // Data scadenza per prossima verifica di messa a terra
        public DateTime DateDeadline { get; set; }
        // Presenza prescrizione
        public string Provision { get; set; } = null;
        // Esecuzione dei lavori indicazioni nella prescrizione
        public bool ExecutedProvisioned { get; set; } = false;
        
    }

}
