using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace sspx.web.Helpers
{
    public static class EmailHelper
    {
        // https://www.debuggex.com/r/fvM3eFXqxp3XeRp6
        const string LEADING_WHITESPACE_PATTERN = @"^\s+";

        // https://www.debuggex.com/r/2EarZRWsAQbYyDU8
        const string WHITESPACE_BETWEEN_HTML_TAGS_PATTERN = @">\s*<";

        // https://www.debuggex.com/r/aIOD4qqzMf9s_u6o
        const string HTML_TAG_AND_AMPERSAND_PATTERN = @"<.*?>|&.*?;";

        public static string CreateEmailFromTemplate(string emailTemplate, Dictionary<string, string> templateKeyValues)
        {
            StringBuilder sb = new StringBuilder(emailTemplate);
            foreach(var keyValuePair in templateKeyValues)
            {
                sb.Replace(keyValuePair.Key, keyValuePair.Value);
            }

            return sb.ToString();
        }

        public static List<string> EmailsToList(string commaDelimitedEmails)
        {
            char[] delimiterChars = { ',', ';' };

            return commaDelimitedEmails
                .Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .ToList();
        }

        public static string SimpleHtmlToPlainText(string simpleHtmlString)
        {
            // Multiline option means we remove leading whitespace for EACH line
            string s1 = Regex.Replace(simpleHtmlString, LEADING_WHITESPACE_PATTERN, string.Empty, RegexOptions.Multiline);

            string s2 = Regex.Replace(s1, WHITESPACE_BETWEEN_HTML_TAGS_PATTERN, string.Empty);
            string s3 = Regex.Replace(s2, HTML_TAG_AND_AMPERSAND_PATTERN, string.Empty);

            return s3.Trim();
        }
    }
}
