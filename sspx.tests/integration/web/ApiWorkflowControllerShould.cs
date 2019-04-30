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

    public class ApiWorkflowControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApiWorkflowControllerShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        // WorkflowController.cs
        
        //[Fact]
        // public void SubmitDraft(EditSubmitRequest req)

        //[Fact]
    }
}