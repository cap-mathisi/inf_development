using sspx.infra.data;
using Xunit;

namespace sspx.infra.tests
{
    public class StandardTests : IClassFixture<SSPDataFixture>
    {
        private SSPDataFixture _fixture;

        public StandardTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
        }

        // TODO CS2
        // [Fact]
        //public void GetStandard()
        //{

        //}

        [Fact]
        public void GetStandards()
        {
            var actual = SSPxDBHelper.GetStandards(_fixture.SSPxTestConfig.SSPxConnectionString);

            Assert.NotNull(actual);
            Assert.True(actual.Count > 0);
        }

        // TODO CS2
        // [Fact]
        //public void AddStandard()
        //{

        //}

        // TODO CS2
        // [Fact]
        //public void DeleteStandard()
        //{

        //}

        // TODO CS2
        // [Fact]
        //public void SaveStandard()
        //{

        //}
    }
}
