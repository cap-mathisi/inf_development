using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Models
{
    public class NavMenuModel
    {
        // TODO CS2:
        public string PathTest { get; set; }

        public bool SignedIn { get; set; } = false;
        public int UserKey { get; set; }
        public string UserFullName { get; set; }

        public string FirstMenuSelection { get; set; }
        public string SecondMenuSelection { get; set; }
        public string ProtocolVersionSelection { get; set; }
        public string ProtocolVersionLastUpdated { get; set; }

        public AdminMenuPermissionTypes AdminMenuPermission { get; set; }

        public List<ProtocolGroupMenuItem> ProtocolGroupMenuItems { get; set; }
        public List<ProtocolMenuItem> ProtocolMenuItems { get; set; }
        public List<ProtocolVersionMenuItem> ProtocolVersionMenuItems { get; set; }

        public int SelectedProtocolKey { get; set; } = DefaultValue.Key;

        public NavMenuModel()
        {
            AdminMenuPermission = AdminMenuPermissionTypes.None;
            ProtocolGroupMenuItems = new List<ProtocolGroupMenuItem>();
            ProtocolMenuItems = new List<ProtocolMenuItem>();
        }
    }
}
