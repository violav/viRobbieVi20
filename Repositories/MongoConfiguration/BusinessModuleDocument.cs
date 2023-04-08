using System;
using Repositories.NestedDocuments;
using Robbie.Repositories.MongoConfiguration;

namespace Repositories.MongoConfiguration
{
    public class BaseTicketDocument : MongoDocument
    {
        // Stato del ticket
        public bool IsOpened { get; set; } = true;
        // Condominio di appartenza del ticket
        public Condominium Condominium { get; set; }

        public List<Message> Messages { get; set; } = new();
        public string? Note { get; set; }
    }

    public class SupplierTicketDocument : BaseTicketDocument
    {
        // Fornitore incaricato della manutenzione
        public Firm SupplierMaintenance { get; set; }
    }

    public class CheckTicketDocument : SupplierTicketDocument
    {
        // Fornitore incaricato della verifica
        public Firm? SupplierCheck { get; set; }
    }



}