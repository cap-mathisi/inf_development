using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using sspx.core.entities;
using sspx.web;

namespace sspx.tests.integration.web
{

    public class ApiProtocolsControllerListShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApiProtocolsControllerListShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        //[Fact]
        public async Task ReturnsThreeItems()
        {
            var response = await _client.GetAsync("/api/protocols");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Protocol>>(stringResponse).ToList();

            Assert.Equal(3, result.Count());
            Assert.Equal(1, result.Count(a => a.ProtocolName == "Breast, Template 123"));
            Assert.Equal(1, result.Count(a => a.ProtocolName == "Colon, Template 124"));
            Assert.Equal(1, result.Count(a => a.ProtocolName == "Prostate, Template 125"));
        }
    }
}