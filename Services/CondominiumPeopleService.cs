using Core.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repositories.NestedDocuments;
using Robbie.Repositories.MongoConfiguration;
using Services.Classes;
using Services.Models.Contracts;

namespace Services
{
    public class CondominiumPeopleFileQuery : IMongoQuery<CondominiumPeopleFile> { }
    public class CondominiumPeopleQuery : IMongoQuery<CondominiumPeople> { }

    public class CondominiumPeopleService : BusinessModuleDocument<CondominiumPeople>
    {
        public CondominiumPeopleService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<CondominiumPeople>(databaseSettings.Value.CondominiumPeopleCollection);
        }

        public override async Task CreateAsync(CondominiumPeople newItem) => await this.collection.InsertOneAsync(newItem);
        public override async Task<List<CondominiumPeople>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<CondominiumPeople> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, CondominiumPeople updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);

        public async Task<CondominiumPeople> GetPersonByEmail(string email) => await this.collection.Find(x => x.Email == email).FirstOrDefaultAsync();
        public async Task CreateManyAsync(List<CondominiumPeople> newItems) => await this.collection.InsertManyAsync(newItems);

        public async Task<List<CondominiumPeople>> GetPersonByEmailOrAlias(string param, string tenantId) =>
            await this.collection.Find(x =>
                    x.Alias.Contains(param) ||
                    x.Email.Contains(param) &&
                    tenantId == x.TenantId
                ).ToListAsync();

        public async Task AddFromImports(List<CondominiumPeopleFile> i_peopleFiles, string i_tenantId)
        {

            foreach (CondominiumPeopleFile personFile in i_peopleFiles)
            {
                if(personFile.Email != "")
                {
                    CondominiumPeople condomiumPerson = await GetPersonByEmail(personFile.Email);

                    if (CheckDocumentIsNotNull(condomiumPerson))
                    {
                        CondominiumPeople person = CondominiumPerson.AddNewPerson(new CondominiumPeopleFileQuery()
                        {
                            Document = personFile,
                            TenantId = i_tenantId
                        });
                        await this.CreateAsync(person);
                    }
                    else
                    {
                        CondominiumPeople person = CondominiumPerson.UpdatePerson(new CondominiumPeopleQuery()
                        {
                            Document = condomiumPerson,
                            TenantId = i_tenantId
                        });
                        await UpdateAsync(condomiumPerson.Id, person);
                    }
                }
            };
        }

    }
}