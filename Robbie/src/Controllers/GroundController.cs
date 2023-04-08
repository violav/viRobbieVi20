using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/grounds")]
    public class GroundController : ControllerBase
    {
        private readonly GroundService groundService;

        public GroundController(GroundService i_groundService)
        {
            this.groundService = i_groundService;
        }

        [HttpGet]
        public async Task<List<Ground>> GetAll([FromHeader] string TenantId) => await groundService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(Ground newItem)
        {
            await groundService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Ground>> Get(string id, [FromHeader] string TenantId)
        {
            var item = await groundService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Ground updatedItem, [FromHeader] string TenantId)
        {
            var req = await groundService.UpdateAsync(id, updatedItem);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var req = await groundService.RemoveAsync(id);
            return req.DeletedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/closed")]
        public async Task<IActionResult> CloseTicket(string id, [FromHeader] string TenantId)
        {
            var item = await groundService.GetAsync(id, TenantId);
            item.IsOpened = false;
            var req = await groundService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/opened")]
        public async Task<IActionResult> OpenTicket(string id, [FromHeader] string TenantId)
        {
            var item = await groundService.GetAsync(id, TenantId);
            item.IsOpened = true;
            var req = await groundService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

    }
}