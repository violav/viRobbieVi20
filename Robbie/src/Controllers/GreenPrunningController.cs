using Microsoft.AspNetCore.Mvc;
using Repositories;
using Robbie.src.Models.Domain;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/green-prunnings")]
    public class GreenPrunningController : ControllerBase
    {
        private readonly GreenPrunningService greenPrunningService;

        public GreenPrunningController(GreenPrunningService greenPrunningService)
        {
            this.greenPrunningService = greenPrunningService;
        }

        [HttpGet]
        public async Task<List<GreenPrunning>> GetAll([FromHeader] string TenantId) => await greenPrunningService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(GreenPrunning newItem)
        {
            await greenPrunningService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<GreenPrunning>> Get(string id, [FromHeader] string TenantId)
        {
            var item = await greenPrunningService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, GreenPrunning updatedItem, [FromHeader] string TenantId)
        {
            var req = await greenPrunningService.UpdateAsync(id, updatedItem);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var req = await greenPrunningService.RemoveAsync(id);
            return req.DeletedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/closed")]
        public async Task<IActionResult> CloseTicket(string id, [FromHeader] string TenantId)
        {
            var item = await greenPrunningService.GetAsync(id, TenantId);
            item.IsOpened = false;
            var req = await greenPrunningService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/opened")]
        public async Task<IActionResult> OpenTicket(string id, [FromHeader] string TenantId)
        {
            var item = await greenPrunningService.GetAsync(id, TenantId);
            item.IsOpened = true;
            var req = await greenPrunningService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

    }
}