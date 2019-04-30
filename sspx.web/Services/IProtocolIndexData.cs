using sspx.web.Models;

namespace sspx.web.Services
{
    public interface IProtocolIndexData
    {
        ProtocolIndexModel GetForUser(int userKey);

        ProtocolIndexModel Get();
    }
}
