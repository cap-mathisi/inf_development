using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace sspx.web.Services
{
    public interface ISSPxEmailSender
    {
        Task SendEmailOneOrMoreRecipientsAsync(string emailsCommaDelimited, string subject, string htmlMessage);
    }
}
