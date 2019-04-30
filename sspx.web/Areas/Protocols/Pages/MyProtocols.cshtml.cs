using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.web.Helpers;
using sspx.web.Models;
using sspx.web.Services;

namespace sspx.web.Areas.Protocols.Pages
{
    public class MyProtocolsModel : PageModel
    {
        public int UserKey { get; private set; }

        private IProtocolIndexData _protocolIndexData;

        [BindProperty]
        public ProtocolIndexModel ProtocolIndex { get; set; }

        public MyProtocolsModel(IProtocolIndexData protocolIndexData)
        {
            _protocolIndexData = protocolIndexData;
        }

        public void OnGet()
        {
            UserKey = HttpContext.Session.Get<int>("userKey");
            ProtocolIndex = _protocolIndexData.GetForUser(UserKey);
        }
    }
}