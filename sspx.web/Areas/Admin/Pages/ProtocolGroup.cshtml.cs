using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.Areas.Admin.data;
using sspx.core.entities;
using sspx.web.Helpers;
using sspx.web.Services;

namespace sspx.Areas.Admin.Pages
{
    public class ProtocolGroupPageModel : PageModel
    {
        private IProtocolGroupRepository _protocolGroupRepository;

        public SearchModel ProtocolGroupSearchModel { get; set; }

        [BindProperty]
        public int ProtocolGroupKey { get; set; } = DefaultValue.Key;

        public bool EditMode { get; set; } = false;

        [Required(ErrorMessage = "Group is required")]
        [BindProperty]
        [DataType(DataType.Text)]
        [Display(Name = "Group Name")]
        public string ProtocolGroupName { get; set; }

        [BindProperty]
        [Display(Name = "Status")]
        public bool Active { get; set; } = DefaultValue.Active;

        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public ProtocolGroupPageModel([FromServices] IProtocolGroupRepository protocolGroupRepository)
        {
            _protocolGroupRepository = protocolGroupRepository;
        }

        public IActionResult OnGet(int protocolGroupKey)
        {
            var protocolGroups = _protocolGroupRepository.List();
            var pageUrl = Url.Page("ProtocolGroup", new { protocolGroupKey = "" });

            ProtocolGroupSearchModel = SearchModel.FromProtocolGroups(pageUrl, protocolGroups);
            HttpContext.Session.Set("protocolGroupSearchModel", ProtocolGroupSearchModel);

            if (protocolGroupKey > 0)
            {
                ProtocolGroup protocolGroup = _protocolGroupRepository.GetByKey(protocolGroupKey);
                if (protocolGroup == null)
                {
                    return RedirectToPage("ProtocolGroup", new { protocolGroupKey = "" });
                }

                protocolGroupKey = protocolGroup.ProtocolGroupKey;
                ProtocolGroupName = protocolGroup.ProtocolGroupName;
                Active = protocolGroup.Active;

                ViewData["Title"] = string.Format("Edit Protocol Group - {0}", ProtocolGroupName);
                EditMode = true;
            }
            else
            {
                ViewData["Title"] = "Create Protocol Group";
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
            ProtocolGroup protocolGroup = new ProtocolGroup()
            {
                ProtocolGroupName = this.ProtocolGroupName,
                ProtocolGroupSortName = this.ProtocolGroupName, // since we don't have a sort field in UI, just use the Group Name
                Active = this.Active
            };

            var protocolGroupKey = this.ProtocolGroupKey;
            bool isNewProtocolGroup = (protocolGroupKey == DefaultValue.Key);

            if (isNewProtocolGroup == true)
            {
                protocolGroup.CreatedBy = userKey;
                protocolGroup.LastUpdated = userKey;

                var newProtocolGroup = _protocolGroupRepository.Add(protocolGroup);
                if (newProtocolGroup == null)
                {
                    ErrorMessage = "Failed to create Protocol Group";
                    StatusMessage = string.Empty;
                    return Page();
                }
                protocolGroupKey = newProtocolGroup.ProtocolGroupKey;
                ErrorMessage = string.Empty;
                StatusMessage = "Protocol Group created successfully";
            }
            else
            {
                protocolGroup.ProtocolGroupKey = protocolGroupKey;
                protocolGroup.LastUpdated = userKey;

                string error = _protocolGroupRepository.Update(protocolGroup);
                if (error != string.Empty)
                {
                    ErrorMessage = "Could not save protocolGroup";
                    StatusMessage = string.Empty;
                    return Page();
                }

                ErrorMessage = string.Empty;
                StatusMessage = "Protocol Group saved successfully";
            }

            return RedirectToPage("ProtocolGroup", new { protocolGroupKey = protocolGroupKey });
        }

        public IActionResult OnPostDelete()
        {
            var userKey = HttpContext.Session.Get<int>("userKey");
            var protocolGroup = new ProtocolGroup()
            {
                ProtocolGroupKey = this.ProtocolGroupKey,
                LastUpdated = userKey
            };

            string error = _protocolGroupRepository.Delete(protocolGroup);
            if (error != string.Empty)
            {
                ErrorMessage = "Could not remove Protocol Group";
                StatusMessage = string.Empty;
                return Page();
            }

            ErrorMessage = string.Empty;
            StatusMessage = "Protocol Group has been deactivated";

            return RedirectToPage("ProtocolGroup", new { ProtocolGroupKey = "" });
        }
    }
}