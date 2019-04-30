using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class QualificationTests : IClassFixture<SSPDataFixture>
    {
        private IQualificationRepository _qualificationRepository;
        private SSPDataFixture _fixture;

        public QualificationTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
            _qualificationRepository = new QualificationRepository(
                _fixture.SSPxTestConfig
            );
        }

        [Fact]
        public void GetQualification()
        {
            var expected = "Engineer";

            var qualification = _qualificationRepository.GetByKey(8);
            var actual = qualification.Description;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetQualifications()
        {
            var actual = _qualificationRepository.List();
            Assert.True(actual.Count > 0);
        }

        [Fact]
        public void GetQualificationsActive()
        {
            var expected = 0;

            var activeQualifications = _qualificationRepository.ListActive();
            var actual = activeQualifications.FindAll(q => q.Active == false).Count;

            Assert.Equal(expected, actual);
        }

        // TODO CS2
        // [Fact]
        //public void AddQualification()
        //{

        //}

        // TODO CS2
        // [Fact]
        //public void SaveQualification()
        //{

        //}
    }
}
