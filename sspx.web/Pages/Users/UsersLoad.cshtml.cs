using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.web.Pages.Users
{
    public class LoadModel : PageModel
    {
        private readonly IRepository<Protocol> _userRepository;

        public LoadModel(IRepository<Protocol> userRepository)
        {
            _userRepository = userRepository;
        }

        public int RecordsAdded { get; set; }

        public void OnGet()
        {
            RecordsAdded = DatabasePopulator.LoadProtocols(_userRepository);
        }
    }
}
