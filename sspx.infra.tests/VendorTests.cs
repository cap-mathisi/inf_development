using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class VendorTests : IClassFixture<SSPDataFixture>
    {
        private IVendorRepository _vendorRepository;
        private SSPDataFixture _fixture;

        public VendorTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
            _vendorRepository = new VendorRepository(
                _fixture.SSPxTestConfig
            );
        }

        [Fact]
        public void List()
        {
            var vendors = _vendorRepository.List();
            Assert.True(vendors.Count > 0);
        }
    }
}
