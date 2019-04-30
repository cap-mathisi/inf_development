using sspx.core.interfaces;
using sspx.core.sharedkernel;

namespace sspx.tests
{
    class NoOpDomainEventDispatcher
    {
        public void Dispatch(BaseDomainEvent domainEvent) { }
    }
}
