using Microsoft.Extensions.Configuration;

namespace sspx.web.Services
{
    public class CapEmails : ICapEmails
    {
        private IConfiguration _config;

        public CapEmails(IConfiguration config)
        {
            _config = config;
        }

        public string GetCAP_StaffEmails()
        {
            return _config["SSPX_EMAIL_CAP_STAFF"];
        }
    }
}
