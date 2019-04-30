using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public class InMemoryVendorRepository : IVendorRepository
    {
        private List<Vendor> _vendors;

        public InMemoryVendorRepository()
        {
            _vendors = new List<Vendor>()
            {
                new Vendor
                {
                    VendorKey = 1,
                    VendorTxt = "Cerner CoPathPlus",
                    Description = "Cerner CoPathPlus"
                },
                new Vendor
                {
                    VendorKey = 2,
                    VendorTxt = "Dolbey",
                    Description = "Dolbey"
                },
                new Vendor
                {
                    VendorKey = 3,
                    VendorTxt = "Epic Beaker",
                    Description = "Epic Beaker"
                },
                new Vendor
                {
                    VendorKey = 4,
                    VendorTxt = "mTuitive",
                    Description = "mTuitive"
                },
                new Vendor
                {
                    VendorKey = 5,
                    VendorTxt = "mTuitive-Cerner Millennium",
                    Description = "mTuitive-Cerner Millennium"
                },
                new Vendor
                {
                    VendorKey = 6,
                    VendorTxt = "mTuitive-Cortex",
                    Description = "mTuitive-Cortex"
                },
                new Vendor
                {
                    VendorKey = 7,
                    VendorTxt = "mTuitive-Meditech",
                    Description = "mTuitive-Meditech"
                },
                new Vendor
                {
                    VendorKey = 8,
                    VendorTxt = "mTuitive-Sunquest Copath",
                    Description = "mTuitive-Sunquest Copath"
                },
                new Vendor
                {
                    VendorKey = 9,
                    VendorTxt = "mTuitive-Other LIS systems",
                    Description = "mTuitive-Other LIS systems"
                },
                new Vendor
                {
                    VendorKey = 10,
                    VendorTxt = "Novopath",
                    Description = "Novopath"
                },
                new Vendor
                {
                    VendorKey = 11,
                    VendorTxt = "Psyche Systems",
                    Description = "Psyche Systems"
                },
                new Vendor
                {
                    VendorKey = 12,
                    VendorTxt = "Sunquest Powerpath",
                    Description = "Sunquest Powerpath"
                },
                new Vendor
                {
                    VendorKey = 13,
                    VendorTxt = "Voicebrook",
                    Description = "Voicebrook"
                }
            };
        }

        public List<Vendor> List()
        {
            return _vendors;
        }
    }
}
