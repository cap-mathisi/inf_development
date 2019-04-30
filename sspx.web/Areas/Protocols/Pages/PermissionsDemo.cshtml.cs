using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using sspx.core.entities;
using sspx.web.Helpers;
using sspx.web.Services;
using System;

namespace sspx.web.Areas.Protocols.Pages
{
    public class PermissionsDemoModel : PageModel
    {
        private IPagePermissions _pagePermissions;
        private IProtocolPermissions _protocolPermissions;
        private IAdminPermissions _adminAccess;
        private IConfiguration _temporaryConfig;

        public bool PermissionsBypassEnabled { get; set; }
        public bool HasPagePermission { get; set; }
        public bool CanEditBreast { get; set; }
        public bool CanViewBreast { get; set; }
        public bool CanEditUrethra { get; set; }
        public bool CanViewUrethra { get; set; }
        public string AdminMenuAccess { get; set; }

        public int UserKey { get; set; }

        public PermissionsDemoModel(IPagePermissions pagePermissions, IProtocolPermissions protocolPermissions, IAdminPermissions adminAccess, IConfiguration temporaryConfig)
        {
            _pagePermissions = pagePermissions;
            _protocolPermissions = protocolPermissions;
            _adminAccess = adminAccess;
            _temporaryConfig = temporaryConfig;
        }

        public void OnGet()
        {
            UserKey = HttpContext.Session.Get<int>("userKey");

            // TODO CS2:
            PermissionsBypassEnabled = string.Equals(_temporaryConfig["SSPX_PERMISSIONS_BYPASS_ALL"], "true", StringComparison.OrdinalIgnoreCase);

            HasPagePermission = _pagePermissions.HasPermission(UserKey, this.Request.Path);
            CanEditBreast = _protocolPermissions.HasPermission(UserKey, 5, ProtocolPermissionTypes.EditProtocol);
            CanViewBreast = _protocolPermissions.HasPermission(UserKey, 5, ProtocolPermissionTypes.View);
            CanEditUrethra = _protocolPermissions.HasPermission(UserKey, 52, ProtocolPermissionTypes.EditProtocol);
            CanViewUrethra = _protocolPermissions.HasPermission(UserKey, 52, ProtocolPermissionTypes.View);
            AdminMenuAccess = _adminAccess.GetForUser(UserKey).ToString();
        }
    }
}