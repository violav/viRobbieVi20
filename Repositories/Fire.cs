using Repositories.MongoConfiguration;

namespace Repositories
{
    // Gestione rinnovo certificati prevenzione anti-incendio
    public class Fire : SupplierTicketDocument
    {
        // Rilasio CPI
        public bool ReleaseCPI { get; set; }
        // SCADENZA CPI
        public DateTime DeadlineCPI { get; set; }
        // ANNI RINNOVO
        public int Last { get; set; }
        // ATTIVITA' 1 SOGGETTA
        public string ActivityOne { get; set; }
        // ATTIVITA' 2 SOGGETTA
        public string ActivityTwo { get; set; }
        // N° PRATICA
        public string NumPratica { get; set; }
        // PIN
        public string Pin { get; set; }
        // Note
        public string Note { get; set; }
    }
}
