using Robbie.Repositories.MongoConfiguration;

namespace Repositories.NestedDocuments
{
    public class CondominiumPeople : MongoDocument
    {
        public string Alias { get; set; } 
        public string Email { get; set; }
        public string? Title { get; set; }
        public string? Telephone { get; set; }
        public string? Note { get; set; }
    }
}
