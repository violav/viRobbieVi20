using MongoDB.Driver;
using Repositories.NestedDocuments;
using Robbie.Repositories.MongoConfiguration;

namespace Repositories
{
    public class Condominium : MongoDocument
    {
        // Nome del condominio
        public string Name { get; set; } = null!;

        // Potenza erogata dal contatore in Kw
        public int PowerCounterUnitKw { get; set; }

        // Condominio soggetto a rinnovo certificato prevenzione incendio
        public bool FireRenewCertificate { get; set; }

        // Numero box
        public int BoxCount { get; set; }

        // Numero appartementi
        public int FlatCount { get; set; }

        // Numero ascensori
        public int LiftCount { get; set; }

        // Anti-incendio a cielo aperto
        public bool SkyOpen { get; set; }

        // Centrale termica
        public bool TermalPlant { get; set; }

        // Riscaldamento autonomo
        public bool IndependentHeatingSystem { get; set; }

        // Dati dell'indirizzo
        public Address Address { get; set; }

        // Franchigie
        public Deductible Deductible { get; set; }

        // Dati bancari
        public Bank Bank { get; set; }

        // Prossimi appuntamenti del condominio
        public BuildingNextTickets BuildingNextTickets { get; set; }
    }

}