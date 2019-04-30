using System;

namespace sspx.core.entities
{
    public class ProtocolPermission
    {
        public int PermissionKey = DefaultValue.Key;
        public string PermissionText = String.Empty;

        public static ProtocolPermissionTypes ToProtocolPermissionType(ProtocolPermission permission)
        {
            return (ProtocolPermissionTypes)permission.PermissionKey;
        }
    }
}
