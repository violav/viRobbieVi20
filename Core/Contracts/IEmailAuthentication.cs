namespace Core.Contracts
{
    public interface IEmailAuthentication
    {
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
