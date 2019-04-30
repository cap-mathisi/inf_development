using System.Collections.Generic;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;

namespace sspx.web.Services
{
    public class VendorRepository : IVendorRepository
    {
        private ISSPxConfig _config;

        public VendorRepository(ISSPxConfig config)
        {
            _config = config;
        }

        public List<Vendor> List()
        {
            var vendors = SSPxDBHelper.GetVendorsActive(_config.SSPxConnectionString);
            return vendors ?? new List<Vendor>();
        }
    }
}
