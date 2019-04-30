using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;
using sspx.web.Models;

namespace sspx.Areas.Admin.Pages
{
    // TODO CS2:
    public class UserPageModel : PageModel
    {
        private readonly ISSPxConfig _config;
        private readonly UserManager<IdentitySSPxUser> _userManager;

        [BindProperty]
        public int UserKey { get; set; } = DefaultValue.Key;

        [BindProperty]
        [Required(ErrorMessage = "User ID is required")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        public string FirstName { get; set; }

        [BindProperty]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MiddleName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [BindProperty]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [BindProperty]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Work # is invalid. ex: 555-555-5555")]
        public string WorkPhone { get; set; }

        [BindProperty]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Home # is invalid. ex: 555-555-5555")]
        public string HomePhone { get; set; }

        [BindProperty]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Cell # is invalid. ex: 555-555-5555")]
        public string CellPhone { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Display(Name = "User Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Type is required")]
        public int UserTypeKey { get; set; }

        // TODO CS2:
        [BindProperty]
        public string Qualifications { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public List<Qualification> UserQualifications { get; set; }
        public List<UserType> UserTypes { get; set; }

        public UserPageModel([FromServices] ISSPxConfig config, UserManager<IdentitySSPxUser> userManager)
        {
            _config = config;
            _userManager = userManager;

            // TODO CS2:
            UserQualifications = SSPxDBHelper.GetQualificationsActive(_config.SSPxConnectionString);
            UserTypes = SSPxDBHelper.GetUserTypesActive(_config.SSPxConnectionString);
        }

        public IActionResult OnGet(int userKey)
        {
            if (userKey > 0)
            {
                // TODO CS2:
                // Admin/User/194.100004300
                User user = SSPxDBHelper.GetUser(_config.SSPxConnectionString, userKey);

                if (user == null)
                {
                    return RedirectToPage("User", new { userKey = "" });
                }

                UserKey = user.UserKey;
                UserID = user.UserID;
                FirstName = user.FirstName;
                MiddleName = user.MiddleName;
                LastName = user.LastName;
                Email = user.Email;
                WorkPhone = user.WorkPhone;
                HomePhone = user.HomePhone;
                CellPhone = user.CellPhone;
                UserTypeKey = user.UserTypeKey;
                Qualifications = user.Qualifications;

                ViewData["Title"] = string.Format("Edit User - {0} {1}", FirstName, LastName);
            }
            else
            {
                ViewData["Title"] = string.Format("Create User");
            }

            return Page();
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm) == false)
            {
                // TODO CS2:
                User user = SSPxDBHelper.SearchUser(_config.SSPxConnectionString, SearchTerm);
                if (user == null)
                {
                    ErrorMessage = "Failed to find a user matching that term";
                    StatusMessage = string.Empty;
                    return RedirectToPage("User", new { userKey = "" });
                }

                return RedirectToPage("User", new { userKey = user.UserKey });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                // validating here instead of with [Required] because it's optional for Update/Delete.
                ErrorMessage = "Password is required";
                StatusMessage = string.Empty;
                return Page();
            }

            var userToCreate = new IdentitySSPxUser { UserName = this.UserID, Email = this.Email };
            var result = await _userManager.CreateAsync(userToCreate, this.Password);

            if (result.Succeeded == false)
            {
                ErrorMessage = string.Empty;
                StatusMessage = string.Empty;
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
                return Page();
            }

            // TODO CS2:
            /*
                _logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                    "~/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
           */

            User user = buildUser();
            // TODO CS2:
            var newUser = SSPxDBHelper.AddUser(_config.SSPxConnectionString, user);
            if (newUser == null)
            {
                ErrorMessage = "Failed to create a user";
                StatusMessage = string.Empty;
                return Page();
            }

            // TODO CS2:
            var userKey = newUser.UserKey;
            var identitySSPxUser = await _userManager.FindByNameAsync(UserID);
            identitySSPxUser.SSPxUserKey = userKey;
            var updateKeyResult = await _userManager.UpdateAsync(identitySSPxUser);

            if (updateKeyResult.Succeeded == false)
            {
                // TODO CS2:
                ErrorMessage = "Failed to set up user";
                StatusMessage = string.Empty;
                return Page();
            }

            ErrorMessage = string.Empty;
            StatusMessage = "User created successfully";
            return RedirectToPage("User", new { userKey = newUser.UserKey });
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            StatusMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Password) == false)
            {
                var IdentitySSPxUser = await _userManager.FindByNameAsync(UserID);

                // generating a token enforces our password rules - https://stackoverflow.com/a/45715804/7803135
                var token = await _userManager.GeneratePasswordResetTokenAsync(IdentitySSPxUser);
                var result = await _userManager.ResetPasswordAsync(IdentitySSPxUser, token, Password);

                if (result.Succeeded == false)
                {
                    ErrorMessage = string.Empty;
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, e.Description);
                    }
                    return Page();
                }
                StatusMessage = "Password updated";
            }

            User user = buildUser();
            // TODO CS2:
            string error = SSPxDBHelper.SaveUser(_config.SSPxConnectionString, user);

            if (error != string.Empty)
            {
                ErrorMessage = "Could not update user";
                return Page();
            }

            // TODO CS2:

            ErrorMessage = string.Empty;
            StatusMessage = "User updated";
            return RedirectToPage("User", new { userKey = UserKey });
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            // TODO CS2:
            string error = SSPxDBHelper.DeleteUser(_config.SSPxConnectionString, UserKey);

            if (error != string.Empty)
            {
                ErrorMessage = "Could not delete user";
                StatusMessage = string.Empty;
                return Page();
            }

            var IdentitySSPxUser = await _userManager.FindByNameAsync(UserID);
            var result = await _userManager.DeleteAsync(IdentitySSPxUser);

            if (result.Succeeded == false)
            {
                ErrorMessage = string.Empty;
                StatusMessage = string.Empty;
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
                return Page();
            }

            ErrorMessage = string.Empty;
            StatusMessage = "User deleted";
            return RedirectToPage("User", new { userKey = "" });
        }

        private User buildUser()
        {
            // TODO CS2:
            this.Qualifications = string.Empty;

            return new User()
            {
                UserKey = this.UserKey,
                UserID = this.UserID,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Email = this.Email,
                WorkPhone = this.WorkPhone,
                HomePhone = this.HomePhone,
                CellPhone = this.CellPhone,
                Password = this.Password,
                UserTypeKey = this.UserTypeKey,
                Qualifications = this.Qualifications
                // TODO CS2:
                // VendorKey = 
                // Specialties = 
            };
        }
    }
}