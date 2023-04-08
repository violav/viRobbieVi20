using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/accidents")]
    public class AccidentController : ControllerBase
    {
        private readonly AccidentService accidentService;

        public AccidentController(AccidentService i_accidentService)
        {
            this.accidentService = i_accidentService;
        }

        [HttpGet]
        public async Task<List<Accident>> GetAll([FromHeader] string TenantId) => await accidentService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(Accident newItem)
        {
            await accidentService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Accident>> Get(string id, [FromHeader] string TenantId)
        {
            var item = await accidentService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Accident updatedItem, [FromHeader] string TenantId)
        {
            var req = await accidentService.UpdateAsync(id, updatedItem);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var req = await accidentService.RemoveAsync(id);
            return req.DeletedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/closed")]
        public async Task<IActionResult> CloseTicket(string id, [FromHeader] string TenantId)
        {
            var item = await accidentService.GetAsync(id, TenantId);
            item.IsOpened = false;
            var req = await accidentService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpPost("{id:length(24)}/opened")]
        public async Task<IActionResult> OpenTicket(string id, [FromHeader] string TenantId)
        {
            var item = await accidentService.GetAsync(id, TenantId);
            item.IsOpened = true;
            var req = await accidentService.UpdateAsync(id, item);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

    }
}