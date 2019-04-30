using sspx.web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sspx.web.Services
{
    public interface INavMenuData
    {
        Task<List<ProtocolGroupMenuItem>> GetProtocolGroupMenuItems();
        Task<List<ProtocolMenuItem>> GetProtocolMenuItems(int userKey);
        Task<List<ProtocolVersionMenuItem>> GetProtocolVersions(int protocolKey);
    }
}
