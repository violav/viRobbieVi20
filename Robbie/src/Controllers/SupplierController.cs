using System.Net;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Robbie.Services;
using Services.Models.DTOs;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService supplierService;
        private readonly ImportsService importsService;

        public SupplierController(SupplierService i_supplierService, ImportsService i_importsService)
        {
            this.supplierService = i_supplierService;
            this.importsService = i_importsService;
        }

        [HttpGet]
        public async Task<List<Supplier>> GetAll([FromHeader] string TenantId) => await supplierService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(Supplier newItem)
        {
            await supplierService.CreateAsync(newItem);

            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpPost("search")]
        public async Task<List<Supplier>> Search(PersonRequest payload, [FromHeader] string TenantId)
            => await this.supplierService.GetPersonByEmailOrAlias(payload.SearchText, TenantId);

        [HttpPost("imports")]
        public async Task<HttpStatusCode> SuppliersUpload([FromHeader] string TenantId, [FromBody] string file)
        {

            //List<SupplierFile> supplierRecords = this.importsService.ReadDataToSuppliers(i_fileStringed);

            //await this.supplierService.UpdateOrAddSupplier(supplierRecords, TenantId);

            return HttpStatusCode.NoContent;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Supplier>> Get(string id, [FromHeader] string TenantId)
        {
            var item = await supplierService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Supplier updatedItem, [FromHeader] string TenantId)
        {
            var req = await supplierService.UpdateAsync(id, updatedItem);
            return req.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, [FromHeader] string TenantId)
        {
            var req = await supplierService.RemoveAsync(id);
            return req.DeletedCount != 0 ? NoContent() : NotFound();
        }
    }
}