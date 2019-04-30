using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core.interfaces;
using sspx.web.Helpers;
using sspx.web.Models;
using sspx.web.Services;

namespace sspx.Areas.Dashboard.Pages
{
    public class IndexModel : PageModel
    {
        public ProtocolIndexModel MyProtocols { get; private set; }
        public ProtocolIndexModel ProtocolToDoList { get; private set; }
        public int ProtocolCount { get; private set; }

        public int UserKey { get; private set; }

        private IProtocolIndexData _protocolIndexData;
        private IProtocolWithGroupRepository _protocolRepository;

        public IndexModel(IProtocolIndexData protocolIndexData, IProtocolWithGroupRepository protocolRepository)
        {
            _protocolIndexData = protocolIndexData;
            _protocolRepository = protocolRepository;
        }

        public void OnGet()
        {
            UserKey = HttpContext.Session.Get<int>("userKey");

            ProtocolToDoList = _protocolIndexData.GetForUser(UserKey);
            MyProtocols = _protocolIndexData.GetForUser(UserKey);
            ProtocolCount = _protocolRepository.ListActive().Count;
        }
    }
}