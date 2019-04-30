using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using Xunit;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.data;
using sspx.tests.builders;

namespace sspx.tests.integration.data
{
    public class ProtocolEfRepositoryShould
    {
        private AppDbContext _dbContext;

        private static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("sspx")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Fact]
        public void AddProtocolAndSetProtocolId()
        {
            var repository = GetRepository();
            var rand = new Random();
            var initialProtocolId = new decimal(rand.NextDouble());
            var protocol = new ProtocolBuilder().ProtocolId(initialProtocolId).Build();

            repository.Add(protocol);

            var newProtocol = repository.List().FirstOrDefault();

            System.Console.WriteLine("old: {0}", protocol);
            System.Console.WriteLine("new: {0}", newProtocol);

            Assert.Equal(protocol, newProtocol);
            Assert.True(newProtocol?.ProtocolId > 0);
        }

        [Fact]
        public void UpdateProtocolAfterAddingIt()
        {
            // add a protocol
            var repository = GetRepository();

            var rand = new Random();
            var initialProtocolId = new decimal(rand.NextDouble());
            var item = new ProtocolBuilder().ProtocolId(initialProtocolId).Build();

            repository.Add(item);

            // detach protocol
            _dbContext.Entry(item).State = EntityState.Detached;

            // fetch the protocol and update its protocolId
            var newProtocol = repository.List()
                .FirstOrDefault(p => p.ProtocolId == initialProtocolId);
            Assert.NotNull(newProtocol);
            Assert.NotSame(item, newProtocol);
            var newTitle = Guid.NewGuid().ToString();
            var rand2 = new Random();
            var newProtocolId = new decimal(rand2.NextDouble());
            newProtocol.ProtocolId = newProtocolId;

            // Update the item
            repository.Update(newProtocol);
            var updatedProtocol = repository.List()
                .FirstOrDefault(p => p.ProtocolId == newProtocolId);

            Assert.NotNull(updatedProtocol);
            Assert.NotEqual(item.ProtocolId, updatedProtocol.ProtocolId);
            Assert.Equal(newProtocol.ProtocolId, updatedProtocol.ProtocolId);
        }

        [Fact]
        public void DeleteItemAfterAddingIt()
        {
            // add a protocol
            var repository = GetRepository();
            var rand = new Random();
            var initialProtocolId = new decimal(rand.NextDouble());
            var protocol = new ProtocolBuilder().ProtocolId(initialProtocolId).Build();

            repository.Add(protocol);

            // delete the item
            repository.Delete(protocol);

            // verify it's no longer there
            Assert.DoesNotContain(repository.List(),
                p => p.ProtocolId == initialProtocolId);
        }

        private EfRepository<Protocol> GetRepository()
        {
            var options = CreateNewContextOptions();
            var mockDispatcher = new Mock<IDomainEventDispatcher>();

            _dbContext = new AppDbContext(options, mockDispatcher.Object);
            return new EfRepository<Protocol>(_dbContext);
        }
    }
}