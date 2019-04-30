using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.Areas.Admin.data;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Helpers;
using sspx.web.Services;

namespace sspx.Areas.Admin.Pages
{
    public class ProtocolPageModel : PageModel
    {
        private IProtocolWithGroupRepository _protocolWithGroupRepository;
        private IProtocolGroupRepository _protocolGroupRepository;
        private IProtocolVersionRepository _protocolVersionRepository;

        public string Title { get; set; } = "Create Protocol";
        public SearchModel ProtocolsWithGroupsSearchModel { get; set; }

        [BindProperty]
        public int ProtocolKey { get; set; } = DefaultValue.Key;
        [BindProperty]
        public int ProtocolVersionKey { get; set; } = DefaultValue.Key;
        [BindProperty]
        public bool TestProtocol { get; set; } = false;

        public bool EditMode { get; set; } = false;

        #region User Inputs

        [BindProperty]
        [Display(Name = "Protocol Group")]
        [Range(1, int.MaxValue, ErrorMessage = "Protocol Group is required")]
        public int ProtocolGroupKey { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [BindProperty]
        [DataType(DataType.Text)]
        [Display (Name = "Protocol Name")]
        public string ProtocolName { get; set; }

        [Required(ErrorMessage = "Protocol Short Name is required")]
        [BindProperty]
        [DataType(DataType.Text)]
        [Display(Name = "Protocol Short Name")]
        public string ProtocolShortName { get; set; }

        [Required(ErrorMessage = "Protocol Version is required")]
        [BindProperty]
        [DataType(DataType.Text)]
        [Display(Name = "Protocol Version")]
        public string ProtocolVersion { get; set; }

        // CS2. DataType.Text (not Date) seems to avoid error with not persisting after validation fail https://stackoverflow.com/a/52188352/7803135
        [BindProperty]
        [DataType(DataType.Text)] 
        [Display(Name = "Publish Date")]
        public DateTime? PublishDate { get; set; } = null;

        [BindProperty]
        [Display(Name = "Status")]
        public bool Active { get; set; } = DefaultValue.Active;

        #endregion

        [BindProperty]
        public List<ProtocolGroup> ProtocolGroupsForDropDown { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public ProtocolPageModel(IProtocolWithGroupRepository protocolWithGroupRepository, IProtocolGroupRepository protocolGroupRepository, IProtocolVersionRepository protocolVersionRepository)
        {
            _protocolWithGroupRepository = protocolWithGroupRepository;
            _protocolGroupRepository = protocolGroupRepository;
            _protocolVersionRepository = protocolVersionRepository;
        }

        public IActionResult OnGet(int protocolKey)
        {
            SetUpPage(protocolKey);

            var protocolsWithGroups = _protocolWithGroupRepository.List();
            var pageUrl = Url.Page("Protocol", new { protocolKey = "" });
            ProtocolsWithGroupsSearchModel = SearchModel.FromProtocolsWithGroups(pageUrl, protocolsWithGroups);
            HttpContext.Session.Set("protocolsWithGroupsSearchModel", ProtocolsWithGroupsSearchModel);

            if (protocolKey > 0)
            {
                var protocolWithGroup = _protocolWithGroupRepository.GetByKey(protocolKey);

                // TODO CS2:
                var protocolVersion = _protocolVersionRepository.GetLatestVersionForProtocolIncludingInactive(protocolKey);
                var missingProtocolOrVersion = (protocolWithGroup == null || protocolVersion == null);
                if (missingProtocolOrVersion)
                {
                    // TODO CS2:
                    return RedirectToPage("Protocol", new { protocolKey = "" });
                }

                ProtocolGroupKey = protocolWithGroup.ProtocolGroupKey;
                ProtocolKey = protocolWithGroup.ProtocolKey;
                ProtocolName = protocolWithGroup.ProtocolName;
                ProtocolShortName = protocolWithGroup.ProtocolShortName;
                TestProtocol = protocolWithGroup.TestProtocol;
                Active = protocolWithGroup.ProtocolActive;

                ProtocolVersionKey = protocolVersion.ProtocolVersionKey;
                ProtocolVersion = protocolVersion.ProtocolVersionText;
                PublishDate = protocolVersion.ReleaseDate;
            }

            return Page();
        }

        public IActionResult OnPostSave([FromServices] IHostingEnvironment hostingEnvironment)
        {
            SetUpPage(this.ProtocolKey);
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var userKey = HttpContext.Session.Get<int>("userKey");

            // TODO CS2:

            var protocolWithGroup = new ProtocolWithGroup()
            {
                ProtocolGroupKey = this.ProtocolGroupKey,
                ProtocolName = this.ProtocolName,
                ProtocolShortName = this.ProtocolShortName,
                ProtocolSortName = this.ProtocolName, // default to name
                ProtocolActive = this.Active,
                LastUpdated = userKey
            };

            bool isNewProtocol = (this.ProtocolKey == DefaultValue.Key);
            if (isNewProtocol == true)
            {
                var createdInDevelopmentEnvironment = hostingEnvironment.IsDevelopment();

                protocolWithGroup.CreatedBy = userKey;
                protocolWithGroup.TestProtocol = createdInDevelopmentEnvironment;

                var newProtocolWithGroup = _protocolWithGroupRepository.Add(protocolWithGroup);
                if (newProtocolWithGroup == null)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create protocol");
                    StatusMessage = string.Empty;
                    return Page();
                }

                ProtocolKey = newProtocolWithGroup.ProtocolKey;

                var protocolVersion = new ProtocolVersion()
                {
                    ProtocolKey = ProtocolKey,
                    ProtocolVersionText = this.ProtocolVersion,
                    TestVersion = createdInDevelopmentEnvironment,
                    Title = this.ProtocolName, // defaulting to Protocol Name
                    SubTitle = string.Empty,
                    ReleaseDate = this.PublishDate,
                    WebPostingDate = this.PublishDate, // defaulting to Publish Date
                    UserKey = userKey,
                    LastUpdated = userKey,
                    Active = true
                };

                var newProtocolVersion = _protocolVersionRepository.Add(protocolVersion);
                if (newProtocolVersion == null)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create protocol version");
                    StatusMessage = string.Empty;
                    return Page();
                }

                ProtocolVersionKey = newProtocolVersion.ProtocolVersionKey;
                StatusMessage = "Protocol created successfully";
            }
            else
            {
                protocolWithGroup.ProtocolKey = this.ProtocolKey;
                protocolWithGroup.LastUpdated = userKey;
                protocolWithGroup.TestProtocol = this.TestProtocol;

                string error = _protocolWithGroupRepository.Update(protocolWithGroup);
                if (error != string.Empty)
                {
                    ModelState.AddModelError(string.Empty, "Could not save protocol");
                    StatusMessage = string.Empty;
                    return Page();
                }

                var protocolVersionToUpdate = _protocolVersionRepository.GetByKey(ProtocolVersionKey);
                protocolVersionToUpdate.ProtocolVersionText = this.ProtocolVersion;
                protocolVersionToUpdate.Title = this.ProtocolName;
                protocolVersionToUpdate.ReleaseDate = this.PublishDate;
                protocolVersionToUpdate.LastUpdated = userKey;

                error = _protocolVersionRepository.Update(protocolVersionToUpdate);
                if (error != string.Empty)
                {
                    ModelState.AddModelError(string.Empty, "Could not update protocol version");
                    StatusMessage = string.Empty;
                    return Page();
                }

                StatusMessage = "Protocol saved successfully";
            }

            return RedirectToPage("ProtocolCaseSummary", new { protocolVersionKey = this.ProtocolVersionKey });
        }

        // TODO CS2:

        private void SetUpPage(int protocolKey)
        {
            ProtocolGroupsForDropDown = _protocolGroupRepository.ListActive();

            if(protocolKey > 0)
            {
                EditMode = true;
                Title = $"Edit Protocol - {ProtocolName}";
            }
        }
    }
}