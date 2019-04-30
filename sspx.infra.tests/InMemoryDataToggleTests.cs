using sspx.web.Helpers;
using Xunit;

namespace sspx.infra.tests
{
    public class InMemoryDataToggleTests
    {
        [Fact]
        public void GivenNullMainToggleAndTrueSecondaryToggleShouldReturnTrue()
        {
            var expected = true;

            var actual = InMemoryDataToggle.UseInMemory(null, "TRUE");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNullMailToggleAndNullSecondaryToggleShouldReturnFalse()
        {
            var expected = false;

            var actual = InMemoryDataToggle.UseInMemory(null, null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenFalseMainToggleAndTrueSecondaryToggleShouldReturnFalse()
        {
            var expected = false;

            var actual = InMemoryDataToggle.UseInMemory("false", null);

            Assert.Equal(expected, actual);
        }
    }
}
