using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.web.Pages.Protocols
{
    public class LoadModel : PageModel
    {
        private readonly IRepository<Protocol> _protocolRepository;

        public LoadModel(IRepository<Protocol> protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        public int RecordsAdded { get; set; }

        public void OnGet()
        {
            RecordsAdded = DatabasePopulator.LoadProtocols(_protocolRepository);
        }
    }
}
