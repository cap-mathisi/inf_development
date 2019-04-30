using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sspx.web.Models;
using sspx.web.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using sspx.web.Services;

namespace sspx.web.ViewComponents
{
    public class NavMenuViewComponent : ViewComponent
    {
        private SignInManager<IdentitySSPxUser> _signInManager;
        private INavMenuData _navMenuData;
        private IAdminPermissions _adminPermissions;

        public NavMenuViewComponent(SignInManager<IdentitySSPxUser> signInManager, INavMenuData navMenuData, IAdminPermissions adminPermissions)
        {
            _signInManager = signInManager;
            _navMenuData = navMenuData;
            _adminPermissions = adminPermissions;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            NavMenuModel model = new NavMenuModel();

            model.SignedIn = _signInManager.IsSignedIn(this.UserClaimsPrincipal);
            if(model.SignedIn == false)
            {
                return View(model);
            }

            model.UserKey = HttpContext.Session.Get<int>("userKey");
            model.AdminMenuPermission = _adminPermissions.GetForUser(model.UserKey);
            model.UserFullName = HttpContext.Session.Get<string>("userFullName");
            
            model.FirstMenuSelection = "Menu";
            model.SecondMenuSelection = "Select";

            string path = Request.Path.ToString().ToLower();

            // TODO CS2:
            model.PathTest = path;

            string razorPagePath = RouteData.Values.ContainsKey("page") ? RouteData.Values["page"].ToString().ToLower() : string.Empty;
            string idFromRoute = string.Empty;
            if (RouteData.Values.ContainsKey("pid"))
            {
                idFromRoute = RouteData.Values["pid"].ToString();
            }
            else if (RouteData.Values.ContainsKey("vid"))
            {
                idFromRoute = RouteData.Values["vid"].ToString();
            }

            if (path.StartsWith("/dashboard/") || path == "/")
            {
                model.FirstMenuSelection = "Dashboard";
            }
            else if (path.StartsWith("/admin"))
            {
                model.FirstMenuSelection = "Admin";
                model.SecondMenuSelection = AdminNavMenuItem.FromPagePath(razorPagePath).DisplayText;
            }
            else if (path.StartsWith("/protocols") || path.StartsWith("/workflow"))
            {
                model.FirstMenuSelection = "Protocols";

                model.ProtocolGroupMenuItems = await _navMenuData.GetProtocolGroupMenuItems();

                List<ProtocolMenuItem> protocolMenuItems = await _navMenuData.GetProtocolMenuItems(model.UserKey);
                model.ProtocolMenuItems = protocolMenuItems;

                if (idFromRoute != string.Empty)
                {
                    model.SelectedProtocolKey = int.Parse(idFromRoute);
                    var currentProtocol = protocolMenuItems.Where(m => m.Id == model.SelectedProtocolKey).FirstOrDefault();
                    if(currentProtocol != null)
                    {
                        model.SecondMenuSelection = currentProtocol.Name;
                    }

                    // TODO CS2:
                    List<ProtocolVersionMenuItem> protocolVersionMenuItems = await _navMenuData.GetProtocolVersions(model.SelectedProtocolKey);
                    model.ProtocolVersionMenuItems = protocolVersionMenuItems;

                    if (protocolVersionMenuItems.Any())
                    {
                        var currentProtocolVersionMenuItem = protocolVersionMenuItems.FirstOrDefault();
                        model.ProtocolVersionSelection = "Work Version";
                        model.ProtocolVersionLastUpdated = $"Last updated: {currentProtocolVersionMenuItem.LastUpdatedDate.ToShortDateString()}";
                    }
                    else
                    {
                        model.ProtocolVersionSelection = "No Versions available";
                        model.ProtocolVersionLastUpdated = string.Empty;
                    }
                }
            }

            return View(model);
        }
    }
}
