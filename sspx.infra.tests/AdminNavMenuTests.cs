using sspx.web.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class AdminNavMenuTests
    {
        [Fact]
        public void GivenProtocolGroupPagePathShouldReturnProtocolGroup()
        {
            var expected = AdminNavMenuItem.ProtocolGroup;

            var actual = AdminNavMenuItem.FromPagePath("/protocolGROUP");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenProtocolCaseSummaryPagePathShouldReturnProtocolCaseSummary()
        {
            var expected = AdminNavMenuItem.Protocol_CaseSummary;

            var actual = AdminNavMenuItem.FromPagePath("/ProtocolCaseSummary");

            Assert.Equal(expected, actual);
        }
        
    }
}
