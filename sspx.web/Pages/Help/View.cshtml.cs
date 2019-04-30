using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.web.Pages.Help
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Protocol> _protocolRepository;

        public List<Protocol> Protocols { get; set; }

        public IndexModel(IRepository<Protocol> protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        public void OnGet()
        {
            Protocols = _protocolRepository.List();
        }
    }
}
