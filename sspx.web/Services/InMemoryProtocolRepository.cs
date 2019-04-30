using sspx.core.entities;
using sspx.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Services
{
    // TODO CS2:
    public class InMemoryProtocolRepository : IAdminRepository<Protocol>
    {
        private Dictionary<int, Protocol> _protocols;

        public InMemoryProtocolRepository()
        {
            _protocols = new Dictionary<int, Protocol>
            {
                {
                    5,
                    new Protocol
                    {
                        ProtocolKey = 5,
                        NamespaceKey = 1,
                        ProtocolName = "Breast Invasive",
                        ProtocolShortName = "Breast Invasive",
                        ProtocolSortName = "Breast Invasive",
                        Active = true
                    }
                 },
                {
                    7,
                    new Protocol
                    {
                        ProtocolKey = 7,
                        NamespaceKey = 1,
                        ProtocolName = "Colon and Rectum",
                        ProtocolShortName = "Colon and Rectum",
                        ProtocolSortName = "Colon and Rectum",
                        Active = true
                    }
                 },
                {
                    52,
                    new Protocol
                    {
                        ProtocolKey = 52,
                        NamespaceKey = 1,
                        ProtocolName = "Urethra",
                        ProtocolShortName = "Urethra",
                        ProtocolSortName = "Urethra",
                        Active = true
                    }
                 },
                {
                    27,
                    new Protocol
                    {
                        ProtocolKey = 27,
                        NamespaceKey = 1,
                        ProtocolName = "Nasal Cavity and Paranasal Sinuses",
                        ProtocolShortName = "Nasal Cavity",
                        ProtocolSortName = "Nasal Cavity and Paranasal Sinuses",
                        Active = true
                    }
                 },
                {
                    36,
                    new Protocol
                    {
                        ProtocolKey = 36,
                        NamespaceKey = 1,
                        ProtocolName = "Pharynx",
                        ProtocolShortName = "Pharynx",
                        ProtocolSortName = "Pharynx",
                        Active = true
                    }
                 },
            };
        }

        public Protocol Add(Protocol protocol)
        {
            int newKey = 1;
            if (_protocols.Values.Any())
            {
                newKey = _protocols.Values.Max(p => p.ProtocolKey) + 1;
            }

            var protocolToAdd = protocol;
            protocolToAdd.ProtocolKey = newKey;

            _protocols.Add(newKey, protocolToAdd);

            return protocolToAdd;
        }

        public string Delete(Protocol protocol)
        {
            var protocolToUpdate = _protocols[protocol.ProtocolKey];
            protocolToUpdate.Active = false;
            protocolToUpdate.LastUpdated = protocol.LastUpdated;

            return string.Empty;
        }

        // TODO CS2:
        public Protocol GetByCkey(decimal cKey)
        {
            throw new NotImplementedException();
        }

        public Protocol GetByKey(int key)
        {
            return _protocols[key];
        }

        public List<Protocol> List()
        {
            return _protocols.Values.OrderBy(p => p.ProtocolSortName).ToList();
        }

        public string Update(Protocol protocol)
        {
            var protocolToUpdate = _protocols[protocol.ProtocolKey];

            protocolToUpdate.ProtocolName = protocol.ProtocolName;
            protocolToUpdate.ProtocolSortName = protocol.ProtocolName; // default for sort name
            protocolToUpdate.LastUpdated = protocol.LastUpdated;
            protocolToUpdate.Active = protocol.Active;

            return string.Empty;
        }
    }
}
