using System;
using System.IO;

// NAA.
// https://blogs.msdn.microsoft.com/premier_developer/2018/04/26/setting-up-net-core-configuration-providers/
using Microsoft.Extensions.Configuration;
using sspx.infra.config;

namespace sspx.infra.tests
{
    public class SSPDataFixture : IDisposable
    {
        public SSPxConfig SSPxTestConfig { get; }

        private IConfigurationRoot Configuration { get; set; }

        public SSPDataFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./appsettings.Development.json",
                             optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            SSPxTestConfig = new SSPxConfig(
                Configuration.GetConnectionString("SSPxData")
            );
        }

        public void Dispose()
        {

        }
    }
}