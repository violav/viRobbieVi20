namespace Core.Contracts
{
    public interface ISmtp
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }
}
