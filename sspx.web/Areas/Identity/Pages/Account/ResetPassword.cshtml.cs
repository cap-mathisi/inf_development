using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core.interfaces;
using sspx.web.Helpers;
using sspx.web.Models;
using sspx.web.Services;

namespace sspx.web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentitySSPxUser> _userManager;
        private readonly ISSPxEmailSender _emailSender;
        private IProtocolVersionRepository _protocolVersionRepository;

        public ResetPasswordModel(UserManager<IdentitySSPxUser> userManager, ISSPxEmailSender emailSender, IProtocolVersionRepository protocolVersionRepository)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _protocolVersionRepository = protocolVersionRepository;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "User ID is required")]
            [Display(Name = "User ID:")]
            public string UserName { get; set; }
        }

        [TempData]
        public string ErrorMessage { get; set; }

        private bool ValidateUser()
        {
            var sample = _protocolVersionRepository.ValidUserID();
            for (int i = 0; i < sample.Count; i++)
            {
                if (sample[i].UserID == Input.UserName || sample[i].Email == Input.UserName)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser())
                {
                    var user = await _userManager.FindByNameAsync(Input.UserName);

                    if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                    {
                        // Don't reveal that the user does not exist or is not confirmed
                        return RedirectToPage("./ResetPasswordConfirmation");
                    }

                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/NewPassword",
                        pageHandler: null,
                        values: new { c = code, u = user.UserName },
                        protocol: Request.Scheme);

                    var emailVariables = new Dictionary<string, string>() {
                    {
                        "[PASSWORD_RESET_URL]", HtmlEncoder.Default.Encode(callbackUrl)
                    }
                };
                    var emailHtml = EmailHelper.CreateEmailFromTemplate(EmailTemplates.RESET_PASSWORD, emailVariables);

                    await _emailSender.SendEmailOneOrMoreRecipientsAsync(user.Email, "Reset Password", emailHtml);

                    return RedirectToPage("./ResetPasswordConfirmation");
                }
                else
                {
                    ErrorMessage = "Please Enter the Valid User Name or Email";
                    return Page();
                }
            }
            return Page();
        }
    }
}
