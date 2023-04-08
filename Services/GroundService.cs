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
    public class GroundService : BusinessModuleDocument<Ground>
    {
        public GroundService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<Ground>(databaseSettings.Value.GroundCollection);
        }

        public override async Task CreateAsync(Ground newItem)=> await this.collection.InsertOneAsync(newItem);
        public override async Task<List<Ground>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<Ground> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, Ground updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);


        public async Task<List<Ground>> GetByCondominiumId(string tenantId, string condominiumId) =>
              await this.collection.Find(x => x.TenantId == tenantId && x.Condominium.Id == condominiumId)
                .ToListAsync();

        public async Task<IEnumerable<Ground>> SearchTickets(BusinessDocumentRequest businessDocumentRequest)  =>
            await this.collection.Find(
                SearchBusinessModuleRequest<Ground>.CommonSearch(businessDocumentRequest)
            ).ToListAsync();

        public async Task<IEnumerable<string>> GetCondominiumIdBySearch(BusinessDocumentRequest businessDocumentRequest) =>
            await this.collection.Find(
                SearchBusinessModuleRequest<Ground>.CommonSearch(businessDocumentRequest)
            ).Project(x => x.Condominium.Id).ToListAsync();

        public async Task<IEnumerable<Ground>> SearchWaitTickets(BusinessDocumentRequest businessDocumentRequest) =>
           await this.collection.Find(x => x.Messages.Any(y => y.User.Id == businessDocumentRequest.UserId)).ToListAsync();

        public async Task<IEnumerable<Ground>> SearchToBeAnswerTickets(BusinessDocumentRequest businessDocumentRequest) =>
           await this.collection.Find(x => x.Messages.Any(y => y.User.Id != businessDocumentRequest.UserId)).ToListAsync();

    }

}