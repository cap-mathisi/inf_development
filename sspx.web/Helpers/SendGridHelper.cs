using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;

namespace sspx.web.Helpers
{
    // CS2 based on https://github.com/sendgrid/sendgrid-csharp/blob/master/src/SendGrid/Helpers/Mail/MailHelper.cs
    public class SendGridHelper
    {
        private const string NameGroup = "name";
        private const string EmailGroup = "email";
        private static readonly Regex Rfc2822Regex = new Regex(
            $@"(?:(?<{NameGroup}>)(?<{EmailGroup}>[^\<]*@.*[^\>])|(?<{NameGroup}>[^\<]*)\<(?<{EmailGroup}>.*@.*)\>)",
            RegexOptions.ECMAScript);

        /// <summary>
        /// Uncomplex conversion of a <![CDATA["Name <email@email.com>"]]> to EmailAddress
        /// </summary>
        /// <param name="rfc2822Email">"email@email.com" or <![CDATA["Name <email@email.com>"]]> string</param>
        /// <returns>EmailsAddress Object</returns>
        public static EmailAddress StringToEmailAddress(string rfc2822Email)
        {
            var match = Rfc2822Regex.Match(rfc2822Email);
            
            if (!match.Success)
            {
                return new EmailAddress(rfc2822Email);
            }

            var email = match.Groups[EmailGroup].Value.Trim();
            var name = match.Groups[NameGroup].Value.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                return new EmailAddress(email);
            }

            return new EmailAddress(email, name);
        }
    }
}
