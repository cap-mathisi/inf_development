using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemorySpecialtyRepositoryTests
    {
        private ISpecialtyRepository _specialtyRepository;

        public InMemorySpecialtyRepositoryTests()
        {
            _specialtyRepository = new InMemorySpecialtyRepository();
        }

        [Fact]
        public void List()
        {
            var expected = 14;

            var actual = _specialtyRepository.List().Count;

            Assert.Equal(expected, actual);
        }
    }
}
