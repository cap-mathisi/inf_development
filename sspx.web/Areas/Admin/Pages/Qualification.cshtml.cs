using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.Areas.Admin.data;
using sspx.core.entities;
using sspx.web.Helpers;
using sspx.web.Services;

namespace sspx.Areas.Admin.Pages
{
    public class QualificationPageModel : PageModel
    {
        private IQualificationRepository _qualificationRepository;

        public SearchModel QualificationsSearchModel { get; set; }

        [BindProperty]
        public int QualificationKey { get; set; } = DefaultValue.Key;

        [Required(ErrorMessage = "Credentials are required")]
        [BindProperty]
        [DataType(DataType.Text)]
        [Display(Name = "Credentials")]
        public string QualificationTxt { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [BindProperty]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [BindProperty]
        [Display(Name = "Status")]
        public bool Active { get; set; } = DefaultValue.Active;

        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public QualificationPageModel([FromServices] IQualificationRepository qualificationRepository)
        {
            _qualificationRepository = qualificationRepository;
        }

        public IActionResult OnGet(int qualificationKey)
        {
            var pageUrl = Url.Page("Qualification", new { qualificationKey = "" });
            var qualifications = _qualificationRepository.List();

            QualificationsSearchModel = SearchModel.FromQualifications(pageUrl, qualifications);
            HttpContext.Session.Set("qualificationSearchModel", QualificationsSearchModel);

            if (qualificationKey > 0)
            {
                Qualification qualification = _qualificationRepository.GetByKey(qualificationKey);
                if (qualification == null)
                {
                    return RedirectToPage("Qualification", new { qualificationKey = "" });
                }

                QualificationKey = qualification.QualificationKey;
                QualificationTxt = qualification.QualificationTxt;
                Description = qualification.Description;
                Active = qualification.Active;

                ViewData["Title"] = string.Format("Edit Qualification - {0}", QualificationTxt);
            }
            else
            {
                ViewData["Title"] = "Create Qualification";
            }

            return Page();
        }

        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var userKey = HttpContext.Session.Get<int>("userKey");
            Qualification qualification = new Qualification()
            {
                QualificationTxt = this.QualificationTxt,
                Description = this.Description,
                Active = this.Active
            };

            var qualificationKey = this.QualificationKey;
            bool isNewQualification = (qualificationKey == DefaultValue.Key);

            if (isNewQualification == true)
            {
                qualification.CreatedBy = userKey;
                qualification.LastUpdated = userKey;

                var newQualification = _qualificationRepository.Add(qualification);
                if (newQualification == null)
                {
                    ErrorMessage = "Failed to create qualification";
                    StatusMessage = string.Empty;
                    return Page();
                }
                qualificationKey = newQualification.QualificationKey;
                ErrorMessage = string.Empty;
                StatusMessage = "Qualification created successfully";
            }
            else
            {
                qualification.QualificationKey = qualificationKey;
                qualification.LastUpdated = userKey;

                string error = _qualificationRepository.Update(qualification);
                if (error != string.Empty)
                {
                    ErrorMessage = "Could not save qualification";
                    StatusMessage = string.Empty;
                    return Page();
                }

                ErrorMessage = string.Empty;
                StatusMessage = "Qualification saved successfully";
            }

            return RedirectToPage("Qualification", new { qualificationKey = qualificationKey });
        }
    }
}