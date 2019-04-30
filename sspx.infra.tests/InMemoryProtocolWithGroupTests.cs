using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryProtocolWithGroupTests
    {
        private IProtocolWithGroupRepository _protocolWithGroupRepository;

        public InMemoryProtocolWithGroupTests()
        {
            _protocolWithGroupRepository = new InMemoryProtocolWithGroupRepository(
                new InMemoryProtocolGroupRepository()
            );
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
            var expected = "Breast";

            var protocolWithGroup = _protocolWithGroupRepository.GetByKey(5);
            var actual = protocolWithGroup.ProtocolGroupName;

            Assert.Equal(expected, actual);
        }

        // TODO CS2
        // [Fact]
        //public void GetLatestVersionForProtocol()
        //{

        //}

        [Fact]
        public void ListAll()
        {
            var expected = 5;

            var protocols = _protocolWithGroupRepository.List();
            var actual = protocols.Count;

            Assert.Equal(expected, actual);
        }

        // TODO CS2
        // [Fact]
        //public void Update()
        //{

        //}
    }
}
