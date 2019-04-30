using System.Linq;
using Xunit;
using sspx.core.events;
using sspx.tests.builders;

namespace sspx.tests.core.entities
{
    public class ProtcolCreatedShould
    {
        [Fact]
        public void SetProtocolCreatedTrue()
        {
            var protocol = new ProtocolBuilder().Build();

            protocol.Create();

            Assert.True(protocol.IsCreated);
        }

        [Fact]
        public void RaiseProtocolCreatedEvent()
        {
            var protocol = new ProtocolBuilder().Build();

            protocol.Create();

            Assert.Single(protocol.Events);
            Assert.IsType<ProtocolCreatedEvent>(protocol.Events.First());
        }
    }
}
