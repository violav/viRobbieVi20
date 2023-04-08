using Core.Contracts;

namespace Core.Models
{
    public class EmailAuthentication : IEmailAuthentication
    {
        public string Address { get; set; }
        public string Password { get; set; }

        public EmailAuthentication(string address, string password)
        {
            Address = address;
            Password = password;
        }
    }
}
