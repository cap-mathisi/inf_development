using SendGrid.Helpers.Mail;
using sspx.web.Helpers;
using Xunit;

namespace sspx.infra.tests
{
    public class SendGridHelperTests
    {
        [Fact]
        public void GivenEmailAddressWithNameShouldCreateEmailAddressObjectWithName()
        {
            var expected = "Yma Sumac";

            var email = SendGridHelper.StringToEmailAddress("Yma Sumac <test@example.com>");
            var actual = email.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenEmailAddressWithNameShouldCreateEmailAddressObjectWithEmail()
        {
            var expected = "test@example.com";

            var email = SendGridHelper.StringToEmailAddress("Yma Sumac <test@example.com>");
            var actual = email.Email;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenEmailAddressWithoutNameShouldCreateEmailAddressWithEmail()
        {
            var expected = "test@example.com";

            var email = SendGridHelper.StringToEmailAddress("test@example.com");
            var actual = email.Email;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenEmailAddressWithoutNameShouldCreateEmailAddressWithNullName()
        {
            var email = SendGridHelper.StringToEmailAddress("test@example.com");

            Assert.Null(email.Name);
        }


    }
}