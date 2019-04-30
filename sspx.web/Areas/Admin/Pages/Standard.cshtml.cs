using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.Areas.Admin.data;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Helpers;

namespace sspx.Areas.Admin.Pages
{
    public class StandardPageModel : PageModel
    {
        private IAdminRepository<Standard> _standardRepository;

        public SearchModel StandardSearchModel { get; set; }

        [BindProperty]
        public decimal StandardCkey { get; set; } = DefaultValue.Ckey;

        public bool EditMode { get; set; } = false;

        [Required(ErrorMessage = "Standard is required")]
        [BindProperty]
        [DataType(DataType.Text)]
        [Display(Name = "Standard")]
        public string StandardName { get; set; }

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

        public StandardPageModel([FromServices] IAdminRepository<Standard> standardRepository)
        {
            _standardRepository = standardRepository;
        }

        public IActionResult OnGet(decimal standardCkey)
        {
            var standards = _standardRepository.List();
            var pageUrl = Url.Page("Standard", new { standardCkey = "" });

            StandardSearchModel = SearchModel.FromStandards(pageUrl, standards);
            HttpContext.Session.Set("standardSearchModel", StandardSearchModel);

            if (standardCkey > 0)
            {
                // TODO CS2:
                var standardCkeyToGet = standardCkey;
                if ((standardCkeyToGet % 1) == 0)
                {
                    standardCkeyToGet += 0.1000043m;
                }

                Standard standard = _standardRepository.GetByCkey(standardCkeyToGet);
                if (standard == null)
                {
                    return RedirectToPage("Standard", new { standardCkey = "" });
                }

                StandardCkey = standard.BasedOnCkey;
                StandardName = standard.BasedOn;
                Description = standard.Description;
                Active = standard.Active;

                ViewData["Title"] = string.Format("Edit Standard - {0}", StandardName);
                EditMode = true;
            }
            else
            {
                ViewData["Title"] = "Create Standard";
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
            Standard standard = new Standard()
            {
                BasedOn = this.StandardName,
                Description = this.Description,
                Active = this.Active
            };

            var standardCkey = this.StandardCkey;
            bool isNewStandard = (standardCkey == DefaultValue.Ckey);

            if (isNewStandard == true)
            {
                standard.CreatedBy = userKey;
                standard.LastUpdated = userKey;

                var newStandard = _standardRepository.Add(standard);
                if (newStandard == null)
                {
                    ErrorMessage = "Failed to create Standard";
                    StatusMessage = string.Empty;
                    return Page();
                }
                standardCkey = newStandard.BasedOnCkey;
                ErrorMessage = string.Empty;
                StatusMessage = "Standard created successfully";
            }
            else
            {
                // TODO CS2:
                var standardCkeyToSave = standardCkey;
                if ((standardCkeyToSave % 1) == 0)
                {
                    standardCkeyToSave += 0.1000043m;
                }

                standard.BasedOnCkey = standardCkeyToSave;
                standard.LastUpdated = userKey;

                string error = _standardRepository.Update(standard);
                if (error != string.Empty)
                {
                    ErrorMessage = "Could not save standard";
                    StatusMessage = string.Empty;
                    return Page();
                }

                ErrorMessage = string.Empty;
                StatusMessage = "Standard saved successfully";
            }

            return RedirectToPage("Standard", new { standardCkey = standardCkey });
        }

        public IActionResult OnPostDelete()
        {
            var userKey = HttpContext.Session.Get<int>("userKey");
            var standard = new Standard()
            {
                BasedOnCkey = this.StandardCkey,
                LastUpdated = userKey
            };

            string error = _standardRepository.Delete(standard);
            if (error != string.Empty)
            {
                ErrorMessage = "Could not remove Standard";
                StatusMessage = string.Empty;
                return Page();
            }

            ErrorMessage = string.Empty;
            StatusMessage = "Standard has been deactivated";

            return RedirectToPage("Standard", new { standardCkey = "" });
        }
    }
}