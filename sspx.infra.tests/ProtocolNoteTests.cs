using System;
using System.Collections.Generic;
using Xunit;
using sspx.infra.data;
using sspx.core.entities;

namespace sspx.infra.tests
{
    public class ProtocolNoteTests : IClassFixture<SSPDataFixture>
    {
        SSPDataFixture _fixture;

        public ProtocolNoteTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetProtocolNotesWork()
        {
            var protocolNotes = SSPxDBHelper.GetProtocolNotes(_fixture.SSPxTestConfig.SSPxConnectionString, EditMode.Work);
            var actual = protocolNotes.Count;

            Assert.True(actual > 0);
        }

        //[Fact]
        // NAA. Check for nulls later.
        public void GetProtocolNotesDraft()
        {
            var protocolNotes = SSPxDBHelper.GetProtocolNotes(_fixture.SSPxTestConfig.SSPxConnectionString, EditMode.Draft);
            var actual = protocolNotes.Count;

            Assert.True(actual > 0);
        }

        [Fact]
        public void GetProtocolNote()
        {
            var protocolNotes = SSPxDBHelper.GetProtocolNotes(_fixture.SSPxTestConfig.SSPxConnectionString, EditMode.Release);
            var actual = protocolNotes.Count;

            Assert.True(actual > 0);
        }

    }
}
