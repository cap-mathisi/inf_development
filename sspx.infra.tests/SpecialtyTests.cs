using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class SpecialtyTests : IClassFixture<SSPDataFixture>
    {
        private ISpecialtyRepository _specialtyRepository;
        private SSPDataFixture _fixture;

        public SpecialtyTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
            _specialtyRepository = new SpecialtyRepository(
                _fixture.SSPxTestConfig
            );
        }

        [Fact]
        public void List()
        {
            var specialties = _specialtyRepository.List();
            Assert.True(specialties.Count > 0);
        }
    }
}
