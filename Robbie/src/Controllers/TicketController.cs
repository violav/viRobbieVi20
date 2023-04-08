

using Microsoft.AspNetCore.Mvc;
using Robbie.Services;
using Services.Models.Contracts;
using Services.Models.DTOs;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly SearchService searchService;

        public TicketController(SearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("search")]
        public async Task<List<Tickets>> Search([FromHeader] string TenantId, [FromQuery] BusinessDocumentRequest businessDocumentRequest) =>
            await searchService.TicketsBusinessModule(TenantId, businessDocumentRequest);

        [HttpGet("connected-condominiums")]
        public async Task<List<Tickets>> GetListCondominius([FromHeader] string TenantId, [FromQuery] BusinessDocumentRequest businessDocumentRequest) =>
            await searchService.TicketsCondominiums(TenantId, businessDocumentRequest);

        [HttpGet("wait-answers")]
        public async Task<List<Tickets>> TicketsNotOpened([FromHeader] string TenantId, [FromQuery] BusinessDocumentRequest businessDocumentRequest) =>
            await searchService.TicketsBusinessModule(TenantId, businessDocumentRequest);

        [HttpGet("wait-tobeanswers")]
        public async Task<List<Tickets>> TicketsNotAnswers([FromHeader] string TenantId, [FromQuery] BusinessDocumentRequest businessDocumentRequest) =>
            await searchService.TicketsBusinessModule(TenantId, businessDocumentRequest);

    }
}