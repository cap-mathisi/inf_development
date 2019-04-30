using System;

namespace sspx.infra.config
{
    public class SSPxConfig : ISSPxConfig
    {
        public string SSPxConnectionString { get; }

        public SSPxConfig(string sspxConnectionString)
        {
            SSPxConnectionString = sspxConnectionString;
        }
    }
}
