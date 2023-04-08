using MongoDB.Driver;
using Repositories;

namespace Robbie.Repositories.MongoConfiguration
{
    public interface IMongoRepository<T>
    {
        public abstract Task<List<T>> GetAllAsync(string tenantId);
        public abstract Task<T> GetAsync(string id, string tenantId);
        public abstract Task CreateAsync(T newItem);
        public abstract Task<ReplaceOneResult> UpdateAsync(string id, T updatedItem);
        public abstract Task<DeleteResult> RemoveAsync(string id);
    }

    public abstract class BusinessModuleDocument<T> : IMongoRepository<T>
    {
        public IMongoCollection<T> collection = null!;

        public abstract Task<List<T>> GetAllAsync(string tenantId);
        public abstract Task<T?> GetAsync(string id, string tenantId);
        public abstract Task CreateAsync(T newItem);
        public abstract Task<ReplaceOneResult> UpdateAsync(string id, T updatedItem);
        public abstract Task<DeleteResult> RemoveAsync(string id);

        public bool CheckDocumentIsNotNull(T doc) => doc != null;

    }
}
