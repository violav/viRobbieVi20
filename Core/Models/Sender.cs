using Core.Contracts;

namespace Core.Models
{
    public class Sender : ISender
    {
        public string Address { get; set; }    
        public string Name { get; set; }    

        public Sender(string address, string name)
        {
            Address = address;
            Name = name;
        }
    }
}
