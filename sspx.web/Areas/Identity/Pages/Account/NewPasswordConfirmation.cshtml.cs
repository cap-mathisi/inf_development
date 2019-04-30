using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sspx.web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class NewPasswordConfirmationModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
