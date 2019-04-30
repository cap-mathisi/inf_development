using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.web.Models;

namespace sspx.web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class NewPasswordModel : PageModel
    {
        private readonly UserManager<IdentitySSPxUser> _userManager;

        public NewPasswordModel(UserManager<IdentitySSPxUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "User ID is required")]
            [Display(Name = "User ID:")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "New password is required")]
            [Display(Name = "New password:")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password:")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string c = null, string u = null)
        {
            if (c == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else if (u == null)
            {
                return BadRequest("A User Name must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    UserName = u,
                    Code = c
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Input.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./NewPasswordConfirmation");
            }
            
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./NewPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
