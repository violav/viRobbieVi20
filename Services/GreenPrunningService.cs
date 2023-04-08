using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repositories;
using Robbie.Repositories.MongoConfiguration;
using Robbie.src.Models.Domain;
using Services.Classes;
using Services.Models.Contracts;
using Services.Models.DTOs;
using Services.Providers;

namespace Services
{
    public class GreenPrunningService : BusinessModuleDocument<GreenPrunning>
    {
        public GreenPrunningService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<GreenPrunning>(databaseSettings.Value.GreenPrunningCollection);
        }

        public override async Task CreateAsync(GreenPrunning newItem)=> await this.collection.InsertOneAsync(newItem);
        public override async Task<List<GreenPrunning>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<GreenPrunning> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, GreenPrunning updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);


    }

}