using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.NestedDocuments;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/condominiums")]
    public class CondominiumController : ControllerBase
    {
        private readonly CondominiumService condominiumService;
        private readonly EmailService emailService;
        private readonly GroundService groundService;

        public CondominiumController(
            CondominiumService condominiumService,
            EmailService emailService,
            GroundService groundService
        )
        {
            this.condominiumService = condominiumService;
            this.groundService = groundService;
            this.emailService = emailService;
        }

        [HttpGet]
        public async Task<List<Condominium>> GetAll([FromHeader] string TenantId) => await condominiumService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(Condominium newItem)
        {
            await condominiumService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Condominium>> GetOne([FromHeader] string TenantId, string id)
        {
            var item = await condominiumService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update([FromHeader] string TenantId, string id, Condominium updatedItem)
        {

            var operation = await condominiumService.UpdateAsync(id, updatedItem);
            return operation.MatchedCount !=0 ? NoContent() : NotFound();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete([FromHeader] string TenantId, string id)
        {
            var result = await condominiumService.RemoveAsync(id);
            return result.DeletedCount != 0 ? NoContent() : NotFound();
        }

        /// <summary>
        /// Get list of grounds by condominium id
        /// </summary>
        [HttpGet("{id:length(24)}/grounds")]
        public async Task<List<Ground>> GetGrounds([FromHeader] string TenantId, string id)
            => await groundService.GetByCondominiumId(TenantId, id);

    }
}