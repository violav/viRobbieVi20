using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repositories;
using Robbie.Repositories.MongoConfiguration;
using Services.Classes;
using Services.Models.Contracts;

namespace Services { 
    public class UserService : BusinessModuleDocument<User>
    {
        public UserService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.collection = mongoDatabase.GetCollection<User>(databaseSettings.Value.UserCollection);
        }

        public override async Task CreateAsync(User newItem)=> await this.collection.InsertOneAsync(newItem);
        public override async Task<List<User>> GetAllAsync(string tenantId) => await this.collection.Find(x => x.TenantId == tenantId).ToListAsync();
        public override async Task<User> GetAsync(string id, string tenantId) => await this.collection.Find(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
        public override async Task<DeleteResult> RemoveAsync(string id) => await this.collection.DeleteOneAsync(x => x.Id == id);
        public override async Task<ReplaceOneResult> UpdateAsync(string id, User updatedItem) => await this.collection.ReplaceOneAsync(x => x.Id == id, updatedItem);


        public async Task<User> GetUserByEmail(string email) => await this.collection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task<User> CreateAfterLoginAsync(string email)
        {
            var now = DateTime.Now;
            var user = new User()
            {
                Email = email,
                CreatedAt = now,
                TenantId = Guid.NewGuid().ToString(),
                LastUpdate = now,
                Name = EmailTools.GetAddressByEmail(email),
                Role = Role.Supplier,
                SupplierIds = new()
            };

            await this.collection.InsertOneAsync(user);

            return user;
        }

        public async Task<List<User>> GetUsersByEmail (string[] emails)
        {
            List<User> users = new();

            foreach (string email in emails)
            {
                var user = await GetUserByEmail(email);
                if (user is not null) users.Add(user);
            }

            return users;
        }

        public async Task<List<string>> GetSupplierIds(string userId)
        {
            var user = await this.collection.Find(x => x.Id == userId).FirstOrDefaultAsync();
            return user.SupplierIds;
        }
    }

}