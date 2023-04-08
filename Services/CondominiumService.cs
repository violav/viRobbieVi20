using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repositories;
using Repositories.NestedDocuments;
using Robbie.Repositories.MongoConfiguration;
using Services.Models.Contracts;

namespace Services
{
    public class CondominiumQuery : IMongoQuery<Condominium> { }

    public class CondominiumAdvisorQuery : IMongoQuery<Person> { }

    public class CondominiumService : BusinessModuleDocument<Condominium>
    {
        public CondominiumService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<Condominium>(databaseSettings.Value.CondominiumCollection);
        }

        public override async Task CreateAsync(Condominium newItem)=> await this.collection.InsertOneAsync(newItem);
        public override async Task<List<Condominium>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<Condominium> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, Condominium updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);

        public async Task<List<Condominium>> GetByName(string name) => await this.collection.Find(x => x.Name == name).ToListAsync();

    }

}