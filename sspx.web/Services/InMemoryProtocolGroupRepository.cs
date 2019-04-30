using System.Collections.Generic;
using System.Linq;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.web.Services
{
    public class InMemoryProtocolGroupRepository : IProtocolGroupRepository
    {
        private Dictionary<int, ProtocolGroup> _protocolGroup;

        public InMemoryProtocolGroupRepository()
        {
            _protocolGroup = new Dictionary<int, ProtocolGroup>
            {
                {
                    3,
                    new ProtocolGroup{
                        ProtocolGroupKey = 3,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Hematologic",
                        ProtocolGroupSortName = "Hematologic",
                        Active = true
                    }
                },
                {
                    14,
                    new ProtocolGroup{
                        ProtocolGroupKey = 14,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Biomarkers",
                        ProtocolGroupSortName = "Biomarkers",
                        Active = false
                    }
                },
                {
                    1,
                    new ProtocolGroup{
                        ProtocolGroupKey = 1,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Breast",
                        ProtocolGroupSortName = "Breast",
                        Active = true
                    }
                },
                {
                    2,
                    new ProtocolGroup{
                        ProtocolGroupKey = 2,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Central Nervous System",
                        ProtocolGroupSortName = "Central Nervous System",
                        Active = true
                    }
                },
                {
                    10,
                    new ProtocolGroup{
                        ProtocolGroupKey = 10,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Endocrine",
                        ProtocolGroupSortName = "Endocrine",
                        Active = true
                    }
                },
                {
                    8,
                    new ProtocolGroup{
                        ProtocolGroupKey = 8,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Gastrointestinal",
                        ProtocolGroupSortName = "Gastrointestinal",
                        Active = true
                    }
                },
                {
                    11,
                    new ProtocolGroup{
                        ProtocolGroupKey = 11,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Genitourinary",
                        ProtocolGroupSortName = "Genitourinary",
                        Active = true
                    }
                },
                {
                    6,
                    new ProtocolGroup{
                        ProtocolGroupKey = 6,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Gynecologic",
                        ProtocolGroupSortName = "Gynecologic",
                        Active = true
                    }
                },
                {
                    5,
                    new ProtocolGroup{
                        ProtocolGroupKey = 5,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Head and Neck",
                        ProtocolGroupSortName = "Head and Neck",
                        Active = true
                    }
                },
                {
                    12,
                    new ProtocolGroup{
                        ProtocolGroupKey = 12,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Ophthalmic",
                        ProtocolGroupSortName = "Ophthalmic",
                        Active = true
                    }
                },
                {
                    7,
                    new ProtocolGroup{
                        ProtocolGroupKey = 7,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Other",
                        ProtocolGroupSortName = "Other",
                        Active = true
                    }
                },
                {
                    13,
                    new ProtocolGroup{
                        ProtocolGroupKey = 13,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Pediatric",
                        ProtocolGroupSortName = "Pediatric",
                        Active = true
                    }
                },
                {
                    4,
                    new ProtocolGroup{
                        ProtocolGroupKey = 4,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Skin",
                        ProtocolGroupSortName = "Skin",
                        Active = true
                    }
                },
                {
                    9,
                    new ProtocolGroup{
                        ProtocolGroupKey = 9,
                        NamespaceKey = 1000043,
                        ProtocolGroupName = "Thorax",
                        ProtocolGroupSortName = "Thorax",
                        Active = true
                    }
                }
            };
        }

        public ProtocolGroup Add(ProtocolGroup protocolGroup)
        {
            int key = 1;
            if (_protocolGroup.Values.Any())
            {
                key = _protocolGroup.Values.Max(pg => pg.ProtocolGroupKey) + 1;
            }

            protocolGroup.ProtocolGroupKey = key;
            protocolGroup.ProtocolGroupSortName = protocolGroup.ProtocolGroupName; // default for sort name
            _protocolGroup.Add(key, protocolGroup);

            return protocolGroup;
        }

        public string Delete(ProtocolGroup protocolGroup)
        {
            var protocolGroupToUpdate = _protocolGroup[protocolGroup.ProtocolGroupKey];
            protocolGroupToUpdate.Active = false;
            protocolGroupToUpdate.LastUpdated = protocolGroup.LastUpdated;
            return string.Empty;
        }

        // TODO CS2:
        public ProtocolGroup GetByCkey(decimal cKey)
        {
            throw new System.NotImplementedException();
        }

        public ProtocolGroup GetByKey(int key)
        {
            return _protocolGroup[key];
        }

        public List<ProtocolGroup> List()
        {
            return _protocolGroup.Values.OrderBy(pg => pg.ProtocolGroupSortName).ToList();
        }

        public List<ProtocolGroup> ListActive()
        {
            return _protocolGroup.Values
                .Where(pg => pg.Active == true)
                .OrderBy(pg => pg.ProtocolGroupSortName).ToList();
        }

        public string Update(ProtocolGroup protocolGroup)
        {
            var protocolGroupToUpdate = _protocolGroup[protocolGroup.ProtocolGroupKey];

            protocolGroupToUpdate.ProtocolGroupName = protocolGroup.ProtocolGroupName;
            protocolGroupToUpdate.ProtocolGroupSortName = protocolGroup.ProtocolGroupName; // default for sort name
            protocolGroupToUpdate.LastUpdated = protocolGroup.LastUpdated;
            protocolGroupToUpdate.Active = protocolGroup.Active;

            return string.Empty;
        }
    }
}
