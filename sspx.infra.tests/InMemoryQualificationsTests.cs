using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryQualificationsTests
    {
        private IQualificationRepository _qualificationRepository;

        public InMemoryQualificationsTests()
        {
            _qualificationRepository = new InMemoryQualificationRepository();
        }

        // TODO CS2
        // [Fact]
        //public void Add()
        //{

        //}

        // TODO CS2
        // [Fact]
        //public void Delete()
        //{

        //}

        [Fact]
        public void GetByKey()
        {
            var expected = "FCAP";

            var version = _qualificationRepository.GetByKey(13);
            var actual = version.Description;

            Assert.Equal(expected, actual);
        }

        // TODO CS2
        // [Fact]
        //public void GetLatestVersionForProtocol()
        //{

        //}

        [Fact]
        public void List()
        {
            var expected = 16;

            var qualifications = _qualificationRepository.List();
            var actual = qualifications.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ListActive()
        {
            var expected = 15;

            var activeQualifications = _qualificationRepository.ListActive();
            var actual = activeQualifications.Count;

            Assert.Equal(expected, actual);
        }

        // TODO CS2
        // [Fact]
        //public void Update()
        //{

        //}
    }
}
