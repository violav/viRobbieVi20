using MongoDB.Driver;
using Robbie.Repositories.MongoConfiguration;
using Microsoft.Extensions.Options;
using Repositories;
using Core.Contracts;
using Repositories.NestedDocuments;
using Services.Models.Contracts;
using Services.Classes;

namespace Robbie.Services
{
    public class SupplierFileQuery : IMongoQuery<SupplierFile> { }

    public class SupplierQuery : IMongoQuery<Supplier> { }

    public class SupplierService : BusinessModuleDocument<Supplier>
    {
       
        public SupplierService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<Supplier>(databaseSettings.Value.SupplierCollection);
        }

        public override async Task CreateAsync(Supplier newItem)=> await this.collection.InsertOneAsync(newItem);
        public override async Task<List<Supplier>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<Supplier> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, Supplier updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);

        public async Task<Supplier> GetSupplierByEmail(string email) => await this.collection.Find(x => x.Email == email).FirstOrDefaultAsync();
        public async Task CreateManyAsync(List<Supplier> newItems) => await this.collection.InsertManyAsync(newItems);

        public async Task UpdateOrAddSupplier(List<SupplierFile> i_supplierFiles, string i_tenantId)
        {

            foreach (SupplierFile supplierFile in i_supplierFiles)
            {
                Supplier supplier = await GetSupplierByEmail(supplierFile.Email);

                if (!CheckDocumentIsNotNull(supplier))
                {
                    await CreateAsync(SupplierTools.GetSupplierFromFiles(new SupplierFileQuery()
                    {
                        Document = supplierFile,
                        TenantId = i_tenantId
                    }));

                } else
                {
                    await UpdateAsync(supplier.Id as string, new Supplier()
                    {
                        Email = supplier.Email,
                        Name = supplier.Name,
                        FiscalCode = supplier.FiscalCode,
                        VatCode = supplier.VatCode,
                        Address = new Address()
                        {
                            City = supplier.Address?.City ?? "",
                            Country = supplier.Address?.Country ?? "",
                            Street = supplier.Address?.Street ?? "",
                            PostalCode = supplier.Address?.PostalCode ?? "",
                            Province = supplier.Address?.Province ?? ""
                        },
                        Bank = supplier.Bank,
                        CreatedAt = supplier.CreatedAt,
                        LastUpdate = new DateTime().Date,
                        Fax = supplier.Fax,
                        Owner = new Person()
                        {
                            Alias = $"{supplier.Owner.Alias}",
                            Email = $"{supplier.Owner.Email}"
                        },
                        Pec = supplier.Pec,
                        Telephone = supplier.Telephone,
                        Note = supplier.Note,
                        TenantId = supplier.TenantId
                    });
                }
            }
        }

        public async Task<List<Supplier>> GetPersonByEmailOrAlias(string text, string tenantId)
        {
           return await this.collection.Find(x =>
                    x.Email.Contains(text) ||
                    x.Name.Contains(text) &&
                    x.TenantId == tenantId
                ).SortByDescending(x => x.Email)
                .ToListAsync();
        }
           
    }

}