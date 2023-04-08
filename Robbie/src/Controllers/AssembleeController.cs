using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/assemblee")]
    public class AssembleeController : ControllerBase
    {
        private readonly AssembleeService assembleeService;

        public AssembleeController(AssembleeService i_assembleeService)
        {
            this.assembleeService = i_assembleeService;
        }

        [HttpGet]
        public async Task<List<Assemblee>> GetAll([FromHeader] string TenantId) => await assembleeService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(Assemblee newItem)
        {
            await assembleeService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Assemblee>> Get(string id, [FromHeader] string TenantId)
        {
            var item = await assembleeService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Assemblee updatedItem, [FromHeader] string TenantId)
        {
            var req = await assembleeService.UpdateAsync(id, updatedItem);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var req = await assembleeService.RemoveAsync(id);
            return req.DeletedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/closed")]
        public async Task<IActionResult> CloseTicket(string id, [FromHeader] string TenantId)
        {
            var item = await assembleeService.GetAsync(id, TenantId);
            item.IsOpened = false;
            var req = await assembleeService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/opened")]
        public async Task<IActionResult> OpenTicket(string id, [FromHeader] string TenantId)
        {
            var item = await assembleeService.GetAsync(id, TenantId);
            item.IsOpened = true;
            var req = await assembleeService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

    }
}