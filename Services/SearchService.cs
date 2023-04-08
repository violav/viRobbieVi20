using Services;
using Services.Classes;
using Services.Models.Contracts;
using Services.Models.DTOs;

namespace Robbie.Services
{
    public class SearchService
    {
        private readonly CondominiumService condominiumService;
        private readonly FireService fireService;
        private readonly GroundService groundService;
        private readonly LiftService liftService;
        private readonly UserService userService;

        public SearchService(CondominiumService codmsS, FireService fireS, GroundService groundS, LiftService liftS, UserService userS)
        {
            this.condominiumService = codmsS;
            this.fireService = fireS;
            this.groundService = groundS;
            this.liftService = liftS;
            this.userService = userS;
        }

        public async Task<List<Tickets>> TicketsBusinessModule(string tenantId, BusinessDocumentRequest businessDocumentRequest)
        {
            List<Tickets> output = new();

            var grounds = await groundService.SearchTickets(businessDocumentRequest);
            if (grounds.Count() > 0) output.AddRange(TicketTools.FromGroundToTicket(grounds));

            return output;
        }

        public async Task<List<Tickets>> TicketsCondominiums(string tenantId, BusinessDocumentRequest businessDocumentRequest)
        {
            List<Tickets> tickets = new();

            var condominiums = await condominiumService.GetAllAsync(tenantId);

            if(condominiums.Count() > 0)
            {
                List<string> ids = new(); 
                var condomiumsIds = condominiums.Select(x => x.Id);

                var condominiusIdsByGround = await groundService.GetCondominiumIdBySearch(businessDocumentRequest);

                ids = condomiumsIds.Intersect(condominiusIdsByGround).ToList();

                var condominiusSelected = condominiums.Where(x => ids.Any(id => id == x.Id)).ToList();

                tickets.AddRange(TicketTools.FromCondominiumToTicket(condominiusSelected));
            }

            return tickets;
        }
    }

}