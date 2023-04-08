namespace Repositories.NestedDocuments
{
    public class Message 
    {
        // User
        public MessageUser? User { get; set; }
        // Data del messaggio
        public DateTime? Date { get; set; }
        // Corpo del messaggio
        public string? Payload { get; set; }
    }
    
}
