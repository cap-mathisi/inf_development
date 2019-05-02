using sspx.core.entities;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Services
{
    public class InMemoryProtocolWithGroupRepository : IProtocolWithGroupRepository
    {
        private IProtocolGroupRepository _protocolGroups;
        private List<ProtocolWithGroup> _protocolsWithGroups;

        public InMemoryProtocolWithGroupRepository(IProtocolGroupRepository protocolGroups)
        {
            _protocolGroups = protocolGroups;
            _protocolsWithGroups = new List<ProtocolWithGroup>
            {
                new ProtocolWithGroup
                {
                    ProtocolKey = 5,
                    ProtocolName = "Breast Invasive",
                    ProtocolShortName = "Breast Invasive",
                    ProtocolSortName = "Breast Invasive",
                    TestProtocol = false,
                    CreatedBy = 1,
                    LastUpdated = 1,
                    ProtocolActive = true,
                    ProtocolGroupKey = 1,
                    ProtocolGroupName = "Breast",
                    ProtocolGroupSortName = "Breast"
                },
                new ProtocolWithGroup
                {
                    ProtocolKey = 7,
                    ProtocolName = "Colon and Rectum",
                    ProtocolShortName = "Colon and Rectum",
                    ProtocolSortName = "Colon and Rectum",
                    TestProtocol = false,
                    CreatedBy = 1,
                    LastUpdated = 1,
                    ProtocolActive = true,
                    ProtocolGroupKey = 8,
                    ProtocolGroupName = "Gastrointestinal",
                    ProtocolGroupSortName = "Gastrointestinal"
                },
                new ProtocolWithGroup
                {
                    ProtocolKey = 52,
                    ProtocolName = "Urethra",
                    ProtocolShortName = "Urethra",
                    ProtocolSortName = "Urethra",
                    TestProtocol = false,
                   CreatedBy = 1,
                    LastUpdated = 1,
                    ProtocolActive = true,
                    ProtocolGroupKey = 11,
                    ProtocolGroupName = "Genitourinary",
                    ProtocolGroupSortName = "Genitourinary"
                },
                new ProtocolWithGroup
                {
                    ProtocolKey = 27,
                    ProtocolName = "Nasal Cavity and Paranasal Sinuses",
                    ProtocolShortName = "Nasal Cavity",
                    ProtocolSortName = "Nasal Cavity and Paranasal Sinuses",
                    TestProtocol = false,
                    CreatedBy = 1,
                    LastUpdated = 1,
                    ProtocolActive = true,
                    ProtocolGroupKey = 5,
                    ProtocolGroupName = "Head and Neck",
                    ProtocolGroupSortName = "Head and Neck"
                },
                new ProtocolWithGroup
                {
                    ProtocolKey = 36,
                    ProtocolName = "Pharynx",
                    ProtocolShortName = "Pharynx",
                    ProtocolSortName = "Pharynx",
                    TestProtocol = false,
                   CreatedBy = 1,
                    LastUpdated = 1,
                    ProtocolActive = true,
                    ProtocolGroupKey = 5,
                    ProtocolGroupName = "Head and Neck",
                    ProtocolGroupSortName = "Head and Neck"
                }
            };
        }

        public ProtocolWithGroup Add(ProtocolWithGroup protocolWithGroup)
        {
            int newKey = 1;
            if (_protocolsWithGroups.Any())
            {
                newKey = _protocolsWithGroups.Max(p => p.ProtocolKey) + 1;
            }

            // must manually update this for our fake data
            var protocolGroupName = _protocolGroups.GetByKey(protocolWithGroup.ProtocolGroupKey).ProtocolGroupName;

            var protocolWithGroupToAdd = protocolWithGroup;
            protocolWithGroupToAdd.ProtocolKey = newKey;
            protocolWithGroupToAdd.ProtocolGroupName = protocolGroupName;

            _protocolsWithGroups.Add(protocolWithGroupToAdd);

            return protocolWithGroupToAdd;
        }

        public string Delete(ProtocolWithGroup protocolWithGroup)
        {
            var protocolWithGroupToUpdate = _protocolsWithGroups.FirstOrDefault(p => p.ProtocolKey == protocolWithGroup.ProtocolKey);
            protocolWithGroupToUpdate.ProtocolActive = false;
            return string.Empty;
        }

        public ProtocolWithGroup GetByKey(int key)
        {
            return _protocolsWithGroups.FirstOrDefault(p => p.ProtocolKey == key);
        }

        public List<ProtocolWithGroup> ListActive()
        {
            return _protocolsWithGroups.Where(p => p.ProtocolActive == true).ToList();
        }

        public List<ProtocolWithGroup> List()
        {
            return _protocolsWithGroups;
        }

        public string Update(ProtocolWithGroup protocolWithGroup)
        {
            // must manually update this for our fake data
            var protocolGroupName = _protocolGroups.GetByKey(protocolWithGroup.ProtocolGroupKey).ProtocolGroupName;

            var protocolWithGroupToUpdate = _protocolsWithGroups.FirstOrDefault(
                p => p.ProtocolKey == protocolWithGroup.ProtocolKey
            );

            protocolWithGroupToUpdate.ProtocolGroupKey = protocolWithGroup.ProtocolGroupKey;
            protocolWithGroupToUpdate.ProtocolGroupName = protocolGroupName;
            protocolWithGroupToUpdate.ProtocolKey = protocolWithGroup.ProtocolKey;
            protocolWithGroupToUpdate.ProtocolName = protocolWithGroup.ProtocolName;
            protocolWithGroupToUpdate.ProtocolShortName = protocolWithGroup.ProtocolShortName;
            protocolWithGroupToUpdate.ProtocolSortName = protocolWithGroup.ProtocolSortName;
            protocolWithGroupToUpdate.TestProtocol = protocolWithGroup.TestProtocol;
            protocolWithGroupToUpdate.CreatedBy = protocolWithGroup.CreatedBy;
            protocolWithGroupToUpdate.ProtocolActive = protocolWithGroup.ProtocolActive;

            return string.Empty;
        }
    }
}
