using sspx.web.Helpers;
using Xunit;

namespace sspx.infra.tests
{
    public class EmailHelperPlainTextTests
    {
        [Fact]
        public void GivenSimpleHtmlStringShouldReturnPlainText()
        {
            var expected = "Here is the first sentence. Here is the second one.";

            var actual = EmailHelper.SimpleHtmlToPlainText("<div>Here is the first sentence. </div><span style='padding-left: 100px'>Here is the second one.</span>");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenHtmlStringWithAmpersandCharacterCodesShouldRemoveTagsAndCodes()
        {
            var expected = "This should strip out tags and ampersand character codes because we don't need those for plain text emails.";

            var actual = EmailHelper.SimpleHtmlToPlainText("<div>This should strip out tags and ampersand character codes because we don't need those for &quot;plain text&quot; emails.&copy;</div>");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenHtmlStringWithLeadingSpacesAndSpaceBetweenTagsShouldRemoveThem()
        {
            var expected = "line one\r\n\r\nline two";

            var actual = EmailHelper.SimpleHtmlToPlainText(@"
                                    <p>
                                        line one
                                    </p>
                                    <p>
                                        line two
                                    </p>
            ");

            Assert.Equal(expected, actual);
        }
    }
}
