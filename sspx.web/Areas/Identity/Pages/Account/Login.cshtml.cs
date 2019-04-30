using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using sspx.core.entities;
using sspx.web.Helpers;
using sspx.infra.config;
using Microsoft.Extensions.Configuration;
using sspx.core.interfaces;
using sspx.web.Models;

namespace sspx.web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentitySSPxUser> _signInManager;
        private readonly ISSPxUserRepository _sspxUserRepository;
        private readonly UserManager<IdentitySSPxUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _appConfig;
        private readonly ISSPxConfig _config;

        public LoginModel(IConfiguration appConfig, [FromServices] ISSPxConfig config, ILogger<LoginModel> logger, SignInManager<IdentitySSPxUser> signInManager, UserManager<IdentitySSPxUser> userManager, ISSPxUserRepository sspxUserRepository)
        {
            _appConfig = appConfig;
            _config = config;
            _logger = logger;
            _signInManager = signInManager;
            _sspxUserRepository = sspxUserRepository;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "User ID is required")]
            [Display(Name = "User ID:")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password:")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index", new { area = "Dashboard" });
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, true);
                if (result.Succeeded)
                {
                    var identityUser = await _userManager.FindByNameAsync(Input.UserName);

                    var userKeyIsMissing = (identityUser.SSPxUserKey == null);
                    if (userKeyIsMissing)
                    {
                        await _signInManager.SignOutAsync();
                        _logger.LogWarning("User has Identity row but is missing SSPxUserKey.");

                        ModelState.AddModelError(string.Empty, "Invalid account");
                        return Page();
                    }

                    int userKey = identityUser.SSPxUserKey ?? DefaultValue.Key;
                    User user = _sspxUserRepository.GetByKey(userKey);
                    if(user == null)
                    {
                        await _signInManager.SignOutAsync();
                        _logger.LogWarning("User has Identity row but could not find corresponding SSPx User.");

                        ModelState.AddModelError(string.Empty, "Invalid account");
                        return Page();
                    }

                    var userFullName = string.Format("{0} {1}", user.FirstName, user.LastName);

                    // clear any session variables in case any hadn't expired yet
                    HttpContext.Session.Clear();

                    HttpContext.Session.Set("userKey", user.UserKey);
                    HttpContext.Session.Set("userFullName", userFullName);

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    // user email must be confirmed to get here
                    _logger.LogWarning("User account locked out.");
                    ModelState.AddModelError(string.Empty, "User account locked out, please try again later");
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
