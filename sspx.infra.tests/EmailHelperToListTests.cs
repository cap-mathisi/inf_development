using sspx.web.Helpers;
using Xunit;

namespace sspx.infra.tests
{
    public class EmailHelperToListTests
    {
        [Fact]
        public void GivenCommaDelimitedStringOfEmailsShouldReturnList()
        {
            var expected = 3;
            var actual = EmailHelper.EmailsToList("fake@example.com,fake2@example.com,fake3@example.com").Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenCommaDelimitedStringOfEmailsWithExtraSpacesShouldTrimSpaces()
        {
            var expected = "fake2@example.com";
            var actual = EmailHelper.EmailsToList("fake@example.com,  fake2@example.com    ,   fake3@example.com,,,,")[1];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenCommaDelimitedStringOfEmailsWithBlankEntriesShouldRemoveBlankEntries()
        {
            var expected = 3;
            var actual = EmailHelper.EmailsToList("fake@example.com,  fake2@example.com    ,   fake3@example.com,,,,").Count;

            Assert.Equal(expected, actual);
        }

    }
}
