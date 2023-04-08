using System.Diagnostics.Contracts;
using Repositories.MongoConfiguration;

namespace Robbie.src.Models.Domain
{
    // Gestione potature alberi ad alto fusto
    public class GreenPrunning : SupplierTicketDocument
    {
        // CAMBIO ESSENZE STAGIONALI DA CONTRATTO
        public bool SeasonalEssence { get; set; }
        // POTATURA STRAORDINARIA ALBERI AD ALTO FUSTO DA CONTRATTO
        public bool ExtraPrunningByContract { get; set; }
        // ABBATTIMENTO ALBERI AD ALTO FUSTO COMPRESO RIMOZIONE DEL CEPPO
        public bool KnockOverHighTrunk { get; set; }
        // PIANTUMAZIONE ALBERI AD ALTO FUSTO
        public bool TrePlanting { get; set; }
        // MANUTENZIONE IMPIANTO D'IRRIGAZIONE
        public bool ManteinanceIrrigationSystem { get; set; }
    }
}
