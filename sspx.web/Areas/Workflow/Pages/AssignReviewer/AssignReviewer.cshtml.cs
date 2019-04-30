using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Models;
using sspx.web.Services;
using System.Text.Encodings.Web;
using sspx.web.Helpers;


namespace sspx.web.Areas.Workflow.Pages.AssignReviewer
{
    public class AssignReviewerModel : PageModel
    {
        private IProtocolVersionRepository _protocolVersionRepository;
        private IRoleRepository _roleRepository;
        private readonly ISSPxEmailSender _emailSender;
        private String _userKey;
        private String _fullName;
        private int _protocolversionkey;
        private String _requestHost;

        public AssignReviewerModel(IProtocolVersionRepository protocolVersionRepository, IRoleRepository roleRepository, ISSPxEmailSender emailSender)
        {
            _protocolVersionRepository = protocolVersionRepository;
            _roleRepository = roleRepository;
            _emailSender = emailSender;
        }

        [BindProperty]
        public AssignReviewerInputModel AssignReviewerInput { get; set; } = new AssignReviewerInputModel();

        public class AssignReviewerInputModel
        {

            [BindProperty]
            public int ProtocolVersionsKey { get; set; } = DefaultValue.Key;

            [Required(ErrorMessage = "Review StartDate are required")]
            [BindProperty]
            [DataType(DataType.Text)]
            [Display(Name = "From Date")]
            public DateTime? ReviewStartDate { get; set; } = null;

            [Required(ErrorMessage = "Review EndDate are required")]
            [BindProperty]
            [DataType(DataType.Text)]
            [Display(Name = "To Date")]
            public DateTime? ReviewEndDate { get; set; } = null;

            public List<LablelistAuthors> Authors { get; set; }

            public List<ChecklistReviewers> Reviewers { get; set; }


            [Display(Name = "All Reviewers")]
            public bool CheckAll { get; set; }

            [Display(Name = "Send Email Notification")]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Send Email Notification are required")]
            public bool isEmail { get; set; }


            [MaxLength(5000)]
            [Display(Name = "Custom Message")]
            public string CustomMessage { get; set; }

            public string AuthorOther { get; set; }

            public string ReviewerOther { get; set; }

            [TempData]
            public string ErrorMessage { get; set; }
        }



        /// <summary>
        /// Assing Reviewer
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            GetUserData();
            AssignReviewerInput.Reviewers = _protocolVersionRepository.GetReviewersbyProtocalVersion(_protocolversionkey);
            AssignReviewerInput.Authors = _protocolVersionRepository.GetAuthorsbyProtocalVersion(_protocolversionkey);
            var assignReviewer = _protocolVersionRepository.GetAssignReviewerById(_protocolversionkey);
            AssignReviewerInput.ReviewStartDate = assignReviewer.FirstOrDefault().ReviewStartDate == null ? DateTime.Now : assignReviewer.FirstOrDefault().ReviewStartDate;
            AssignReviewerInput.ReviewEndDate = assignReviewer.FirstOrDefault().ReviewEndDate == null ? DateTime.Now.AddDays(13) : assignReviewer.FirstOrDefault().ReviewEndDate;
            AssignReviewerInput.CustomMessage = assignReviewer.FirstOrDefault().CustomMessage;
            return Page();
        }

        public IActionResult OnPostSave([FromServices] IHostingEnvironment hostingEnvironment)
        {
            //AssignReviewerInput.Reviewers = _protocolVersionRepository.GetReviewersbyProtocalVersion(141);
            //AssignReviewerInput.Authors = _protocolVersionRepository.GetAuthorsbyProtocalVersion(141);
            try
            {
                string error = string.Empty;
                GetUserData();
                if (ModelState.IsValid == false)
                {
                    AssignReviewerInput.Reviewers = _protocolVersionRepository.GetReviewersbyProtocalVersion(_protocolversionkey);
                    return Page();
                }
                //string selectedAuthors = ConcatenateLablelistItems(AssignReviewerInput.Authors, AssignReviewerInput.AuthorOther, true);
                //string selectedReviwers = ConcatenateChecklistItems(AssignReviewerInput.Reviewers, AssignReviewerInput.ReviewerOther, true);
                DateTime from = AssignReviewerInput.ReviewStartDate.Value;
                DateTime to = AssignReviewerInput.ReviewEndDate.Value;
                string comment = AssignReviewerInput.CustomMessage;
                bool isEmail = AssignReviewerInput.isEmail;

                ProtocolVersion protocalVersion = new ProtocolVersion()
                {
                    ProtocolVersionKey = _protocolversionkey,
                    ReviewStartDate = AssignReviewerInput.ReviewStartDate.Value,
                    ReviewEndDate = AssignReviewerInput.ReviewEndDate.Value,
                    CustomMessage = AssignReviewerInput.CustomMessage,
                    LastUpdatedDt = DateTime.Now
                };

                //save protocalversion     
                error = _protocolVersionRepository.Update(protocalVersion);

                //save protocalversionuserrole

                for (int i = 0; i < AssignReviewerInput.Reviewers.Count; i++)
                {
                    if (AssignReviewerInput.Reviewers[i].Selected)
                    {
                        UserRole protocolVersionUserRole = new UserRole()
                        {
                            ProtocolVersionUserRoleKey = AssignReviewerInput.Reviewers[i].ProtocolVersionUserRoleKey,
                            Assignreviewerflag = AssignReviewerInput.Reviewers[i].Selected,
                        };
                        error = _roleRepository.Update(protocolVersionUserRole);
                    }
                }

                EmailNotificationReviewer(AssignReviewerInput);
                EmailNotificationAuthor(AssignReviewerInput);

                //return RedirectToPage("ProtocolCaseSummary");
            }
            catch (Exception ex)
            {

            }
            return RedirectToPage("/Index", new { area = "Dashboard" });
        }

        private string ConcatenateLablelistItems(List<LablelistAuthors> checkboxes, string otherDescription, bool addCommas)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var checkbox in checkboxes)
            {
                sb.Append($"{checkbox.AuthorName}");
                sb.Append(addCommas ? ", " : " ");
            }

            if (string.IsNullOrWhiteSpace(otherDescription) == false)
            {
                sb.Append(otherDescription.TrimStart());
            }

            return sb.ToString().TrimEnd().TrimEnd(',');
        }

        private string ConcatenateChecklistItems(List<ChecklistReviewers> checkboxes, string otherDescription, bool addCommas)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Selected == true)
                {
                    sb.Append($"{checkbox.ReviewerName}");
                    sb.Append(addCommas ? ", " : " ");
                }
            }

            if (string.IsNullOrWhiteSpace(otherDescription) == false)
            {
                sb.Append(otherDescription.TrimStart());
            }

            return sb.ToString().TrimEnd().TrimEnd(',');
        }

        private async void EmailNotificationAuthor(AssignReviewerInputModel assingnreviewer)
        {
            for (int i = 0; i < AssignReviewerInput.Authors.Count; i++)
            {
                //var callbackUrl = Url.Page(
                //        "Protocols/SSPxProtocolReader",
                //        values: new { vid = "141" });
                var callbackUrl = string.Format("{0}://{1}/{2}/{3}", Request.Scheme, _requestHost, "Protocols/SSPxProtocolReader", _protocolversionkey);
                var emailVariables = new Dictionary<string, string>()
            {
                { "[FIRST_NAME]", AssignReviewerInput.Authors[i].FirstName},
                { "[Custome_Message]", Convert.ToString(assingnreviewer.CustomMessage)  },
                { "[Protocol_Name]", AssignReviewerInput.Authors[i].ProtocolName },
                { "[Author_Email]", AssignReviewerInput.Authors[i].Email},
                {"[Primary_Author_Name]", ConcatenateLablelistItems(AssignReviewerInput.Authors, AssignReviewerInput.AuthorOther, true)},
                {"[EndDate]", Convert.ToString(assingnreviewer.ReviewEndDate) },
                { "[Access_Protocol]", HtmlEncoder.Default.Encode(callbackUrl) },
                { "[Author_Name]", _fullName },
                };
                var emailHtml = EmailHelper.CreateEmailFromTemplate(EmailTemplates.ASSIGN_REVIEWER, emailVariables);

                await _emailSender.SendEmailOneOrMoreRecipientsAsync(Convert.ToString(AssignReviewerInput.Authors[i].Email), "Single Source Product (SSP) - Protocol Review", emailHtml);

            }
        }

        private async void EmailNotificationReviewer(AssignReviewerInputModel assingnreviewer)
        {

            for (int i = 0; i < AssignReviewerInput.Reviewers.Count; i++)
            {
                if (AssignReviewerInput.Reviewers[i].Selected)
                {
                    //var callbackUrl = Url.Page(
                    //    "Protocols/SSPxProtocolReader",
                    //    values: new { vid = "141" });
                    var callbackUrl = string.Format("{0}://{1}/{2}/{3}", Request.Scheme, _requestHost, "Protocols/SSPxProtocolReader", _protocolversionkey);
                    var emailVariables = new Dictionary<string, string>()            {
                { "[FIRST_NAME]", AssignReviewerInput.Reviewers[i].FirstName},
                 { "[Custome_Message]", Convert.ToString(assingnreviewer.CustomMessage)  },
                { "[Protocol_Name]", AssignReviewerInput.Reviewers[i].ProtocolName },
                { "[Author_Email]",  AssignReviewerInput.Reviewers[i].Email },
                {"[Primary_Author_Name]", ConcatenateLablelistItems(AssignReviewerInput.Authors, AssignReviewerInput.AuthorOther, true)},
                {"[EndDate]", Convert.ToString(assingnreviewer.ReviewEndDate) },
                { "[Access_Protocol]", HtmlEncoder.Default.Encode(callbackUrl) },
                { "[Author_Name]", _fullName },
                    };
                    var emailHtml = EmailHelper.CreateEmailFromTemplate(EmailTemplates.ASSIGN_REVIEWER, emailVariables);

                    await _emailSender.SendEmailOneOrMoreRecipientsAsync(Convert.ToString(AssignReviewerInput.Reviewers[i].Email), "Single Source Product (SSP) - Protocol Review", emailHtml);
                }
            }

        }

        #region private method

        private void GetUserData()
        {
            if (HttpContext.Session.Get<int?>("userKey") != null)
            {
                _userKey = HttpContext.Session.Get<int>("userKey").ToString();
                _fullName = HttpContext.Session.Get<string>("userFullName");
                _protocolversionkey = HttpContext.Session.Get<int>("protocolVersionKey");
                _requestHost = Convert.ToString(Request.Host);
                if (_protocolversionkey == 0)
                { _protocolversionkey = 141; }
            }
        }

        #endregion
    }
}