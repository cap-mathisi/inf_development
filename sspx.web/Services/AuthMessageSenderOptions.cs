namespace sspx.web.Services
{
    public class AuthMessageSenderOptions
    {
        public string SSPX_EMAIL_SENDGRID_API_KEY { get; set; }
        public string SSPX_EMAIL_FROM_EMAIL { get; set; }
        public string SSPX_EMAIL_FROM_NAME { get; set; }
        public string SSPX_EMAIL_OVERRIDE_ONLY_SEND_TO { get; set; }
    }
}
