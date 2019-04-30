using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Helpers;
using sspx.web.Models;
using sspx.web.Services;

namespace sspx.web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RequestOnlineAccessModel : PageModel
    {
        private readonly ICapEmails _capEmails;
        private readonly ISSPxEmailSender _emailSender;

        private readonly IQualificationRepository _qualificationRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly ISSPxUserRepository _sspxUserRepository;
        private readonly UserManager<IdentitySSPxUser> _userManager;
        private readonly IVendorRepository _vendorRepository;

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
            [Display(Name = "Name:")]
            public string FirstName { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string MiddleName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
            public string LastName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
            [EmailAddress]
            [Display(Name = "Email:")]
            [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Email is invalid")] // extra validation because ASP.Net Core considers test@test valid -- https://stackoverflow.com/a/38386015/7803135
            public string Email { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Work Phone:")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Work # is invalid. ex: 555-555-5555")]
            public string WorkPhone { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Cell Phone:")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Cell # is invalid. ex: 555-555-5555")]
            public string CellPhone { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "User ID is required") ]
            [Display(Name = "User ID:")]
            public string UserID { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
            [Display(Name = "Password:")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password:")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Are you associated with any CAP committees?")]
            public List<Checkbox> Committees { get; set; }

            [Display(Name = "Other Committees, specify:")]
            [MaxLength(50)]
            public string CommitteesOther { get; set; }

            [Display(Name = "Specialty")]
            public List<Checkbox> Specialties { get; set; }

            [Display(Name = "Other, specify:")]
            [MaxLength(50)]
            public string SpecialtyOther { get; set; }

            [Display(Name = "Qualification")]
            public List<Checkbox> Qualifications { get; set; }

            [Display(Name = "Other, specify:")]
            [MaxLength(50)]
            public string QualificationOther { get; set; }

            [Display(Name = "PDF")]
            public bool FormatPDF { get; set; }
            [Display(Name = "Excel")]
            public bool FormatExcel { get; set; }
            [Display(Name = "Word")]
            public bool FormatWord { get; set; }

            [Display(Name = "Who is your vendor?")]
            public int VendorKey { get; set; }

            [Display(Name = "Do you modify the checklist?")]
            public string ModifyChecklist { get; set; }
        }

        [BindProperty]
        public List<Vendor> VendorsForDropDown { get; private set; }

        public RequestOnlineAccessModel(ICapEmails capEmails, ISSPxEmailSender emailSender, ISSPxUserRepository sspxUserRepository, UserManager<IdentitySSPxUser> userManager, IQualificationRepository qualificationRepository, ISpecialtyRepository specialtyRepository, IUserTypeRepository userTypeRepository, IVendorRepository vendorRepository)
        {
            _capEmails = capEmails;
            _emailSender = emailSender;
            _sspxUserRepository = sspxUserRepository;
            _qualificationRepository = qualificationRepository;
            _specialtyRepository = specialtyRepository;
            _userManager = userManager;
            _userTypeRepository = userTypeRepository;
            _vendorRepository = vendorRepository;
        }

        public void OnGet()
        {
            SetUpPage();

            Input.Committees = _userTypeRepository.List()
                .Select(ut => Checkbox.FromUserType(ut))
                .ToList();

            Input.Specialties = _specialtyRepository.List()
                .Select(s => Checkbox.FromSpecialty(s))
                .ToList();

            Input.Qualifications = _qualificationRepository.List()
                .Select(q => Checkbox.FromQualification(q))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SetUpPage();

            if (ModelState.IsValid == false)
            {
                return Page();
            }

            if(await CreateIdentityUser() == false)
            {
                return Page();
            }

            var sspxUser = CreateSSPxUser();
            if (sspxUser == null)
            {
                return Page();
            }

            var identityUser = await _userManager.FindByNameAsync(sspxUser.UserID);
            if (identityUser == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to add user (could not find Identity user)");
                return Page();
            }

            if (await LinkIdentityUserToSSPxUser(identityUser, sspxUser) == false)
            {
                return Page();
            }

            var staffEmails = _capEmails.GetCAP_StaffEmails();

            EmailCAPStaff(staffEmails);
            EmailUserConfirmation(identityUser);

            return RedirectToPage("./RequestOnlineAccessConfirmation");
        }

        #region Helper methods

        private void SetUpPage()
        {
            VendorsForDropDown = _vendorRepository.List();
        }

        private void addErrorsToModelState(IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private User BuildSSPxUserModel()
        {
            string selectedCommittees = ConcatenateChecklistItems(Input.Committees, Input.CommitteesOther, true);
            string selectedSpecialties = ConcatenateChecklistItems(Input.Specialties, Input.SpecialtyOther, true);
            string selectedQualifications = ConcatenateChecklistItems(Input.Qualifications, Input.QualificationOther, true);

            // TODO CS2:
            User sspxUser = new User
            {
                UserID = Input.UserID,
                FirstName = Input.FirstName,
                MiddleName = Input.MiddleName,
                LastName = Input.LastName,
                Email = Input.Email,
                WorkPhone = Input.WorkPhone,
                CellPhone = Input.CellPhone,
                UserType = selectedCommittees,
                Specialties = selectedSpecialties,
                Qualifications = selectedQualifications,
                // formats,
                VendorKey = Input.VendorKey
                // modifychecklist (convert to bool)
            };

            return sspxUser;
        }

        private async Task<bool> CreateIdentityUser()
        {
            var identityUserToAdd = new IdentitySSPxUser
            {
                UserName = Input.UserID,
                Email = Input.Email
            };
            var createIdentityUserResult = await _userManager.CreateAsync(identityUserToAdd, Input.Password);
            if (createIdentityUserResult.Succeeded == false)
            {
                addErrorsToModelState(createIdentityUserResult);
                return false;
            }
            return true;
        }

        private User CreateSSPxUser()
        {
            User sspxUserToCreate = BuildSSPxUserModel();
            var user = _sspxUserRepository.Add(sspxUserToCreate);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to add user");
            }
            return user;
        }

        private async Task<bool> LinkIdentityUserToSSPxUser(IdentitySSPxUser identityUser, User sspxUser)
        {
            identityUser.SSPxUserKey = sspxUser.UserKey;

            var updateIdentityUserResult = await _userManager.UpdateAsync(identityUser);
            if (updateIdentityUserResult.Succeeded == false)
            {
                addErrorsToModelState(updateIdentityUserResult);
                return false;
            }

            return true;
        }

        private async void EmailCAPStaff(string staffEmails)
        {
            var subject = $"{Input.FirstName} {Input.LastName} has requested online access";

            var emailVariables = new Dictionary<string, string>()
            {
                { "[FIRST_NAME]", Input.FirstName },
                { "[LAST_NAME]", Input.LastName },
                { "[USER_ID]", Input.UserID },
                // { "[APPROVAL_LINK]", "TODO" }, // TODO CS2: add once we get direction from CAP
            };
            var emailHtml = EmailHelper.CreateEmailFromTemplate(EmailTemplates.USER_REQUESTED_ONLINE_ACCESS, emailVariables);

            await _emailSender.SendEmailOneOrMoreRecipientsAsync(staffEmails, subject, emailHtml);
        }

        private async void EmailUserConfirmation(IdentitySSPxUser identityUser)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = identityUser.Id, code = code },
                protocol: Request.Scheme);

            var emailVariables = new Dictionary<string, string>()
            {
                { "[FIRST_NAME]", Input.FirstName },
                { "[USER_ID]", Input.UserID },
                { "[EMAIL_CONFIRMATION_LINK]", HtmlEncoder.Default.Encode(callbackUrl) }
            };
            var emailHtml = EmailHelper.CreateEmailFromTemplate(EmailTemplates.ACCOUNT_CONFIRMATION, emailVariables);

            await _emailSender.SendEmailOneOrMoreRecipientsAsync(Input.Email, "Single Source Product (SSP) - User Account", emailHtml);
        }

        private string ConcatenateChecklistItems(List<Checkbox> checkboxes, string otherDescription, bool addCommas)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var checkbox in checkboxes)
            {
                if(checkbox.Selected == true)
                {
                    sb.Append($"{checkbox.Title}");
                    sb.Append(addCommas ? ", " : " ");
                }
            }

            if (string.IsNullOrWhiteSpace(otherDescription) == false)
            {
                sb.Append(otherDescription.TrimStart());
            }

            return sb.ToString().TrimEnd().TrimEnd(',');
        }

        #endregion

    }
}