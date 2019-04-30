using sspx.web.Helpers;
using System.Collections.Generic;
using Xunit;

namespace sspx.infra.tests
{
    public class EmailHelperCreateFromTemplateTests
    {
        [Fact]
        public void GivenHtmlEmailTemplateAndDictionaryShouldReturnCompletedEmail()
        {
            var expected = @"Dear John, Please verify your email address by clicking this link: <a href='https://www.google.com'>https://www.google.com</a>";

            var variables = new Dictionary<string, string>()
            {
                { "[FIRST_NAME]", "John" },
                { "[EMAIL_CONFIRMATION_LINK]", "https://www.google.com" },
            };
            var actual = EmailHelper.CreateEmailFromTemplate(
                @"Dear [FIRST_NAME], Please verify your email address by clicking this link: <a href='[EMAIL_CONFIRMATION_LINK]'>[EMAIL_CONFIRMATION_LINK]</a>",
                variables
            );

            Assert.Equal(expected, actual);
        }

    }
}
