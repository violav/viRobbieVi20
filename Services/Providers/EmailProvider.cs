
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Core.Contracts;
using Core.Models;

namespace Services.Providers
{
    /**
            if ($ambiente != "produzione") {
	            $ROOTFRONT = "https://www.studio2e.it/";
	            $HOST = "89.46.111.234";
	            $USER = "Sql1474852";
	            $PASSWORD = "62kp220er2";
	            $DBNAME = "Sql1474852_1";

            } else
            {
	
	            //***********************************
	            // ATTENZIONE: VALORI DI PRODUZIONE!!
	            //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
	
	            $ROOTFRONT = "https://www.studio2e.it/";
	            $HOST = "89.46.111.234";
	            $USER = "Sql1474852";
	            $PASSWORD = "62kp220er2";
	            $DBNAME = "Sql1474852_1";
	            $folder = "fototickets";

                define("ROOTFRONT", $ROOTFRONT);
            define("HOST", $HOST);
            define("USER", $USER);
            define("PASSWORD", $PASSWORD);
            define("DBNAME", $DBNAME);
            define("PORT", $PORT);
            define('EMAIL_SYSTEM', "staff@studio2e.it");
            ?>
    **/


    public class EmailProvider : IEmailClient
    {
        private readonly Smtp _smtp; 
        private readonly EmailAuthentication _authentication; 
        private readonly Sender _sender; 

        public EmailProvider(Smtp smtp, EmailAuthentication authentication, Sender sender)
        {
            this._smtp = smtp;
            this._authentication = authentication;
            this._sender = sender;
        }

        public void SendEmail(Recipient recipient, Message msg)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(this._sender.Name, this._sender.Address));
            message.To.Add(new MailboxAddress(recipient.Name, recipient.Email));
            message.Subject = msg.Subject;
            message.Body = new TextPart("plain")
            {
                Text = msg.Body
            };
            using (var client = new SmtpClient())
            {
                client.Connect(this._smtp.Host, this._smtp.Port, this._smtp.UseSsl);
                client.Authenticate(this._authentication.Address, this._authentication.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}