using Core.Models;

namespace Core.Contracts
{
    public interface IEmailClient
    {
        void SendEmail(Recipient recipient, Message msg);
    }
}
