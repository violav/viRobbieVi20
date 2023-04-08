using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repositories;
using Robbie.Repositories.MongoConfiguration;
using Services.Classes;
using Services.Models.Contracts;
using Services.Models.DTOs;
using Services.Providers;

namespace Services
{
    public class AssembleeService : BusinessModuleDocument<Assemblee>
    {
        public AssembleeService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<Assemblee>(databaseSettings.Value.AssembleeCollection);
        }

        public override async Task CreateAsync(Assemblee newItem)=> await this.collection.InsertOneAsync(newItem);
        public override async Task<List<Assemblee>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<Assemblee> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, Assemblee updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);
    }

}