using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using sspx.web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sspx.web.Services
{
    public class EmailSender : ISSPxEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Environment Variables or Secret Manager

        public Task SendEmailOneOrMoreRecipientsAsync(string emailsCommaDelimited, string subject, string htmlMessage)
        {
            List<string> emails;

            bool overrideEmailWasProvided = string.IsNullOrWhiteSpace(Options.SSPX_EMAIL_OVERRIDE_ONLY_SEND_TO) == false;
            if (overrideEmailWasProvided)
            {
                emails = EmailHelper.EmailsToList(Options.SSPX_EMAIL_OVERRIDE_ONLY_SEND_TO);
            }
            else
            {
                emails = EmailHelper.EmailsToList(emailsCommaDelimited);
            }

            var sendgridEmails = emails
                .Select(e => SendGridHelper.StringToEmailAddress(e))
                .ToList();

            string plainTextMessage = EmailHelper.SimpleHtmlToPlainText(htmlMessage);
            return Execute(subject, htmlMessage, plainTextMessage, sendgridEmails);
        }

        private Task Execute(string subject, string htmlMessage, string plainTextMessage, List<EmailAddress> recipients)
        {
            var client = new SendGridClient(Options.SSPX_EMAIL_SENDGRID_API_KEY);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SSPX_EMAIL_FROM_EMAIL, Options.SSPX_EMAIL_FROM_NAME),
                Subject = subject
            };

            if (string.IsNullOrEmpty(htmlMessage) == false)
            {
                msg.AddContent(MimeType.Html, htmlMessage);
            }

            if (string.IsNullOrEmpty(plainTextMessage) == false)
            {
                msg.AddContent(MimeType.Text, plainTextMessage);
            }

            msg.AddTos(recipients);

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

    }
}