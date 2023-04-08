using Core.Contracts;

namespace Core.Models
{
    public class Smtp : ISmtp
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }

        public Smtp(string host, int port, bool useSSl)
        {
            Host = host;
            Port = port;
            UseSsl = useSSl;
        }
    }
}
