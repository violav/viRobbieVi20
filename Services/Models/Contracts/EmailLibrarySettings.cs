namespace Services.Models.Contracts
{
    public class EmailLibrarySettings
    {
        public const string EmailLibrary = "EmailLibrary";

        public string SmtpClientHost { get; set; } = null!;
        public int SmtpClientPort { get; set; }
        public bool SmtpClientUseSsl { get; set; }
        public string SenderName { get; set; } = null!;
        public string SenderAddress { get; set; } = null!;
        public string AuthenticationAddress { get; set; } = null!;
        public string AuthenticationPassword { get; set; } = null!;

    }

}
