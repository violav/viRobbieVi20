using Robbie.Repositories.MongoConfiguration;

namespace Repositories
{
    public class Tenant : MongoDocument
    {
        public string Name { get; set; } = null!;
    }
 
}