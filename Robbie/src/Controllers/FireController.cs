using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/fire")]
    public class FireController : ControllerBase
    {
        private readonly FireService fireService;

        public FireController(FireService i_fireService)
        {
            this.fireService = i_fireService;
        }

        [HttpGet]
        public async Task<List<Fire>> GetAll([FromHeader] string TenantId) => await fireService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(Fire newItem)
        {
            await fireService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Fire>> GetOne(string id, [FromHeader] string TenantId)
        {
            var item = await fireService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Fire updatedItem, [FromHeader] string TenantId)
        {
            var request = await fireService.UpdateAsync(id, updatedItem);
            return request.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var req = await fireService.RemoveAsync(id);
            return req.DeletedCount != 0 ? NoContent() : NotFound();
        }

        [HttpPost("{id:length(24)}/closed")]
        public async Task<IActionResult> CloseTicket(string id, [FromHeader] string TenantId)
        {
            var item = await fireService.GetAsync(id, TenantId);
            item.IsOpened = false;
            var req = await fireService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/opened")]
        public async Task<IActionResult> OpenTicket(string id, [FromHeader] string TenantId)
        {
            var item = await fireService.GetAsync(id, TenantId);
            item.IsOpened = true;
            var req = await fireService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }
    }
}