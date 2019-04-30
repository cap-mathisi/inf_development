using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        public List<User> Users { get; set; }

        public IndexModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void OnGet()
        {
            Users = _userRepository.List();
        }
    }
}
