using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryVendorRepositoryTests
    {
        private IVendorRepository _vendorRepository;

        public InMemoryVendorRepositoryTests()
        {
            _vendorRepository = new InMemoryVendorRepository();
        }

        [Fact]
        public void List()
        {
            var expected = 13;

            var actual = _vendorRepository.List().Count;

            Assert.Equal(expected, actual);
        }
    }
}
