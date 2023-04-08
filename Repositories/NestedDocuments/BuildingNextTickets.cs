using System;
namespace Repositories.NestedDocuments
{
    // Classe con riferimenti temporali ai prossimi interventi
    public class BuildingNextTickets
	{
        // Messa a terra 
        public DateTime? NextGroundJob { get; set; }
        // Intervent ascensore
        public DateTime? NextLiftJob { get; set; }
        // Rinnovo anti-incendio
        public DateTime? NextFireJob { get; set; }
    }
}

