using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repositories;
using Robbie.Repositories.MongoConfiguration;
using Services.Models.Contracts;

namespace Services
{
    public class LiftService : BusinessModuleDocument<Lift>
    {
        public LiftService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<Lift>(databaseSettings.Value.LiftCollection);
        }

        public override async Task CreateAsync(Lift newItem)=> await this.collection.InsertOneAsync(newItem);
        public override async Task<List<Lift>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<Lift> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, Lift updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);

    }

}