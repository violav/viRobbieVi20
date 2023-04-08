using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/lifts")]
    public class LiftController : ControllerBase
    {
        private readonly LiftService liftService;

        public LiftController(LiftService i_liftService)
        {
            this.liftService = i_liftService;
        }

        [HttpGet]
        public async Task<List<Lift>> GetAll([FromHeader] string TenantId) => await liftService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(Lift newItem)
        {
            await liftService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Lift>> Get(string id, [FromHeader] string TenantId)
        {
            var item = await liftService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Lift updatedItem, [FromHeader] string TenantId)
        {
            var req = await liftService.UpdateAsync(id, updatedItem);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var req = await liftService.RemoveAsync(id);
            return req.DeletedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/closed")]
        public async Task<IActionResult> CloseTicket(string id, [FromHeader] string TenantId)
        {
            var item = await liftService.GetAsync(id, TenantId);
            item.IsOpened = false;
            var req = await liftService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/opened")]
        public async Task<IActionResult> OpenTicket(string id, [FromHeader] string TenantId)
        {
            var item = await liftService.GetAsync(id, TenantId);
            item.IsOpened = true;
            var req = await liftService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }
    }
}