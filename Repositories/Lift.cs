using Repositories.MongoConfiguration;
using Repositories.NestedDocuments;
using Robbie.Repositories.MongoConfiguration;

namespace Repositories
{
    // Gestione verifiche biennali impianto ascensore
    public class Lift : CheckTicketDocument
    {
        // Periodicità verifica periodica impianto ascensore
        public int Last { get; set; }
        // Data stipula contratto
        public DateTime DateSignedContract { get; set; }
        // Data della verifica periodica
        public DateTime DateInitialCheck { get; set; }
        // Data della verifica del verbale
        public DateTime DateCheckContract { get; set; }
        // Data scadenza per prossima verifica impianto ascensore
        public DateTime DateDeadline { get; set; }
        // Presenza prescrizione
        public string Provision { get; set; } = null;
        // Esecuzione dei lavori indicazioni nella prescrizione
        public bool ExecutedProvisioned { get; set; } = false;
    }
}
