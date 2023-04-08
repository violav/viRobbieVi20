using System.Net;
using Microsoft.AspNetCore.Mvc;
using Repositories.NestedDocuments;
using Robbie.Services;
using Services;
using Services.Models.DTOs;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/condominium-people")]
    public class CondominiumPeopleController : ControllerBase
    {
        private readonly CondominiumPeopleService condominiumPeopleService;
        private readonly ImportsService importsService;

        public CondominiumPeopleController(CondominiumPeopleService i_condoniumPeopleService, ImportsService i_importService)
        {
            this.condominiumPeopleService = i_condoniumPeopleService;
            this.importsService = i_importService;
        }

        [HttpGet]
        public async Task<List<CondominiumPeople>> GetAll([FromHeader] string TenantId) => await condominiumPeopleService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(CondominiumPeople newItem)
        {
            await condominiumPeopleService.CreateAsync(newItem);

            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
        }

        [HttpPost("imports")]
        public async Task<HttpStatusCode> PeopleUpload([FromHeader] string TenantId, [FromBody] string file)
        {
            //List<CondominiumPeopleFile> condominiumPeopleRecords = this.importsService.ReadDataToCondominiumPeople(file);

            //await this.condominiumPeopleService.AddFromImports(condominiumPeopleRecords, TenantId);

            return HttpStatusCode.NoContent;
        }

        [HttpPost("search")]
        public async Task<List<CondominiumPeople>> PeopleSearch(PersonRequest payload, [FromHeader] string TenantId)
            => await this.condominiumPeopleService.GetPersonByEmailOrAlias(payload.SearchText, TenantId);


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CondominiumPeople>> Get(string id, [FromHeader] string TenantId)
        {
            var item = await condominiumPeopleService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, CondominiumPeople updatedItem, [FromHeader] string TenantId)
        {
            await condominiumPeopleService.UpdateAsync(id, updatedItem);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var book = await condominiumPeopleService.GetAsync(id, TenantId);

            if (book is null)
            {
                return NotFound();
            }

            await condominiumPeopleService.RemoveAsync(id);

            return NoContent();
        }
    }
}